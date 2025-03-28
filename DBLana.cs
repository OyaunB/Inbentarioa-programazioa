using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                        // Limpia el DataGridView
                        dgv.Rows.Clear();
                        dgv.Columns.Clear();

                        // Configura las columnas según los campos de la consulta
                        // Por ejemplo, supongamos que la tabla tiene las columnas ID_Gailuak, Marka, Izena, etc.
                        dgv.Columns.Add("ID_Gailuak", "ID");
                        dgv.Columns.Add("ID_Mintegia", "ID_Mintegia");
                        dgv.Columns.Add("Marka", "Marka");
                        dgv.Columns.Add("Izena", "Izena");
                        dgv.Columns.Add("Erosketa_data", "ErosketaData");
                        dgv.Columns.Add("Egoera", "Egoera");

                        // Lee cada registro y agrégalo al DataGridView
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
