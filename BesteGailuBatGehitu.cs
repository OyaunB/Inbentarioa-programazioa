﻿using MySql.Data.MySqlClient;
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
    public partial class BesteGailuBatGehitu : Form
    {
        public BesteGailuBatGehitu()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            // Kargatu ComboBox-a hasierako egoerarekin
            CargarComboBoxEgoera();
        }

        private void CargarComboBoxEgoera()
        {
            // ComboBox-a hutsik dagoen egiaztatu eta gero kargatu
            if (comboBoxEgoera.Items.Count == 0)
            {
                comboBoxEgoera.Items.AddRange(new string[] { "Ongi", "Apurtuta", "Kompontzen" });
                comboBoxEgoera.SelectedIndex = 0; // "Ongi" hautatuko da lehen aldiz
            }
        }

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

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
            aukeraAutatzeko form8 = new aukeraAutatzeko();
            form8.ShowDialog();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Scroll-a beharrezkoa denean
        }

        private void BtAukeraAutatu_Click(object sender, EventArgs e)
        {
            // Botoiaren logika
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox-a aldatzen denean egon daitekeen logika
        }

        private void bidaliBotoia_Click(object sender, EventArgs e)
        {
            string marka = tbMarka.Text.Trim();
            string modeloa = tbModeloa.Text.Trim();
            string egoera = comboBoxEgoera.SelectedItem?.ToString() ?? "Ongi";  // 'Ongi' hautatu ezean

            if (string.IsNullOrEmpty(marka) || string.IsNullOrEmpty(modeloa))
            {
                MessageBox.Show("Marka eta Modeloa eremuak bete behar dira.");
                return;
            }

            try
            {
                string connectionString = "server=localhost;database=inbentarioa;uid=root;pwd=root;";
                GailuakDAL gailuakDAL = new GailuakDAL(connectionString);

                bool result = gailuakDAL.GehituBesteGailua(marka, modeloa, egoera); // Egoera ere pasatzen da

                if (result)
                {
                    MessageBox.Show("Gailua ondo gehitu da!");
                    tbMarka.Text = "";
                    tbModeloa.Text = "";
                }
                else
                {
                    MessageBox.Show("Errorea gertatu da gailua gehitzean.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea: " + ex.Message);
            }
        }
    }
}
