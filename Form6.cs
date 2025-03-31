using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        // PANTALLA KOLOREZTATZEKO
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Color colorInicio = ColorTranslator.FromHtml("#5de0e6");
            Color colorFin = ColorTranslator.FromHtml("#004aad");

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, colorInicio, colorFin, LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CargarDatos(); // 🔹 Carga los datos al abrir el formulario
        }

        private void CargarDatos()
        {
            try
            {
                string konekzioString = "server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;";

                using (MySqlConnection connection = new MySqlConnection(konekzioString))
                {
                    connection.Open(); // 🔹 Abrir conexión aquí, no en DBKonexioa.Konektatu()

                    string query = "SELECT * FROM Erabiltzaileak"; // Consulta SQL

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridViewErabiltzailea.DataSource = null;
                        dataGridViewErabiltzailea.DataSource = dt; // Mostrar datos en DataGridView
                    }
                } // 🔹 Konexioa automatikoki ixten da hemen
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos(); // 🔹 Botón para actualizar los datos sin cerrar la ventana
        }
    }
}
