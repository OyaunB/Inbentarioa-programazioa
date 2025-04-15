//DBOrdenagailuaGehitu.cs
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Inbentarioa;
using MySql.Data.MySqlClient;

public class DBOrdenagailuakGehitu
{

    public bool GehituOrdenagailua(string mintegiKodea, string marka, string modeloa,
                               DateTime erosketaData, string txartelGrafikoa,
                               int ram, int usbPortuak, string egoera)
    {
        bool success = false;
        MySqlConnection konekzioa = null;
        MySqlTransaction transakzioa = null;

        try
        {
            konekzioa = DBKonexioa.Konektatu();
            transakzioa = konekzioa.BeginTransaction();

            // 1. Insertar en Gailuak
            string queryGailuak = @"INSERT INTO Gailuak 
                            (Gailu_Mota, ID_Mintegia, Marka, Modeloa, Erosketa_data, EgoeraGailua) 
                            VALUES ('Ordenagailuak', @mintegia, @marka, @modeloa, @erosketaData, @egoera)";

            using (MySqlCommand cmdGailuak = new MySqlCommand(queryGailuak, konekzioa, transakzioa))
            {
                cmdGailuak.Parameters.AddWithValue("@mintegia", mintegiKodea);
                cmdGailuak.Parameters.AddWithValue("@marka", marka);
                cmdGailuak.Parameters.AddWithValue("@modeloa", modeloa);
                cmdGailuak.Parameters.AddWithValue("@erosketaData", erosketaData);
                cmdGailuak.Parameters.AddWithValue("@egoera", egoera);
                cmdGailuak.ExecuteNonQuery();
            }

            // 2. Obtener el ID recién insertado
            long azkenId;
            using (MySqlCommand cmdId = new MySqlCommand("SELECT LAST_INSERT_ID();", konekzioa, transakzioa))
            {
                azkenId = Convert.ToInt64(cmdId.ExecuteScalar());
            }

            // 3. Insertar en Ordenagailuak
            string queryOrdenagailuak = @"INSERT INTO Ordenagailuak 
                                    (ID_Gailuak, Memoria_RAM, TxartelGrafikoa, USB_Portuak, Marka, Modeloa) 
                                    VALUES (@id, @ram, @txartela, @usb, @marka, @modeloa)";

            using (MySqlCommand cmdOrdenagailuak = new MySqlCommand(queryOrdenagailuak, konekzioa, transakzioa))
            {
                cmdOrdenagailuak.Parameters.AddWithValue("@id", azkenId);
                cmdOrdenagailuak.Parameters.AddWithValue("@ram", ram);
                cmdOrdenagailuak.Parameters.AddWithValue("@txartela", txartelGrafikoa);
                cmdOrdenagailuak.Parameters.AddWithValue("@usb", usbPortuak);
                cmdOrdenagailuak.Parameters.AddWithValue("@marka", marka);
                cmdOrdenagailuak.Parameters.AddWithValue("@modeloa", modeloa);
                cmdOrdenagailuak.ExecuteNonQuery();
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
            throw new Exception("Errorea: " + ex.Message);
        }
        finally
        {
            DBKonexioa.ItxiKonexioa();
        }

        return success;
    }

    private static long cmdLastInsertId(MySqlConnection konexioa)
    {
        using (MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID();", konexioa))
        {
            return (long)cmd.ExecuteScalar();
        }
    }

}
