using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class GailuakGehitu : Form
    {
        private readonly GailuakDAL gailuakDAL;

        public GailuakGehitu()
        {
            InitializeComponent();
            gailuakDAL = new GailuakDAL("server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;");
            dataGridViewGailuakGehitu.CellClick += dataGridViewGailuakGehitu_CellClick;
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

        private void GailuakGehitu_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDatos();
        }

        private void ConfigurarDataGridView()
        {
            // Configuración general
            dataGridViewGailuakGehitu.AllowUserToAddRows = false;
            dataGridViewGailuakGehitu.AllowUserToDeleteRows = false;
            dataGridViewGailuakGehitu.MultiSelect = false;
            dataGridViewGailuakGehitu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewGailuakGehitu.RowHeadersVisible = false;
            dataGridViewGailuakGehitu.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridViewGailuakGehitu.DefaultCellStyle.SelectionForeColor = Color.Black;

            //NO establecemos ReadOnly = true aquí, lo haremos por columna
        }
        private void dataGridViewGailuakGehitu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Permitir edición solo en las columnas específicas al hacer clic
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dataGridViewGailuakGehitu.Columns[e.ColumnIndex].Name;

                if (columnName == "EgoeraGailua" || columnName == "EzabatzekoMarka")
                {
                    dataGridViewGailuakGehitu.BeginEdit(true);
                }
            }
        }
        private void CargarDatos()
        {
            try
            {
                DataTable table = gailuakDAL.ObtenerTodosGailuak();
                //__________________________
                dataGridViewGailuakGehitu.DataSource = table;

                // Asegurar que EgoeraGailua no sea NULL
                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["EgoeraGailua"].Value == null || row.Cells["EgoeraGailua"].Value == DBNull.Value)
                    {
                        row.Cells["EgoeraGailua"].Value = "Ongi"; // Valor por defecto
                    }
                }
                //__________________________
                // Configurar columna ComboBox para EgoeraGailua
                if (dataGridViewGailuakGehitu.Columns.Contains("EgoeraGailua"))
                {
                    dataGridViewGailuakGehitu.Columns.Remove("EgoeraGailua");
                }

                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                comboBoxColumn.Name = "EgoeraGailua";
                comboBoxColumn.HeaderText = "Egoera Gailua";
                comboBoxColumn.DataPropertyName = "EgoeraGailua";
                comboBoxColumn.Items.AddRange("Ongi", "Apurtuta", "Kompontzen");
                dataGridViewGailuakGehitu.Columns.Add(comboBoxColumn);

                dataGridViewGailuakGehitu.DataSource = table;

                // Configurar columnas especiales
                ConfigurarColumnasEditables();

                // Asegurarse de que las columnas especiales son visibles
                if (dataGridViewGailuakGehitu.Columns.Contains("EgoeraGailua"))
                {
                    dataGridViewGailuakGehitu.Columns["EgoeraGailua"].Visible = true;
                }

                if (dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka"))
                {
                    dataGridViewGailuakGehitu.Columns["EzabatzekoMarka"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigurarColumnasEditables()
        {
            // Hacer todas las columnas NO editables por defecto
            foreach (DataGridViewColumn column in dataGridViewGailuakGehitu.Columns)
            {
                column.ReadOnly = true;
            }

            // Configurar columnas editables
            if (dataGridViewGailuakGehitu.Columns.Contains("EgoeraGailua"))
            {
                dataGridViewGailuakGehitu.Columns["EgoeraGailua"].ReadOnly = false;
            }

            // Configurar columna EzabatzekoMarka
            if (!dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka"))
            {
                var checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "EzabatzekoMarka",
                    HeaderText = "Ezabatuta",
                    ReadOnly = false
                };
                dataGridViewGailuakGehitu.Columns.Add(checkBoxColumn);
            }
            else
            {
                dataGridViewGailuakGehitu.Columns["EzabatzekoMarka"].ReadOnly = false;
            }

            // Rellenar la columna checkbox si existe columna Ezabatuta
            if (dataGridViewGailuakGehitu.Columns.Contains("Ezabatuta"))
            {
                dataGridViewGailuakGehitu.Columns["Ezabatuta"].Visible = false;

                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["Ezabatuta"].Value != DBNull.Value)
                    {
                        bool ezabatzekoMarka = Convert.ToInt32(row.Cells["Ezabatuta"].Value) == 1;
                        row.Cells["EzabatzekoMarka"].Value = ezabatzekoMarka;
                    }
                }
            }
        }

        private void ConfigurarColumnasEspeciales()
        {
            // Configurar columna EzabatzekoMarka
            if (dataGridViewGailuakGehitu.Columns.Contains("Ezabatuta"))
            {
                dataGridViewGailuakGehitu.Columns["Ezabatuta"].Visible = false;

                if (!dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka"))
                {
                    var checkBoxColumn = new DataGridViewCheckBoxColumn
                    {
                        Name = "EzabatzekoMarka",
                        HeaderText = "Ezabatuta",
                        ReadOnly = false,
                        FalseValue = false,
                        TrueValue = true
                    };
                    dataGridViewGailuakGehitu.Columns.Add(checkBoxColumn);
                }

                // Rellenar la columna checkbox
                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["Ezabatuta"].Value != DBNull.Value)
                    {
                        bool ezabatzekoMarka = Convert.ToInt32(row.Cells["Ezabatuta"].Value) == 1;
                        row.Cells["EzabatzekoMarka"].Value = ezabatzekoMarka;
                    }
                }
            }

            // Configurar qué columnas son editables
            foreach (DataGridViewColumn column in dataGridViewGailuakGehitu.Columns)
            {
                column.ReadOnly = !(column.Name == "EzabatzekoMarka" || column.Name == "EgoeraGailua");
            }
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Aukerak().ShowDialog();
        }

        private void BtGehitu_Click(object sender, EventArgs e)
        {
            this.Hide();
            new aukeraAutatzeko().ShowDialog();
        }
        private void btAldatu_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["ID"].Value != null && row.Cells["ID"].Value != DBNull.Value)
                    {
                        int id = Convert.ToInt32(row.Cells["ID"].Value);

                        // Obtener EgoeraGailua (manejar DBNull)
                        string egoeraGailua = "Ongi"; // Valor por defecto
                        if (row.Cells["EgoeraGailua"].Value != null && row.Cells["EgoeraGailua"].Value != DBNull.Value)
                        {
                            egoeraGailua = row.Cells["EgoeraGailua"].Value.ToString();
                        }

                        // Obtener EzabatzekoMarka (manejar DBNull)
                        bool ezabatzekoMarka = false;
                        if (dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka") &&
                            row.Cells["EzabatzekoMarka"].Value != null &&
                            row.Cells["EzabatzekoMarka"].Value != DBNull.Value)
                        {
                            ezabatzekoMarka = Convert.ToBoolean(row.Cells["EzabatzekoMarka"].Value);
                        }

                        // Obtener ID_Mintegia (manejar DBNull si es necesario)
                        int idMintegia = 0;
                        if (row.Cells["ID_Mintegia"].Value != null && row.Cells["ID_Mintegia"].Value != DBNull.Value)
                        {
                            idMintegia = Convert.ToInt32(row.Cells["ID_Mintegia"].Value);
                        }

                        // Obtener Erosketa_Data (manejar DBNull si es necesario)
                        DateTime erosketaData = DateTime.Now;
                        if (row.Cells["Erosketa_Data"].Value != null && row.Cells["Erosketa_Data"].Value != DBNull.Value)
                        {
                            erosketaData = Convert.ToDateTime(row.Cells["Erosketa_Data"].Value);
                        }

                        // Actualizar en la base de datos
                        gailuakDAL.ActualizarGailua(
                            id,
                            idMintegia,
                            row.Cells["Marka"].Value?.ToString() ?? "", // Usar operador ?. para manejar null
                            row.Cells["Modeloa"].Value?.ToString() ?? "",
                            erosketaData,
                            ezabatzekoMarka,
                            egoeraGailua
                        );
                    }
                }
                MessageBox.Show("Aldaketak ondo gorde dira.", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos(); // Recargar para reflejar cambios
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea aldaketak gordetzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEzabatu_Click(object sender, EventArgs e)
        {
            if (dataGridViewGailuakGehitu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Mesedez, hautatu gailu bat ezabatzeko.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Ziur zaude gailu hau ezabatu nahi duzula?", "Kontuz",
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int idGailua = Convert.ToInt32(dataGridViewGailuakGehitu.SelectedRows[0].Cells["ID"].Value);
                    gailuakDAL.EliminarGailuaCompleto(idGailua);
                    MessageBox.Show("Gailua ezabatu da.", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errorea ezabatzerakoan: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}