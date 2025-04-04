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
            g.Gailu_Mota,
            g.ID_Mintegia,
            g.Marka,
            g.Modeloa AS Izena,
            g.Erosketa_data AS ErosketaData,
            CASE WHEN e.ID_Gailuak IS NOT NULL THEN 1 ELSE 0 END AS EstaEliminado,
            g.EgoeraGailua,
            o.Memoria_RAM,
            o.TxartelGrafikoa,
            o.USB_Portuak,
            o.Kolorea AS Ordenagailu_Kolorea,
            i.Kolorea AS Imprimagailu_Kolorea
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

                    // Agregar columna combinada para características específicas
                    table.Columns.Add("Ezaugarriak", typeof(string));
                    foreach (DataRow row in table.Rows)
                    {
                        switch (row["Gailu_Mota"].ToString())
                        {
                            case "Ordenagailuak":
                                row["Ezaugarriak"] = $"RAM: {row["Memoria_RAM"]}GB, GPU: {row["TxartelGrafikoa"]}, USB: {row["USB_Portuak"]}, Kolorea: {row["Ordenagailu_Kolorea"]}";
                                break;
                            case "Inprimagailuak":
                                row["Ezaugarriak"] = $"Kolorea: {row["Imprimagailu_Kolorea"]}";
                                break;
                            case "BesteGailuak":
                                string egoera = row["EgoeraGailua"].ToString();
                                row["Ezaugarriak"] = $"Egoera: {egoera}";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Devolvemos una tabla vacía en caso de error
                // Podrías también lanzar la excepción si prefieres manejar el error fuera
                Console.WriteLine("Error al obtener los gailuak: " + ex.Message);
            }

            return table; // Esta línea asegura que siempre se devuelve un valor
        }
        public void ActualizarGailua(int id, int idMintegia, string marka, string modeloa,
                                   DateTime erosketaData, bool ezabatzekoMarka, string egoeraGailua)
        {
            string updateQuery = @"UPDATE gailuak 
                           SET ID_Mintegia = @ID_Mintegia, 
                               Marka = @Marka, 
                               Modeloa = @Modeloa, 
                               Erosketa_data = @Erosketa_data, 
                               EzabatzekoMarka = @EzabatzekoMarka,
                               EgoeraGailua = @EgoeraGailua
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
                cmd.Parameters.AddWithValue("@Data_Ezabatu", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Marka", marka);
                cmd.Parameters.AddWithValue("@Modeloa", modeloa);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string ObtenerDatosGailua(int idGailuak)
        {
            string selectQuery = @"SELECT Marka, Modeloa 
                          FROM gailuak 
                          WHERE ID_Gailuak = @ID_Gailuak";

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
        public bool ExisteEnEzabatutakoGailuak(int idGailuak)
        {
            string query = "SELECT COUNT(*) FROM EzabatutakoGailuak WHERE ID_Gailuak = @ID_Gailuak";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ID_Gailuak", idGailuak);
                connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }
}