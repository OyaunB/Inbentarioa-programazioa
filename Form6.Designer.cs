namespace Inbentarioa
{
    partial class Form6
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            dataGridView1 = new DataGridView();
            pictureBox1 = new PictureBox();
            BtGehitu = new Button();
            btAldatu = new Button();
            btEzabatu = new Button();
            btAtzera = new Button();
            TbGailuakGehitu = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(-10, 88);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(820, 262);
            dataGridView1.TabIndex = 22;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 32);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // BtGehitu
            // 
            BtGehitu.Anchor = AnchorStyles.None;
            BtGehitu.BackColor = Color.Transparent;
            BtGehitu.BackgroundImage = (Image)resources.GetObject("BtGehitu.BackgroundImage");
            BtGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtGehitu.ForeColor = Color.Transparent;
            BtGehitu.Location = new Point(22, 356);
            BtGehitu.Name = "BtGehitu";
            BtGehitu.Size = new Size(170, 74);
            BtGehitu.TabIndex = 23;
            BtGehitu.UseVisualStyleBackColor = false;
            // 
            // btAldatu
            // 
            btAldatu.Anchor = AnchorStyles.None;
            btAldatu.BackColor = Color.Transparent;
            btAldatu.BackgroundImage = (Image)resources.GetObject("btAldatu.BackgroundImage");
            btAldatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAldatu.ForeColor = Color.Transparent;
            btAldatu.Location = new Point(198, 356);
            btAldatu.Name = "btAldatu";
            btAldatu.Size = new Size(185, 74);
            btAldatu.TabIndex = 24;
            btAldatu.UseVisualStyleBackColor = false;
            // 
            // btEzabatu
            // 
            btEzabatu.Anchor = AnchorStyles.None;
            btEzabatu.BackColor = Color.Transparent;
            btEzabatu.BackgroundImage = (Image)resources.GetObject("btEzabatu.BackgroundImage");
            btEzabatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btEzabatu.ForeColor = Color.Transparent;
            btEzabatu.Location = new Point(389, 356);
            btEzabatu.Name = "btEzabatu";
            btEzabatu.Size = new Size(204, 74);
            btEzabatu.TabIndex = 25;
            btEzabatu.UseVisualStyleBackColor = false;
            // 
            // btAtzera
            // 
            btAtzera.Anchor = AnchorStyles.None;
            btAtzera.BackColor = Color.Transparent;
            btAtzera.BackgroundImage = (Image)resources.GetObject("btAtzera.BackgroundImage");
            btAtzera.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAtzera.ForeColor = Color.Transparent;
            btAtzera.Location = new Point(599, 355);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(179, 77);
            btAtzera.TabIndex = 26;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // TbGailuakGehitu
            // 
            TbGailuakGehitu.AutoSize = true;
            TbGailuakGehitu.BackColor = Color.Transparent;
            TbGailuakGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TbGailuakGehitu.ForeColor = SystemColors.ControlLightLight;
            TbGailuakGehitu.Location = new Point(130, 41);
            TbGailuakGehitu.Name = "TbGailuakGehitu";
            TbGailuakGehitu.Size = new Size(544, 41);
            TbGailuakGehitu.TabIndex = 27;
            TbGailuakGehitu.Text = "ERABILTZAILEAK KUDEATU";
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TbGailuakGehitu);
            Controls.Add(btAtzera);
            Controls.Add(btEzabatu);
            Controls.Add(btAldatu);
            Controls.Add(BtGehitu);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Name = "Form6";
            Text = "Form6";
            Load += Form6_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private PictureBox pictureBox1;
        private Button BtGehitu;
        private Button btAldatu;
        private Button btEzabatu;
        private Button btAtzera;
        private Label TbGailuakGehitu;
    }
}