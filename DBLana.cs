using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Inbentarioa
{
    internal class DBLana
    {
        public void CargarDatos(DataGridView dgv)
        {
            try
            {
                using (MySqlConnection kon = DBKonexioa.Konektatu())
                {
                    string kontsulta = "SELECT * FROM Gailuak";
                    MySqlCommand komandoa = new MySqlCommand(kontsulta, kon);
                    using (MySqlDataReader irakurri = komandoa.ExecuteReader())
                    {
                        dgv.Rows.Clear();
                        dgv.Columns.Clear();
                        dgv.Columns.Add("ID_Gailuak", "ID");
                        dgv.Columns.Add("ID_Mintegia", "ID_Mintegia");
                        dgv.Columns.Add("Marka", "Marka");
                        dgv.Columns.Add("Izena", "Izena");
                        dgv.Columns.Add("Erosketa_data", "ErosketaData");
                        dgv.Columns.Add("Egoera", "Egoera");

                        while (irakurri.Read())
                        {
                            dgv.Rows.Add(
                                irakurri["ID_Gailuak"],
                                irakurri["ID_Mintegia"],
                                irakurri["Marka"],
                                irakurri["Izena"],
                                irakurri["Erosketa_data"],
                                irakurri["Egoera"]
                            );
                        }
                    }
                }
                DBKonexioa.ItxiKonexioa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message);
            }
        }
    }
}
