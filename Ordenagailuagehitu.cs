//OrdenagailuakGehitu.cs
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
    public partial class Ordenagailuagehitu : Form
    {
        public Ordenagailuagehitu()
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bidaliBotoia_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            aukeraAutatzeko form8 = new aukeraAutatzeko();
            form8.ShowDialog();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // Rellenar el ComboBox con las opciones de estado
            comboBoxEgoeraOrd.Items.AddRange(new string[] { "Ongi", "Apurtuta", "Kompontzen" });
            comboBoxEgoeraOrd.SelectedIndex = 0; // Seleccionar "Ongi" por defecto
        }

        private void lbizena_Click(object sender, EventArgs e)
        {

        }

        private void bidaliBotoia_Click_1(object sender, EventArgs e)
        {
            // Validar campos requeridos
            if (string.IsNullOrWhiteSpace(btMintegiarenKodea.Text) ||
                string.IsNullOrWhiteSpace(btMarka.Text) ||
                string.IsNullOrWhiteSpace(btModeloa.Text) ||
                string.IsNullOrWhiteSpace(btErosketaData.Text) ||
                string.IsNullOrWhiteSpace(btTxartelGrafikoa.Text) ||
                string.IsNullOrWhiteSpace(btRAMMemoria.Text) ||
                string.IsNullOrWhiteSpace(btUSBPortuak.Text) ||
                comboBoxEgoeraOrd.SelectedItem == null)
            {
                MessageBox.Show("Mesedez, bete eremu guztiak.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar formatos numéricos
            if (!int.TryParse(btMintegiarenKodea.Text, out int mintegiKodea) ||
                !int.TryParse(btRAMMemoria.Text, out int ram) ||
                !int.TryParse(btUSBPortuak.Text, out int usbPortuak))
            {
                MessageBox.Show("Mintegi kodea, RAM eta USB portuak zenbakiak izan behar dira.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar fecha
            if (!DateTime.TryParse(btErosketaData.Text, out DateTime erosketaData))
            {
                MessageBox.Show("Sartu data egokia (Adibidez: 2023-12-31).", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DBOrdenagailuakGehitu gailuakDAL = new DBOrdenagailuakGehitu();
                bool result = gailuakDAL.GehituOrdenagailua(
                    mintegiKodea.ToString(),
                    btMarka.Text,
                    btModeloa.Text,
                    erosketaData,
                    btTxartelGrafikoa.Text,
                    ram,
                    usbPortuak,
                    comboBoxEgoeraOrd.SelectedItem.ToString()
                );

                if (result)
                {
                    MessageBox.Show("Ordenagailua ondo gehitu da!", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Limpiar formulario
                    btMintegiarenKodea.Text = "";
                    btMarka.Text = "";
                    btModeloa.Text = "";
                    btErosketaData.Text = "";
                    btTxartelGrafikoa.Text = "";
                    btRAMMemoria.Text = "";
                    btUSBPortuak.Text = "";
                    comboBoxEgoeraOrd.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea gertatu da: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ordenagailuagehitu_Load(object sender, EventArgs e)
        {
            // Rellenar ComboBox de estado
            comboBoxEgoeraOrd.Items.AddRange(new string[] { "Ongi", "Apurtuta", "Kompontzen" });
            comboBoxEgoeraOrd.SelectedIndex = 0;

            // Configurar fecha actual por defecto
            btErosketaData.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

    }
}
