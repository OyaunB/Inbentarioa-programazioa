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
            // Erabiltzaile eta pasahitza irakurri
            string erabiltzailea = TbErabiltzailea.Text;
            string pasahitza = TbPasahitza.Text;

            // Erabiltzaile mota egiaztatu
            //ZUZENDARIA
            if (erabiltzailea == "admin" && pasahitza == "admin")
            {
                this.Hide();
                Aukerak f2 = new Aukerak("zuzendaria"); // Zuzendaria bidali
                f2.ShowDialog();
                this.Show();
            }
            //IKT IRAKASLEA
            else if (erabiltzailea == "aitzi2025" && pasahitza == "aitzi2025")
            {
                this.Hide();
                Aukerak f2 = new Aukerak("IKT Irakaslea"); // IKT irakaslea bidali
                f2.ShowDialog();
                this.Show();
            }
            //IRAKASLEA
            else if (erabiltzailea == "iker2025" && pasahitza == "iker2025")
            {
                this.Hide();
                Aukerak f2 = new Aukerak("Irakaslea"); // Irakaslea bidali
                f2.ShowDialog();
                this.Show();
            }
            else if (erabiltzailea == "gorka2025" && pasahitza == "gorka2025")
            {
                this.Hide();
                Aukerak f2 = new Aukerak("Irakaslea"); // Irakaslea bidali
                f2.ShowDialog();
                this.Show();
            }
            else if (erabiltzailea == "aingeru2025" && pasahitza == "aingeru2025")
            {
                this.Hide();
                Aukerak f2 = new Aukerak("Ordezkaria"); // Ordezkaria bidali
                f2.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Erabiltzaile edo pasahitza okerra!", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void TbErabiltzailea_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
