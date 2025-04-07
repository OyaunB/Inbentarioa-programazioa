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

        }

        private void lbizena_Click(object sender, EventArgs e)
        {

        }

        private void bidaliBotoia_Click_1(object sender, EventArgs e)
        {
            // Jasotako datuak
            string mintegia = btMintegiarenKodea.Text.Trim();
            string marka = btMarka.Text.Trim();
            string modeloa = btModeloa.Text.Trim();
            string erosketadata = btErosketaData.Text.Trim();
            string txartela = btTxartelGrafikoa.Text.Trim();
            string ram = btRamMemoria.Text.Trim();
            string usb = btUSBPortuak.Text.Trim();
            string kolorea = btKolorea.Text.Trim();
            string egoera = btEgoera.Text.Trim();

            // Egiaztatu hutsik ez dauden eta formatu egokia duten
            if (string.IsNullOrWhiteSpace(mintegia) || string.IsNullOrWhiteSpace(marka) ||
                string.IsNullOrWhiteSpace(modeloa) || string.IsNullOrWhiteSpace(erosketadata) ||
                string.IsNullOrWhiteSpace(txartela) || string.IsNullOrWhiteSpace(ram) ||
                string.IsNullOrWhiteSpace(usb) || string.IsNullOrWhiteSpace(kolorea) ||
                string.IsNullOrWhiteSpace(egoera))
            {
                MessageBox.Show("Sartu datu guztiak mesedez.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Formatuaren egiaztapena
            if (!int.TryParse(mintegia, out _) || !int.TryParse(ram, out _) || !int.TryParse(usb, out _) || !DateTime.TryParse(erosketadata, out _))
            {
                MessageBox.Show("Sartu datuak formatu egokian mesedez.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DBOrdenagailuakGehitu.OrdenagailuaGehitu(mintegia, marka, modeloa, erosketadata, txartela, ram, usb, kolorea, egoera);
                MessageBox.Show("Ordenagailua ongi gehituta!", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea gertatu da: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
