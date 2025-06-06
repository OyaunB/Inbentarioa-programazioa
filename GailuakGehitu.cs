﻿//GailuakGehitu.cs
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//  En cualquier otro formulario donde necesites el rol del usuario, simplemente accede a:
//     string rolActual = Errola.ErabiltzaileRola;
namespace Inbentarioa
{
    public partial class GailuakGehitu : Form
    {
       
        private readonly GailuakDAL gailuakDAL;
        private DataTable todosLosGailuak;

        public GailuakGehitu()
        {
            InitializeComponent();
            gailuakDAL = new GailuakDAL("server=127.0.0.1;database=inbentarioa;uid=root;pwd=root;");
            dataGridViewGailuakGehitu.CellClick += dataGridViewGailuakGehitu_CellClick;

            // Configurar el ComboBox
            comboBoxMintegiak.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMintegiak.SelectedIndexChanged += comboBoxMintegiak_SelectedIndexChanged;
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
        {   //Mintegiak kargatzeko=
            ConfigurarDataGridView();
            CargarMintegiak(); // Cargar los mintegiak en el ComboBox
            CargarTodosLosGailuak(); // Cargar todos los gailuak inicialmente
            //_______________________
            ConfigurarDataGridView();
            CargarDatos();

            string rol = Errola.ErabiltzaileRola?.ToLower() ?? "";

            // Configurar botones según el rol
            btAtzera.Enabled = true; // Siempre permitir volver atrás
            BtGehitu.Enabled = (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt");
            btAldatu.Enabled = (rol == "irakaslea" || rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt");
            btEzabatu.Enabled = (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt"); // Irakaslea NO puede eliminar

            // Configurar edición del DataGridView
            bool permitirEdicion = (rol == "irakaslea" || rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt");
            foreach (DataGridViewColumn column in dataGridViewGailuakGehitu.Columns)
            {
                column.ReadOnly = !permitirEdicion ||
                                !(column.Name == "EgoeraGailua"); // Irakaslea solo puede editar EgoeraGailua (no EzabatzekoMarka)
            }
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
            string rol = Errola.ErabiltzaileRola?.ToLower() ?? "";

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dataGridViewGailuakGehitu.Columns[e.ColumnIndex].Name;

                if (columnName == "EgoeraGailua" && (rol == "irakaslea" || rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt"))
                {
                    dataGridViewGailuakGehitu.BeginEdit(true); // Solo permite editar EgoeraGailua
                }
            }
        }
        private bool cargandoMintegiak = false; // Variable de control

        private void CargarMintegiak()
        {
            cargandoMintegiak = true; // Activamos el flag

            try
            {
                DataTable mintegiak = gailuakDAL.ObtenerTodosMintegiak();

                // Agregar opción "Todos" al principio
                DataRow todosRow = mintegiak.NewRow();
                todosRow["ID_Mintegia"] = -1;
                todosRow["Izena"] = "Guztiak";
                mintegiak.Rows.InsertAt(todosRow, 0);

                // Desvinculamos temporalmente el evento
                comboBoxMintegiak.SelectedIndexChanged -= comboBoxMintegiak_SelectedIndexChanged;

                comboBoxMintegiak.DataSource = mintegiak;
                comboBoxMintegiak.DisplayMember = "Izena";
                comboBoxMintegiak.ValueMember = "ID_Mintegia";
                comboBoxMintegiak.SelectedIndex = 0;

                // Volvemos a vincular el evento
                comboBoxMintegiak.SelectedIndexChanged += comboBoxMintegiak_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea mintegiak kargatzean: " + ex.Message,
                              "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cargandoMintegiak = false; // Desactivamos el flag
            }
        }

        private void CargarTodosLosGailuak()
        {
            todosLosGailuak = gailuakDAL.ObtenerTodosGailuak();
            MostrarGailuak(todosLosGailuak);
        }

        private void MostrarGailuak(DataTable gailuak)
        {
            try
            {
                dataGridViewGailuakGehitu.DataSource = gailuak;

                // Resto de la configuración del DataGridView...
                ConfigurarColumnasEditables();

                // Asegurar que EgoeraGailua no sea NULL
                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["EgoeraGailua"].Value == null || row.Cells["EgoeraGailua"].Value == DBNull.Value)
                    {
                        row.Cells["EgoeraGailua"].Value = "Ongi";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBoxMintegiak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMintegiak.SelectedItem == null) return;

            try
            {
                // Manejo seguro del SelectedValue
                object selectedValue = comboBoxMintegiak.SelectedValue;

                if (selectedValue == null)
                {
                    MessageBox.Show("Ez da mintegirik hautatu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Conversión segura a int
                if (!int.TryParse(selectedValue.ToString(), out int idMintegia))
                {
                    MessageBox.Show("Mintegiaren IDa ez da baliozkoa.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cargar los gailuak según la selección
                if (idMintegia == -1) // "Todos" seleccionado
                {
                    MostrarGailuak(todosLosGailuak);
                }
                else
                {
                    DataTable gailuakMintegia = gailuakDAL.ObtenerGailuakPorMintegia(idMintegia);
                    MostrarGailuak(gailuakMintegia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea gailuak kargatzean: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: registrar el error en un log
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

                // if (dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka"))
                // {
                //     dataGridViewGailuakGehitu.Columns["EzabatzekoMarka"].Visible = true;
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datuak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigurarEdicionSegunRol()
        {
            string rol = Errola.ErabiltzaileRola?.ToLower() ?? "";

            // Configurar columnas editables según el rol
            foreach (DataGridViewColumn column in dataGridViewGailuakGehitu.Columns)
            {
                if (rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt")
                {
                    column.ReadOnly = false;
                }
                else if (rol == "irakaslea")
                {
                    column.ReadOnly = !(column.Name == "EzabatzekoMarka" || column.Name == "EgoeraGailua");
                }
                else
                {
                    column.ReadOnly = true;
                }
            }
        }
        private void ConfigurarEdicionSegunRol(string rol)
        {
            bool permitirEdicion = rol == "zuzendaria" || rol == "ikt irakaslea" || rol == "ikt" || rol == "irakaslea";

            foreach (DataGridViewColumn column in dataGridViewGailuakGehitu.Columns)
            {
                column.ReadOnly = !permitirEdicion ||
                                 !(column.Name == "EzabatzekoMarka" || column.Name == "EgoeraGailua");
            }

            // Restricción adicional para ordezkaria
            if (rol == "ordezkaria")
            {
                foreach (DataGridViewColumn column in dataGridViewGailuakGehitu.Columns)
                {
                    column.ReadOnly = true;
                }
            }
        }
        private void ConfigurarColumnasEditables()
        {
            // Configurar columna ComboBox para EgoeraGailua si no existe
            if (!dataGridViewGailuakGehitu.Columns.Contains("EgoeraGailua"))
            {
                DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    Name = "EgoeraGailua",
                    HeaderText = "Egoera Gailua",
                    DataPropertyName = "EgoeraGailua",
                    ReadOnly = true // Se ajustará en ConfigurarEdicionSegunRol
                };
                comboBoxColumn.Items.AddRange("Ongi", "Apurtuta", "Kompontzen");
                dataGridViewGailuakGehitu.Columns.Add(comboBoxColumn);
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
                // Guardar el Mintegiak seleccionado antes de hacer cambios
                int mintegiIdSeleccionado = comboBoxMintegiak.SelectedValue != null ?
                                           Convert.ToInt32(comboBoxMintegiak.SelectedValue) : -1;

                foreach (DataGridViewRow row in dataGridViewGailuakGehitu.Rows)
                {
                    if (row.Cells["ID"].Value != null && row.Cells["ID"].Value != DBNull.Value)
                    {
                        int id = Convert.ToInt32(row.Cells["ID"].Value); // Gailuaren ID

                        // 🔁 Lortu mintegiaren izena eta bere ID-a
                        string mintegiIzena = row.Cells["Mintegi_Izena"].Value?.ToString() ?? "";
                        int idMintegia = GailuakDAL.LortuMintegiarenID(mintegiIzena);

                        // 🔁 Egoera
                        string egoeraGailua = "Ongi";
                        if (row.Cells["EgoeraGailua"].Value != null && row.Cells["EgoeraGailua"].Value != DBNull.Value)
                        {
                            egoeraGailua = row.Cells["EgoeraGailua"].Value.ToString();
                        }

                        // 🔁 Ezabatzeko marka
                        bool ezabatzekoMarka = false;
                        if (dataGridViewGailuakGehitu.Columns.Contains("EzabatzekoMarka") &&
                            row.Cells["EzabatzekoMarka"].Value != null &&
                            row.Cells["EzabatzekoMarka"].Value != DBNull.Value)
                        {
                            ezabatzekoMarka = Convert.ToBoolean(row.Cells["EzabatzekoMarka"].Value);
                        }

                        // 🔁 Erosketa data
                        DateTime erosketaData = DateTime.Now;
                        if (row.Cells["Erosketa_Data"].Value != null && row.Cells["Erosketa_Data"].Value != DBNull.Value)
                        {
                            erosketaData = Convert.ToDateTime(row.Cells["Erosketa_Data"].Value);
                        }

                        // 🔁 Aktualizatu datuak
                        gailuakDAL.ActualizarGailua(
                            id,
                            idMintegia,
                            row.Cells["Marka"].Value?.ToString() ?? "",
                            row.Cells["Modeloa"].Value?.ToString() ?? "",
                            erosketaData,
                            ezabatzekoMarka,
                            egoeraGailua
                        );
                    }
                }

                MessageBox.Show("Aldaketak ondo gorde dira.", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar los datos según el Mintegiak seleccionado
                if (mintegiIdSeleccionado == -1) // "Guztiak" seleccionado
                {
                    CargarTodosLosGailuak();
                }
                else
                {
                    DataTable gailuakMintegia = gailuakDAL.ObtenerGailuakPorMintegia(mintegiIdSeleccionado);
                    MostrarGailuak(gailuakMintegia);
                }
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
                    // Guardar el Mintegiak seleccionado antes de hacer cambios
                    int mintegiIdSeleccionado = comboBoxMintegiak.SelectedValue != null ?
                                               Convert.ToInt32(comboBoxMintegiak.SelectedValue) : -1;

                    int idGailua = Convert.ToInt32(dataGridViewGailuakGehitu.SelectedRows[0].Cells["ID"].Value);
                    gailuakDAL.EliminarGailuaCompleto(idGailua);
                    MessageBox.Show("Gailua ezabatu da.", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar los datos según el Mintegiak seleccionado
                    if (mintegiIdSeleccionado == -1) // "Guztiak" seleccionado
                    {
                        CargarTodosLosGailuak();
                    }
                    else
                    {
                        DataTable gailuakMintegia = gailuakDAL.ObtenerGailuakPorMintegia(mintegiIdSeleccionado);
                        MostrarGailuak(gailuakMintegia);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errorea ezabatzerakoan: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewGailuakGehitu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}