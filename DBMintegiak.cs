//DBMintegiak.cs
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Inbentarioa
{
    internal class DBMintegiak
    {
        private string konekzioString = "server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;";

        // 🔹 Obtener todos los almacenes
        public DataTable LortuMintegiak()
        {
            DataTable dt = new DataTable();
           

            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Mintegiak";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak lortzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }



        // 🔹 Añadir nuevo almacén
        // 🔹 Añadir nuevo almacén sin especificar ID
        public bool GehituMintegia(string izena, string kokapena)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "INSERT INTO Mintegiak (Izena, Kokapena) VALUES (@izena, @kokapena)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@izena", izena);
                        cmd.Parameters.AddWithValue("@kokapena", kokapena);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegia gehitzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // 🔹 Actualizar almacén existente
        public bool EguneratuMintegia(int id, string izena, string kokapena)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "UPDATE Mintegiak SET Izena = @izena, Kokapena = @kokapena WHERE ID_Mintegia = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@izena", izena);
                        cmd.Parameters.AddWithValue("@kokapena", kokapena);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegia eguneratzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 🔹 Eliminar almacén
        public bool EzabatuMintegia(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();

                    // Verificar si hay registros dependientes en gailuak
                    string checkQuery = "SELECT COUNT(*) FROM gailuak WHERE ID_Mintegia = @id";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@id", id);
                        int dependentCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (dependentCount > 0)
                        {
                            MessageBox.Show($"Ezin da mintegia ezabatu, {dependentCount} gailu daude erregistratuta ID_Mintegia honekin.",
                                            "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Si no hay dependencias, eliminar el registro de Mintegiak
                    string deleteQuery = "DELETE FROM Mintegiak WHERE ID_Mintegia = @id";
                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int affectedRows = cmd.ExecuteNonQuery();

                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegia ezabatzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // 🔹 Obtener el último ID y sumarle 1
        public int LortuHurrengoID()
        {
            int nextId = 1; // Si no hay registros, empezamos en 1

            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "SELECT MAX(ID_Mintegia) FROM Mintegiak";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            nextId = Convert.ToInt32(result) + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea hurrengo ID-a lortzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nextId;
        }

      
    }
}