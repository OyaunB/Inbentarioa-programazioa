using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Inbentarioa
{
    internal class DBErabiltzaileak
    {
        private string konekzioString = "server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;";

        public DataTable LortuErabiltzaileak()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Erabiltzaileak";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak lortzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public bool GehituErabiltzailea(int id, string izena, string errola, string erabiltzailea)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    // Asegúrate de que el nombre de la columna coincida con tu tabla (ej: ErabiltzaileIzena)
                    string query = "INSERT INTO Erabiltzaileak (ID_Erabiltzaileak, Izena, Errola, ErabiltzaileIzena) VALUES (@id, @izena, @errola, @erabiltzailea)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@izena", izena);
                        cmd.Parameters.AddWithValue("@errola", errola);
                        cmd.Parameters.AddWithValue("@erabiltzailea", erabiltzailea);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea erabiltzailea gehitzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool EguneratuErabiltzailea(int id, string izena, string errola, string erabiltzailea)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "UPDATE Erabiltzaileak SET Izena = @izena, Errola = @errola WHERE ID_Erabiltzaileak = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@izena", izena);
                        cmd.Parameters.AddWithValue("@errola", errola);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea erabiltzailea eguneratzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool GordeErabiltzaileaFitxategian(string erabiltzaileaAntzinakoa, string erabiltzaileaBerria, string pasahitza, string errola, bool gehitu)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Erabiltzaileak.txt");
            List<string> lines = new List<string>();

            try
            {
                // Leer todas las líneas del archivo si existe
                if (File.Exists(filePath))
                {
                    lines = File.ReadAllLines(filePath).ToList();
                }

                // Buscar si el usuario antiguo existe en el archivo
                int index = lines.FindIndex(line => line.StartsWith(erabiltzaileaAntzinakoa + ";"));

                if (gehitu)
                {
                    if (index == -1)
                    {
                        // Si no existe, añadir como nuevo usuario
                        lines.Add($"{erabiltzaileaBerria};{pasahitza};{errola}");
                    }
                    else
                    {
                        // Si existe, actualizar la línea con el nuevo nombre
                        lines[index] = $"{erabiltzaileaBerria};{pasahitza};{errola}";
                    }
                }
                else if (index != -1)
                {
                    // Si estamos eliminando, quitar la línea
                    lines.RemoveAt(index);
                }

                // Guardar cambios en el archivo
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea fitxategia eguneratzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool EzabatuErabiltzailea(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "DELETE FROM Erabiltzaileak WHERE ID_Erabiltzaileak = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea erabiltzailea ezabatzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
