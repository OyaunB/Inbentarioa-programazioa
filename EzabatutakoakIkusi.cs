//EzabatutakoakIkusi.cs
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
    public partial class EzabatutakoakIkusi : Form
    {
        public EzabatutakoakIkusi()
        {
            InitializeComponent();
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

        private void Form4_Load(object sender, EventArgs e)
        {
            // Configurar el DataGridView antes de cargar los datos
            DataGridViewEzabatutakoak.ReadOnly = true;
            DataGridViewEzabatutakoak.AllowUserToAddRows = false;
            DataGridViewEzabatutakoak.AllowUserToDeleteRows = false;
            DataGridViewEzabatutakoak.AllowUserToResizeRows = false;
            DataGridViewEzabatutakoak.MultiSelect = false;
            DataGridViewEzabatutakoak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewEzabatutakoak.EditMode = DataGridViewEditMode.EditProgrammatically;
            DataGridViewEzabatutakoak.RowHeadersVisible = false;

            // Estilo visual para la selección
            DataGridViewEzabatutakoak.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            DataGridViewEzabatutakoak.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Cargar los datos
            DBEzabatutakoak db = new DBEzabatutakoak();
            DataTable dt = db.LortuEzabatutakoGailuak();
            DataGridViewEzabatutakoak.DataSource = dt;

            // Opcional: Configurar el ancho de las columnas
            if (DataGridViewEzabatutakoak.Columns.Count > 0)
            {
                DataGridViewEzabatutakoak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Tabla f2 = new Izarraitz();
            //f2.Show();
        }

        private void btAtzera_Click(object sender, EventArgs e)
        {
            this.Hide();
           // Aukerak f2 = new Aukerak();
            //f2.ShowDialog();
        }

        private void TbGailuakGehitu_Click(object sender, EventArgs e)
        {

        }

        private void DataGridViewEzabatutakoak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
