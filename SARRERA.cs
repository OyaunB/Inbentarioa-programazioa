//SARRERA.cs
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Inbentarioa
{
    public partial class Sarrera : Form
    {
        private bool _isPainting = false;
        public Sarrera()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }



        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Ez deitu base.OnPaintBackground() degradadoa guztiz kontrolatu nahi baduzu
            Color colorInicio = ColorTranslator.FromHtml("#5de0e6");
            Color colorFin = ColorTranslator.FromHtml("#004aad");

            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                colorInicio,
                colorFin,
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e); // Garrantzitsua: base-ko kodea exekutatu

            if (WindowState != FormWindowState.Minimized && this.ClientRectangle.Width > 0)
            {
                this.Invalidate(); // Baina ez Refresh()
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
            TbErabiltzailea.KeyDown += TbErabiltzailea_KeyDown;
            TbPasahitza.KeyDown += TbPasahitza_KeyDown;


        }
        private void TbErabiltzailea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita que el enter haga un sonido o recargue
                TbPasahitza.Focus(); // Mueve el foco al campo de la contraseña
            }
        }
        private void TbPasahitza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                // Aldaketa 1: Ez deitu button1_Click zuzenean. Hobeto:
                BtBidali.PerformClick(); // <- Honek botoiaren Click eventua modu egokian aktibatzen du
            }
        }

        

     






        private void TbErabiltzailea_TextChanged(object sender, EventArgs e)
        {

        }
        // Gehitu klasean (Sarrera klasearen barruan):
        private bool ProcessingLogin = false;
        private void BtBidali_Click(object sender, EventArgs e)
        {
            if (this.ProcessingLogin) return;
            this.ProcessingLogin = true;

            try
            {
                string erabiltzailea = TbErabiltzailea.Text.Trim();
                string pasahitza = TbPasahitza.Text.Trim();

                DBErabiltzaileak dbErabiltzaileak = new DBErabiltzaileak();
                var (egiaztatuta, izena, errola) = dbErabiltzaileak.EgiaztatuErabiltzailea(erabiltzailea, pasahitza);

                if (egiaztatuta)
                {
                    TbErabiltzailea.Text = "";
                    TbPasahitza.Text = "";

                    // Rola gorde klase estatikoan
                    Errola.ErabiltzaileRola = errola;

                    this.Hide();
                    using (Aukerak f2 = new Aukerak())
                    {
                        f2.ShowDialog();
                    }
                    this.Show();
                    return;
                }

                MessageBox.Show("Erabiltzaile edo pasahitza okerra!", "Errorea",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea datu-basearekin konektatzerakoan: " + ex.Message,
                              "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.ProcessingLogin = false;
            }
        }
    }
}
