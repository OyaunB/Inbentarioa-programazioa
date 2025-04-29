//DBErabiltzaileak.cs
using System;
using System.IO;
using System.Linq;
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
        public bool GehituErabiltzailea(int id, string izena, string errola, string erabiltzailea, string pasahitza) 
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = @"INSERT INTO Erabiltzaileak 
                            (ID_Erabiltzaileak, Izena, Errola, ErabiltzaileIzena, ErabiltzailePasahitza) 
                            VALUES (@id, @izena, @errola, @erabiltzailea, @pasahitza)"; // 🔹 Añade el campo

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@izena", izena);
                        cmd.Parameters.AddWithValue("@errola", errola);
                        cmd.Parameters.AddWithValue("@erabiltzailea", erabiltzailea);
                        cmd.Parameters.AddWithValue("@pasahitza", pasahitza); // 🔹 Pasa el valor
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
                    string query = "UPDATE Erabiltzaileak SET Izena = @izena, Errola = @errola, ErabiltzaileIzena = @erabiltzailea WHERE ID_Erabiltzaileak = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@izena", izena);
                        cmd.Parameters.AddWithValue("@errola", errola);
                        cmd.Parameters.AddWithValue("@erabiltzailea", erabiltzailea);
                        int affectedRows = cmd.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea erabiltzailea eguneratzerakoan: " + ex.Message,
                               "Errorea",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public (bool, string, string) EgiaztatuErabiltzailea(string erabiltzaileIzena, string pasahitza)
        {
            try
            {
                using (MySqlConnection konexioa = new MySqlConnection(konekzioString))
                {
                    konexioa.Open();
                    string kontsulta = @"SELECT Izena, Errola, ErabiltzailePasahitza 
                                FROM Erabiltzaileak 
                                WHERE ErabiltzaileIzena = @erabiltzaileIzena";

                    using (MySqlCommand komandoa = new MySqlCommand(kontsulta, konexioa))
                    {
                        komandoa.Parameters.AddWithValue("@erabiltzaileIzena", erabiltzaileIzena);

                        using (MySqlDataReader irakurlea = komandoa.ExecuteReader())
                        {
                            if (irakurlea.Read())
                            {
                                string pasahitzaDB = irakurlea["ErabiltzailePasahitza"].ToString();

                                if (pasahitza == pasahitzaDB)
                                {
                                    return (true, irakurlea["Izena"].ToString(),
                                            irakurlea["Errola"].ToString());
                                }
                            }
                        }
                    }
                }
                return (false, null, null);
            }
            catch (Exception ex)
            {
                throw new Exception("Errorea erabiltzailea egiaztatzerakoan: ");
            }
        }
        public bool GordeErabiltzaileaFitxategian(string erabiltzaileaAntzinakoa, string erabiltzaileaBerria, string pasahitza, string errola, bool gehitu)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Erabiltzaileak.txt");

            try
            {
                List<string> lines = File.Exists(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();

                int index = lines.FindIndex(line => line.StartsWith(erabiltzaileaAntzinakoa + ";"));

                string nuevaLinea = $"{erabiltzaileaBerria};{pasahitza};{errola}";

                if (gehitu) 
                {
                    if (index != -1)
                        lines[index] = nuevaLinea;
                    else
                        lines.Add(nuevaLinea);
                }
                else // Eliminar
                {
                    if (index != -1)
                        lines.RemoveAt(index);
                }   

                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea fitxategia eguneratzerakoan");
                return false;
            }
        }
        public int LortuHurrengoId()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "SELECT IFNULL(MAX(ID_Erabiltzaileak), 0) + 1 FROM Erabiltzaileak";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea ID hurrengoa lortzerakoan: ");
                return -1; // O maneja el error de otra forma
            }
        }
        public List<string> LortuErrolak()
        {
            // Lista fija de roles oficiales (sin duplicados)
            List<string> errolak = new List<string>
    {
        "irakaslea",
        "ikt irakaslea", 
        "zuzendaria",
        "ordezkaria"
    };

            try
            {
                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Errola FROM Erabiltzaileak";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string errolaDb = reader["Errola"].ToString();

                                // Solo añadir si no existe ya en la lista
                                if (!errolak.Contains(errolaDb))
                                {
                                    errolak.Add(errolaDb);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea errolak lortzerakoan");
            }

            // Ordenar alfabéticamente antes de devolver
            errolak.Sort();
            return errolak;
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
