using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class Sarrera : Form
    {
        public Sarrera()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Forzar el redibujado del formulario
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string erabiltzailea = TbErabiltzailea.Text.Trim();
            string pasahitza = TbPasahitza.Text.Trim();

            // Fitxategitik erabiltzaileak irakurri
            string[] lerroak;
            try
            {
                lerroak = File.ReadAllLines("Erabiltzaileak.txt"); // Fitxategia irakurri
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea erabiltzaile fitxategia irakurtzerakoan: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Erabiltzailea bilatu
            foreach (string lerroa in lerroak)
            {
                string[] zatiak = lerroa.Split(';');
                if (zatiak.Length == 3)
                {
                    string fitxErabiltzailea = zatiak[0];
                    string fitxPasahitza = zatiak[1];
                    string rola = zatiak[2];

                    if (erabiltzailea == fitxErabiltzailea && pasahitza == fitxPasahitza)
                    {
                        this.Hide();
                        Aukerak f2 = new Aukerak(rola); // Rola bidali
                        f2.ShowDialog();
                        this.Show();
                        return;
                    }
                }
            }

            // Ez bada aurkitu, errorea erakutsi
            MessageBox.Show("Erabiltzaile edo pasahitza okerra!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        private void TbErabiltzailea_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
