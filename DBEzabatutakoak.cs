using System;
using System.Data;
using MySql.Data.MySqlClient;

public class DBEzabatutakoak
{
    private string konekzioString = "Server=localhost;Database=inbentarioa;Uid=root;Pwd=root;";

    public DataTable LortuEzabatutakoGailuak()
    {
        DataTable dt = new DataTable();
        try
        {
            using (MySqlConnection konekzioa = new MySqlConnection(konekzioString))
            {
                konekzioa.Open();
                string query = "SELECT ID_Ezabatua, ID_Gailuak, Data_Ezabatu, Marka, Modeloa FROM EzabatutakoGailuak";
                using (MySqlCommand cmd = new MySqlCommand(query, konekzioa))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("MySQL errorea datuak lortzerakoan: " + ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errorea datuak lortzerakoan: " + ex.Message);
            throw;
        }
        return dt;
    }
}
