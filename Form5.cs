using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class Form5 : Form
    {
        private DBMintegiak dbMintegiak = new DBMintegiak();

        public Form5()
        {
            InitializeComponent();
            // Configurar DataGridView al iniciar
            DataGridViewMintegiak.AutoGenerateColumns = true;
            DataGridViewMintegiak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            CargarMintegiak();

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



        private void CargarMintegiak()
        {
            try
            {
                DataGridViewMintegiak.DataSource = dbMintegiak.LortuMintegiak();


                // Configurar columnas (si es necesario)
                if (DataGridViewMintegiak.Columns.Count > 0)
                {
                    DataGridViewMintegiak.Columns["ID_Mintegia"].HeaderText = "ID";
                    DataGridViewMintegiak.Columns["Izena"].HeaderText = "Izena";
                    DataGridViewMintegiak.Columns["Kokapena"].HeaderText = "Kokapena";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtGehitu_Click(object sender, EventArgs e)
        {
            // Verificar si los controles existen
            if (Controls.Find("textBoxIzena", true).Length == 0 ||
                Controls.Find("textBoxKokapena", true).Length == 0)
            {
                MessageBox.Show("Faltan controles en el formulario!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var txtIzena = Controls.Find("textBoxIzena", true)[0] as TextBox;
            var txtKokapena = Controls.Find("textBoxKokapena", true)[0] as TextBox;

            if (string.IsNullOrEmpty(txtIzena.Text) || string.IsNullOrEmpty(txtKokapena.Text))
            {
                MessageBox.Show("Bete izena eta kokapena!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 🔹 No se pasa el ID, MySQL lo genera automáticamente
                if (dbMintegiak.GehituMintegia(txtIzena.Text, txtKokapena.Text))
                {
                    MessageBox.Show("Mintegia gehitu da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMintegiak();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void BtGehitu_Click_1(object sender, EventArgs e)
        {
            if (DataGridViewMintegiak.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = DataGridViewMintegiak.SelectedRows[0];

                    var txtID = Controls.Find("textBoxID", true)[0] as TextBox;
                    var txtIzena = Controls.Find("textBoxIzena", true)[0] as TextBox;
                    var txtKokapena = Controls.Find("textBoxKokapena", true)[0] as TextBox;

                    txtID.Text = row.Cells["ID_Mintegia"].Value.ToString();
                    txtIzena.Text = row.Cells["Izena"].Value.ToString();
                    txtKokapena.Text = row.Cells["Kokapena"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errorea datuak kargatzerakoan: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aukerak f2 = new Aukerak();
            f2.ShowDialog();
        }

        private void btAldatu_Click(object sender, EventArgs e)
        {
            if (DataGridViewMintegiak.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(DataGridViewMintegiak.SelectedRows[0].Cells["ID_Mintegia"].Value);
                string izena = Microsoft.VisualBasic.Interaction.InputBox("Sartu izen berria:", "Aldatu Mintegia", DataGridViewMintegiak.SelectedRows[0].Cells["Izena"].Value.ToString());
                string kokapena = Microsoft.VisualBasic.Interaction.InputBox("Sartu kokapen berria:", "Aldatu Mintegia", DataGridViewMintegiak.SelectedRows[0].Cells["Kokapena"].Value.ToString());

                if (!string.IsNullOrWhiteSpace(izena) && !string.IsNullOrWhiteSpace(kokapena))
                {
                    if (dbMintegiak.EguneratuMintegia(id, izena, kokapena))
                    {
                        MessageBox.Show("Mintegia eguneratu da!", "Oharra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarMintegiak();
                    }
                }
            }
            else
            {
                MessageBox.Show("Aldatzeko mintegi bat aukeratu!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

       

        private void BtGehitu_Click_2(object sender, EventArgs e)
        {
            string izena = Microsoft.VisualBasic.Interaction.InputBox("Sartu mintegiaren izena:", "Gehitu Mintegia", "");
            string kokapena = Microsoft.VisualBasic.Interaction.InputBox("Sartu kokapena:", "Gehitu Mintegia", "");

            if (!string.IsNullOrWhiteSpace(izena) && !string.IsNullOrWhiteSpace(kokapena))
            {
                // 🔹 Llamamos a GehituMintegia() sin pasar un ID
                if (dbMintegiak.GehituMintegia(izena, kokapena))
                {
                    MessageBox.Show("Mintegia gehitu da!", "Oharra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMintegiak(); // Refrescar la tabla
                }
            }
        }

        private void btEzabatu_Click(object sender, EventArgs e)
        {
            if (DataGridViewMintegiak.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(DataGridViewMintegiak.SelectedRows[0].Cells["ID_Mintegia"].Value);
                DialogResult result = MessageBox.Show("Ziur zaude mintegia ezabatu nahi duzula?", "Ezabatu Mintegia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (dbMintegiak.EzabatuMintegia(id))
                    {
                        MessageBox.Show("Mintegia ezabatu da!", "Oharra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarMintegiak();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ezabatzeko mintegi bat aukeratu!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}