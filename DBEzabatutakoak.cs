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
                string query = "SELECT ID_Gailuak, Data_Ezabatu, Izena FROM EzabatutakoGailuak"; // Especifica las columnas en lugar de usar *
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
            // Específico para errores de MySQL
            Console.WriteLine("MySQL errorea datuak lortzerakoan: " + ex.Message);
            throw; // Re-lanzar la excepción para manejarla en el formulario
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errorea datuak lortzerakoan: " + ex.Message);
            throw; // Re-lanzar la excepción para manejarla en el formulario
        }
        return dt;
    }
}