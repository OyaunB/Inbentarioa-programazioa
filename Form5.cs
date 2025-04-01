using System;
using System.Data;
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
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            CargarMintegiak();
        }

        private void CargarMintegiak()
        {
            try
            {
                dataGridView1.DataSource = dbMintegiak.LortuMintegiak();

                // Configurar columnas (si es necesario)
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["ID_Mintegia"].HeaderText = "ID";
                    dataGridView1.Columns["Izena"].HeaderText = "Izena";
                    dataGridView1.Columns["Kokapena"].HeaderText = "Kokapena";
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
            if (Controls.Find("textBoxID", true).Length == 0 ||
                Controls.Find("textBoxIzena", true).Length == 0 ||
                Controls.Find("textBoxKokapena", true).Length == 0)
            {
                MessageBox.Show("Faltan controles en el formulario!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var txtID = Controls.Find("textBoxID", true)[0] as TextBox;
            var txtIzena = Controls.Find("textBoxIzena", true)[0] as TextBox;
            var txtKokapena = Controls.Find("textBoxKokapena", true)[0] as TextBox;

            if (string.IsNullOrEmpty(txtIzena.Text) || string.IsNullOrEmpty(txtKokapena.Text))
            {
                MessageBox.Show("Bete izena eta kokapena!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtID.Text, out int id))
            {
                MessageBox.Show("ID zenbaki bat izan behar da!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (dbMintegiak.GehituMintegia(id, txtIzena.Text, txtKokapena.Text))
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];

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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hautatu mintegi bat eguneratzeko!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Mintegia"].Value);

                var txtIzena = Controls.Find("textBoxIzena", true)[0] as TextBox;
                var txtKokapena = Controls.Find("textBoxKokapena", true)[0] as TextBox;

                if (dbMintegiak.EguneratuMintegia(id, txtIzena.Text, txtKokapena.Text))
                {
                    MessageBox.Show("Mintegia eguneratu da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMintegiak();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errorea: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEzabatu_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hautatu mintegi bat ezabatzeko!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Ziur zaude mintegi hau ezabatu nahi duzula?",
                                       "Kontuz",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Mintegia"].Value);

                    if (dbMintegiak.EzabatuMintegia(id))
                    {
                        MessageBox.Show("Mintegia ezabatu da!", "Ongi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarMintegiak();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errorea: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}