﻿// DBGailuak.cs
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
            m.Izena AS Mintegi_Izena,
            g.Marka,
            g.Modeloa AS Modeloa,
            g.Erosketa_data AS Erosketa_Data,
            g.EgoeraGailua AS EgoeraGailua,
            o.Memoria_RAM,
            o.TxartelGrafikoa,
            o.USB_Portuak
        FROM Gailuak g
        LEFT JOIN Mintegiak m ON g.ID_Mintegia = m.ID_Mintegia
        LEFT JOIN Ordenagailuak o ON g.ID_Gailuak = o.ID_Gailuak AND g.Gailu_Mota = 'Ordenagailuak'
        LEFT JOIN Imprimagailuak i ON g.ID_Gailuak = i.ID_Gailuak AND g.Gailu_Mota = 'Inprimagailuak'
        LEFT JOIN BesteGailuak b ON g.ID_Gailuak = b.ID_Gailuak AND g.Gailu_Mota = 'BesteGailuak'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
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

        //LortuMintegiarenIDa
        public static int LortuMintegiarenID(string izena)
        {
            int id = 0;
            string connectionString = DBKonexioa.GetConnectionString();  // Asumiendo que tienes este método
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID_Mintegia FROM Mintegiak WHERE Izena = @izena";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@izena", izena);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        id = Convert.ToInt32(result);
                    }
                }
            }
            return id;
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
                        throw new Exception("Error al eliminar el gailua");
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
        public GailuakDAL()
        {
            MySqlConnection Konektatu = DBKonexioa.Konektatu(); 
        }
        public bool GehituBesteGailua(int idMintegia, string marka, string modeloa, string egoera)
        {
            bool success = false;
            MySqlConnection konekzioa = null;

            try
            {
                konekzioa = new MySqlConnection(connectionString);
                konekzioa.Open();

                // 1. ID berria kalkulatu
                int nuevoId;
                string queryId = "SELECT IFNULL(MAX(ID_Gailuak), 0) + 1 FROM Gailuak";
                using (MySqlCommand cmdId = new MySqlCommand(queryId, konekzioa))
                {
                    nuevoId = Convert.ToInt32(cmdId.ExecuteScalar());
                }

                // 2. Gailuak taulan sartu BALIO GUZTIEKIN
                DateTime gaurkoData = DateTime.Now;

                // 3. Gailuak taulan sartu
                string queryGailuak = @"INSERT INTO Gailuak 
            (ID_Gailuak, Gailu_Mota, Marka, Modeloa, Erosketa_data, EgoeraGailua, ID_Mintegia) 
            VALUES 
            (@id, 'BesteGailuak', @marka, @modeloa, @data, @egoera, @idMintegia)"; // ID_Mintegia gehituta
                using (MySqlCommand cmdGailuak = new MySqlCommand(queryGailuak, konekzioa))
                {
                    cmdGailuak.Parameters.AddWithValue("@id", nuevoId);
                    cmdGailuak.Parameters.AddWithValue("@marka", marka);
                    cmdGailuak.Parameters.AddWithValue("@modeloa", modeloa);
                    cmdGailuak.Parameters.AddWithValue("@data", gaurkoData);
                    cmdGailuak.Parameters.AddWithValue("@egoera", egoera);
                    cmdGailuak.Parameters.AddWithValue("@idMintegia", idMintegia); // ID_Mintegia ere sartzen da
                    cmdGailuak.ExecuteNonQuery();
                }

                // 4. BesteGailuak taulan sartu
                string queryBesteGailuak = "INSERT INTO BesteGailuak (ID_Gailuak, Marka, Modeloa) VALUES (@id, @marka, @modeloa)";
                using (MySqlCommand cmdBeste = new MySqlCommand(queryBesteGailuak, konekzioa))
                {
                    cmdBeste.Parameters.AddWithValue("@id", nuevoId);
                    cmdBeste.Parameters.AddWithValue("@marka", marka);
                    cmdBeste.Parameters.AddWithValue("@modeloa", modeloa);
                    cmdBeste.ExecuteNonQuery();
                }

                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Errorea");
                throw;
            }
            finally
            {
                konekzioa?.Close();
            }

            return success;
        }
        // GEHITU INPRIMAGAILUAK GEHITZEKO METODOA
        public bool GehituImprimagailua(int mintegiId, string marka, string modeloa, string egoera)
        {
            bool success = false;
            MySqlConnection konekzioa = null;
            MySqlTransaction transakzioa = null;

            try
            {
                konekzioa = DBKonexioa.Konektatu();
                transakzioa = konekzioa.BeginTransaction();

                // 1. Verificar que existe el mintegia
                string checkMintegia = "SELECT COUNT(*) FROM Mintegiak WHERE ID_Mintegia = @MintegiId";
                using (MySqlCommand cmdCheck = new MySqlCommand(checkMintegia, konekzioa, transakzioa))
                {
                    cmdCheck.Parameters.AddWithValue("@MintegiId", mintegiId);
                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (count == 0)
                    {
                        throw new Exception($"Ez da mintegi hori existitzen (ID: {mintegiId})");
                    }
                }

                // 2. Insertar en Gailuak (ahora con ID_Mintegia)
                string queryGailuak = @"INSERT INTO Gailuak 
                    (Gailu_Mota, ID_Mintegia, Marka, Modeloa, Erosketa_data, EgoeraGailua) 
                    VALUES ('Imprimagailuak', @mintegiId, @marka, @modeloa, @data, @egoera)";

                using (MySqlCommand cmdGailuak = new MySqlCommand(queryGailuak, konekzioa, transakzioa))
                {
                    cmdGailuak.Parameters.AddWithValue("@mintegiId", mintegiId);
                    cmdGailuak.Parameters.AddWithValue("@marka", marka);
                    cmdGailuak.Parameters.AddWithValue("@modeloa", modeloa);
                    cmdGailuak.Parameters.AddWithValue("@data", DateTime.Now);
                    cmdGailuak.Parameters.AddWithValue("@egoera", egoera);
                    cmdGailuak.ExecuteNonQuery();
                }

                // 3. Obtener el ID recién insertado
                long azkenId;
                using (MySqlCommand cmdId = new MySqlCommand("SELECT LAST_INSERT_ID();", konekzioa, transakzioa))
                {
                    azkenId = Convert.ToInt64(cmdId.ExecuteScalar());
                }

                // 4. Insertar en Imprimagailuak
                string queryImprimagailuak = @"INSERT INTO Imprimagailuak 
                            (ID_Gailuak, Marka, Modeloa) 
                            VALUES (@id, @marka, @modeloa)";

                using (MySqlCommand cmdImprimagailuak = new MySqlCommand(queryImprimagailuak, konekzioa, transakzioa))
                {
                    cmdImprimagailuak.Parameters.AddWithValue("@id", azkenId);
                    cmdImprimagailuak.Parameters.AddWithValue("@marka", marka);
                    cmdImprimagailuak.Parameters.AddWithValue("@modeloa", modeloa);
                    cmdImprimagailuak.ExecuteNonQuery();
                }

                transakzioa.Commit();
                success = true;
            }
            catch (MySqlException ex)
            {
                transakzioa?.Rollback();
                Console.WriteLine("MySQL errorea: " + ex.Message);
                throw new Exception("Errorea datu-basean: " + ex.Message);
            }
            catch (Exception ex)
            {
                transakzioa?.Rollback();
                Console.WriteLine("Errorea: " + ex.Message);
                throw;
            }
            finally
            {
                DBKonexioa.ItxiKonexioa();
            }

            return success;
        }

        // Clase para obtener 'Mintegiak y usarlo para cargar el nombre
        public DataTable ObtenerMintegiak()
        {
            DataTable table = new DataTable();
            string query = "SELECT ID_Mintegia, Izena FROM Mintegiak";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, DBKonexioa.Konektatu()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errorea mintegiak lortzerakoan: " + ex.Message);
            }
            finally
            {
                DBKonexioa.ItxiKonexioa(); 
            }

            return table;
        }

        //Ordenagailuak gehitzeko=
        // Ordenagailuak gehitzeko funtzioa
        public void OrdenagailuakGehitu(string kodea, string marka, string modeloa, string sistemaEragilea, string prozesadorea, string ram, string ssd, string hdd, string pantaila, DateTime erosketaData, string egoera, string ikasgela, string gelaKokapena)
        {
            string connectionString = "server=localhost;user=root;database=gailuak;port=3306;password=";

            using (MySqlConnection konexioa = new MySqlConnection(connectionString))
            {
                try
                {
                    konexioa.Open();

                    string kontsulta = "INSERT INTO ordenagailuak (Kodea, Marka, Modeloa, Sistema_eragilea, Prozesadorea, RAM, SSD, HDD, Pantaila, Erosketa_data, Egoera, Ikasgela, Gela_kokapena) " +
                                       "VALUES (@kodea, @marka, @modeloa, @sistema_eragilea, @prozesadorea, @ram, @ssd, @hdd, @pantaila, @erosketa_data, @egoera, @ikasgela, @gela_kokapena)";

                    using (MySqlCommand command = new MySqlCommand(kontsulta, konexioa))
                    {
                        // Parametroak gehitu
                        command.Parameters.AddWithValue("@kodea", kodea);
                        command.Parameters.AddWithValue("@marka", marka);
                        command.Parameters.AddWithValue("@modeloa", modeloa);
                        command.Parameters.AddWithValue("@sistema_eragilea", sistemaEragilea);
                        command.Parameters.AddWithValue("@prozesadorea", prozesadorea);
                        command.Parameters.AddWithValue("@ram", ram);
                        command.Parameters.AddWithValue("@ssd", ssd);
                        command.Parameters.AddWithValue("@hdd", hdd);
                        command.Parameters.AddWithValue("@pantaila", pantaila);
                        command.Parameters.AddWithValue("@erosketa_data", erosketaData); // DateTime zuzenean
                        command.Parameters.AddWithValue("@egoera", egoera);
                        command.Parameters.AddWithValue("@ikasgela", ikasgela);
                        command.Parameters.AddWithValue("@gela_kokapena", gelaKokapena);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea datuak gehitzean: " + ex.Message);
                }
            }
        }


        public DataTable ObtenerTodosMintegiak()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID_Mintegia", typeof(int));
            table.Columns.Add("Izena", typeof(string));
            string query = "SELECT ID_Mintegia, Izena FROM Mintegiak";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, DBKonexioa.Konektatu()))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errorea mintegiak lortzerakoan: " + ex.Message);
                throw; // Re-lanzar la excepción para manejo superior
            }
            finally
            {
                DBKonexioa.ItxiKonexioa();
            }

            return table;
        }

        public DataTable ObtenerGailuakPorMintegia(int idMintegia)
        {
            DataTable table = new DataTable();

            string query = @"
        SELECT 
            g.ID_Gailuak AS ID,
            g.Gailu_Mota AS Gailu_Mota,
            m.Izena AS Mintegi_Izena,
            g.Marka,
            g.Modeloa AS Modeloa,
            g.Erosketa_data AS Erosketa_Data,
            g.EgoeraGailua AS EgoeraGailua,
            o.Memoria_RAM,
            o.TxartelGrafikoa,
            o.USB_Portuak
        FROM Gailuak g
        LEFT JOIN Mintegiak m ON g.ID_Mintegia = m.ID_Mintegia
        LEFT JOIN Ordenagailuak o ON g.ID_Gailuak = o.ID_Gailuak AND g.Gailu_Mota = 'Ordenagailuak'
        LEFT JOIN Imprimagailuak i ON g.ID_Gailuak = i.ID_Gailuak AND g.Gailu_Mota = 'Inprimagailuak'
        LEFT JOIN BesteGailuak b ON g.ID_Gailuak = b.ID_Gailuak AND g.Gailu_Mota = 'BesteGailuak'
        WHERE g.ID_Mintegia = @idMintegia";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMintegia", idMintegia);
                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

    }
}