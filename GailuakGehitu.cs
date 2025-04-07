//GailuakGehitu.cs
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class GailuakGehitu : Form
    {
        private readonly string connectionString = "server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;";
        private readonly GailuakDAL gailuakDAL;

        public GailuakGehitu()
        {
            InitializeComponent();
            gailuakDAL = new GailuakDAL(connectionString);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color colorInicio = ColorTranslator.FromHtml("#5de0e6");
            Color colorFin = ColorTranslator.FromHtml("#004aad");

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                colorInicio,
                colorFin,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        private void CargarDatos()
        {
            try
            {
                // Obtener los datos de la base de datos
                DataTable table = gailuakDAL.ObtenerTodosGailuak();

                // Asegurar que las columnas se generen automáticamente
                dataGridViewGailuakGehitu.AutoGenerateColumns = true;

                // Asignar los datos al DataGridView primero
                dataGridViewGailuakGehitu.DataSource = table;

                // Ocultar la columna original "Ezabatuta" (la que viene de la DB)
                if (dataGridViewGailuakGehitu.Columns.Contains("Ezabatuta"))
                {
                    dataGridViewGailuakGehitu.Columns["Ezabatuta"].Visible = false;
                }

                // Verificar si ya existe la columna de checkbox antes de agregarla
                if (!dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka"))
                {
                    var checkBoxColumn = new DataGridViewCheckBoxColumn
                    {
                        Name = "EzabatzekoMarka",
                        HeaderText = "Ezabatuta",
                        ReadOnly = true,
                        FalseValue = false,
                        TrueValue = true
                    };

                    dataGridViewGailuakGehitu.Columns.Add(checkBoxColumn);
                }

                // Rellenar la columna checkbox en base a "Ezabatuta"
                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["Ezabatuta"].Value != DBNull.Value)
                    {
                        bool estaEliminado = Convert.ToInt32(row.Cells["Ezabatuta"].Value) == 1;
                        row.Cells["EzabatzekoMarka"].Value = estaEliminado;
                    }
                }

                dataGridViewGailuakGehitu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void GailuakGehitu_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Manejo del evento click del label si es necesario
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
            aukeraAutatzeko f7 = new aukeraAutatzeko();
            f7.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Manejo del evento click de la celda si es necesario
        }
        private void btAldatu_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                            {
                                if (row.Cells["ID"].Value != null)
                                {
                                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                                    bool estaEliminado = Convert.ToInt32(row.Cells["EstaEliminado"].Value) == 1;
                                    bool marcadoParaEliminar = Convert.ToBoolean(row.Cells["EzabatzekoMarka"].Value);

                                    // --- Actualizar TODOS los campos editables en Gailuak ---
                                    gailuakDAL.ActualizarGailua(
                                        id,
                                        Convert.ToInt32(row.Cells["ID_Mintegia"].Value),
                                        row.Cells["Marka"].Value?.ToString() ?? "",
                                        row.Cells["Izena"].Value?.ToString() ?? "",
                                        Convert.ToDateTime(row.Cells["ErosketaData"].Value),
                                        marcadoParaEliminar,
                                        NormalizarEgoeraGailua(row.Cells["EgoeraGailua"].Value?.ToString())
                                    );

                                    // --- Mover a EzabatutakoGailuak si está marcado y no estaba eliminado ---
                                    if (!estaEliminado && marcadoParaEliminar)
                                    {
                                        string datosGailua = gailuakDAL.ObtenerDatosGailua(id);
                                        if (datosGailua != null)
                                        {
                                            string[] partes = datosGailua.Split('|');
                                            gailuakDAL.MoverAEzabatutakoGailuak(id, partes[0], partes[1]);
                                        }
                                    }
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos(); // Recargar para reflejar cambios
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string NormalizarEgoeraGailua(string egoeraGailua)
        {
            var estadosValidos = new[] { "Ongi", "Apurtuta", "Kompontzen" };
            if (!estadosValidos.Any(e => e.Equals(egoeraGailua, StringComparison.OrdinalIgnoreCase)))
            {
                return "Ongi"; // Valor por defecto
                               // Formatea con primera letra en mayúscula
                return char.ToUpper(egoeraGailua[0]) + egoeraGailua.Substring(1).ToLower();

            }
            

            return char.ToUpper(egoeraGailua[0]) + egoeraGailua.Substring(1).ToLower();
        }
    }
}