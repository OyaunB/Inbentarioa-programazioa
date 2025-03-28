using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class GailuakGehitu : Form
    {

        public GailuakGehitu()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Definir los colores del degradado usando códigos hexadecimales
            Color colorInicio = ColorTranslator.FromHtml("#5de0e6"); // Azul claro
            Color colorFin = ColorTranslator.FromHtml("#004aad");    // Azul oscuro

            // Crear un pincel con degradado lineal
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, // Área donde se aplicará el degradado
                colorInicio,         // Color inicial
                colorFin,            // Color final
                LinearGradientMode.Horizontal)) // Dirección del degradado (horizontal)
            {
                // Rellenar el fondo del formulario con el degradado
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void GailuakGehitu_Load(object sender, EventArgs e)
        {
            DBLana dblana = new DBLana();  // Crear una instancia de DBLana
            dblana.CargarDatos(dataGridViewGailuakGehitu);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }

        private void BtGehitu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btAldatu_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = null; // Hasieratu konexioa

            try
            {
                connection = DBKonexioa.Konektatu(); // Lortu konexioa
                connection.Open(); // Konexioa ireki

                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["ID"].Value != null) // Saihestu lerro hutsak
                    {
                        int id = Convert.ToInt32(row.Cells["ID"].Value);
                        int idMintegia = Convert.ToInt32(row.Cells["ID_Mintegia"].Value);
                        string marka = row.Cells["Marka"].Value.ToString();
                        string izena = row.Cells["Izena"].Value.ToString();
                        DateTime erosketadata = Convert.ToDateTime(row.Cells["ErosketaData"].Value);
                        bool egoera = Convert.ToBoolean(row.Cells["Egoera"].Value);

                        string query = "UPDATE gailuak SET ID_Mintegia = @ID_Mintegia, Marka = @Marka, Izena = @Izena, ErosketaData = @ErosketaData, Egoera = @Egoera WHERE ID = @ID";

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            cmd.Parameters.AddWithValue("@ID_Mintegia", idMintegia);
                            cmd.Parameters.AddWithValue("@Marka", marka);
                            cmd.Parameters.AddWithValue("@Izena", izena);
                            cmd.Parameters.AddWithValue("@ErosketaData", erosketadata);
                            cmd.Parameters.AddWithValue("@Egoera", egoera);

                            cmd.ExecuteNonQuery(); // SQL exekutatu
                        }
                    }
                }

                MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close(); // Konexioa itxi
                }
            }
        }

    }
}
