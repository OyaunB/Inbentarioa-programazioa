using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class DBOrdenagailuakGehitu
{
   
    public static void OrdenagailuaGehitu(string mintegiKodea, string marka, string modeloa,
        string erosketadata, string txartela, string ram, string usb, string kolorea, string egoera)
    {
        string konexioaKatea = "server=localhost;user=root;database=inbentarioa;password=;";

        using (MySqlConnection konexioa = new MySqlConnection(konexioaKatea))
        {
            try
            {
                konexioa.Open();

                // 1. Egiaztatu Mintegia existitzen dela
                string kontsultaMintegia = "SELECT COUNT(*) FROM Mintegiak WHERE ID_Mintegia = @Mintegia";
                using (MySqlCommand cmdCheck = new MySqlCommand(kontsultaMintegia, konexioa))
                {
                    cmdCheck.Parameters.AddWithValue("@Mintegia", mintegiKodea);
                    int kopurua = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (kopurua == 0)
                    {
                        MessageBox.Show("Ez da mintegi hori existitzen (ID: " + mintegiKodea + ").", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 2. Gailuak sartu
                string insertGailuak = @"INSERT INTO Gailuak 
                    (Gailu_Mota, ID_Mintegia, Marka, Modeloa, Erosketa_data, EgoeraGailua) 
                    VALUES 
                    ('Ordenagailuak', @Mintegia, @Marka, @Modeloa, @ErosketaData, @Egoera)";

                using (MySqlCommand cmd1 = new MySqlCommand(insertGailuak, konexioa))
                {
                    cmd1.Parameters.AddWithValue("@Mintegia", mintegiKodea);
                    cmd1.Parameters.AddWithValue("@Marka", marka);
                    cmd1.Parameters.AddWithValue("@Modeloa", modeloa);
                    cmd1.Parameters.AddWithValue("@ErosketaData", Convert.ToDateTime(erosketadata));
                    cmd1.Parameters.AddWithValue("@Egoera", egoera);
                    cmd1.ExecuteNonQuery();
                }

                // 3. Lortu azken sartutako ID_Gailuak
                long azkenId = cmdLastInsertId(konexioa);

                // 4. Ordenagailuak sartu
                string insertOrdenagailuak = @"INSERT INTO Ordenagailuak 
                    (ID_Gailuak, Memoria_RAM, TxartelGrafikoa, USB_Portuak, Kolorea, Marka, Modeloa)
                    VALUES
                    (@ID, @RAM, @Txartela, @USB, @Kolorea, @Marka, @Modeloa)";

                using (MySqlCommand cmd2 = new MySqlCommand(insertOrdenagailuak, konexioa))
                {
                    cmd2.Parameters.AddWithValue("@ID", azkenId);
                    cmd2.Parameters.AddWithValue("@RAM", int.Parse(ram));
                    cmd2.Parameters.AddWithValue("@Txartela", txartela);
                    cmd2.Parameters.AddWithValue("@USB", int.Parse(usb));
                    cmd2.Parameters.AddWithValue("@Kolorea", kolorea);
                    cmd2.Parameters.AddWithValue("@Marka", marka);
                    cmd2.Parameters.AddWithValue("@Modeloa", modeloa);
                    cmd2.ExecuteNonQuery();
                }

                MessageBox.Show("Ordenagailua ondo gehitu da!", "✅", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea: " + ex.Message, "❌", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

 
    private static long cmdLastInsertId(MySqlConnection konexioa)
    {
        using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID();", konexioa))
        {
            return (long)cmd.ExecuteScalar();
        }
    }

}
