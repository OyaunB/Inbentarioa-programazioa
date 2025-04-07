// DBGailuak.cs
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Inbentarioa
{
    public class GailuakDAL
    {
        private readonly string connectionString;

        public GailuakDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable ObtenerTodosGailuak()
        {
            DataTable table = new DataTable();

            string query = @"
                SELECT 
                    g.ID_Gailuak AS ID,
                    g.Gailu_Mota AS Gailu_Mota,
                    g.ID_Mintegia,
                    g.Marka,
                    g.Modeloa AS Modeloa,
                    g.Erosketa_data AS Erosketa_Data,
                    CASE WHEN e.ID_Gailuak IS NOT NULL THEN 1 ELSE 0 END AS Ezabatuta,
                    g.EgoeraGailua AS EgoeraGailua,
                    o.Memoria_RAM,
                    o.TxartelGrafikoa,
                    o.USB_Portuak
                FROM Gailuak g
                LEFT JOIN Ordenagailuak o ON g.ID_Gailuak = o.ID_Gailuak AND g.Gailu_Mota = 'Ordenagailuak'
                LEFT JOIN Imprimagailuak i ON g.ID_Gailuak = i.ID_Gailuak AND g.Gailu_Mota = 'Inprimagailuak'
                LEFT JOIN BesteGailuak b ON g.ID_Gailuak = b.ID_Gailuak AND g.Gailu_Mota = 'BesteGailuak'
                LEFT JOIN EzabatutakoGailuak e ON g.ID_Gailuak = e.ID_Gailuak";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los gailuak: " + ex.Message);
            }

            return table;
        }

        public void ActualizarGailua(int id, int idMintegia, string marka, string modeloa,
                       DateTime erosketaData, bool ezabatzekoMarka, string egoeraGailua)
        {
            // Normalizar el valor del estado
            egoeraGailua = NormalizarEgoeraGailua(egoeraGailua);

            string updateQuery = @"UPDATE gailuak 
                       SET ID_Mintegia = @ID_Mintegia, 
                           Marka = @Marka, 
                           Modeloa = @Modeloa, 
                           Erosketa_data = @Erosketa_data, 
                           EgoeraGailua = @EgoeraGailua,
                           EzabatzekoMarka = @EzabatzekoMarka
                       WHERE ID_Gailuak = @ID_Gailuak";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
            {
                cmd.Parameters.AddWithValue("@ID_Gailuak", id);
                cmd.Parameters.AddWithValue("@ID_Mintegia", idMintegia);
                cmd.Parameters.AddWithValue("@Marka", marka);
                cmd.Parameters.AddWithValue("@Modeloa", modeloa);
                cmd.Parameters.AddWithValue("@Erosketa_data", erosketaData);
                cmd.Parameters.AddWithValue("@EzabatzekoMarka", ezabatzekoMarka);
                cmd.Parameters.AddWithValue("@EgoeraGailua", egoeraGailua);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void MoverAEzabatutakoGailuak(int idGailuak, string marka, string modeloa)
        {
            string insertQuery = @"INSERT INTO EzabatutakoGailuak 
                          (ID_Gailuak, Data_Ezabatu, Marka, Modeloa) 
                          VALUES (@ID_Gailuak, @Data_Ezabatu, @Marka, @Modeloa)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
            {
                cmd.Parameters.AddWithValue("@ID_Gailuak", idGailuak);
                cmd.Parameters.AddWithValue("@Data_Ezabatu", DateTime.Now);
                cmd.Parameters.AddWithValue("@Marka", marka);
                cmd.Parameters.AddWithValue("@Modeloa", modeloa);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarGailuaCompleto(int idGailuak)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Mover a EzabatutakoGailuak
                        var datos = ObtenerDatosGailua(idGailuak);
                        if (datos != null)
                        {
                            MoverAEzabatutakoGailuak(idGailuak, datos.Split('|')[0], datos.Split('|')[1]);
                        }

                        // 2. Eliminar de tablas específicas
                        string[] erlazioTaulak = { "Imprimagailuak", "Ordenagailuak", "BesteGailuak" };
                        foreach (string taula in erlazioTaulak)
                        {
                            var deleteQuery = new MySqlCommand(
                                $"DELETE FROM {taula} WHERE ID_Gailuak = @ID_Gailuak",
                                connection, transaction);
                            deleteQuery.Parameters.AddWithValue("@ID_Gailuak", idGailuak);
                            deleteQuery.ExecuteNonQuery();
                        }

                        // 3. Eliminar de Gailuak
                        var deleteGailuak = new MySqlCommand(
                            "DELETE FROM Gailuak WHERE ID_Gailuak = @ID_Gailuak",
                            connection, transaction);
                        deleteGailuak.Parameters.AddWithValue("@ID_Gailuak", idGailuak);
                        deleteGailuak.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error al eliminar el gailua: " + ex.Message);
                    }
                }
            }
        }

        public string ObtenerDatosGailua(int idGailuak)
        {
            string selectQuery = @"SELECT Marka, Modeloa FROM gailuak WHERE ID_Gailuak = @ID_Gailuak";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("@ID_Gailuak", idGailuak);
                connection.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return $"{reader["Marka"]}|{reader["Modeloa"]}";
                    }
                }
            }
            return null;
        }

        public string NormalizarEgoeraGailua(string egoeraGailua)
        {
            if (string.IsNullOrWhiteSpace(egoeraGailua))
                return "Ongi";

            // Asegurar formato correcto (primera letra mayúscula, resto minúsculas)
            return char.ToUpper(egoeraGailua[0]) + egoeraGailua.Substring(1).ToLower();
        }
    }
}