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
            dataGridViewErabiltzailea = new DataGridView();
            BtGehitu = new Button();
            btAldatu = new Button();
            btEzabatu = new Button();
            btAtzera = new Button();
            TbGailuakGehitu = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewErabiltzailea).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewErabiltzailea
            // 
            dataGridViewErabiltzailea.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewErabiltzailea.Location = new Point(203, 80);
            dataGridViewErabiltzailea.Name = "dataGridViewErabiltzailea";
            dataGridViewErabiltzailea.RowHeadersWidth = 51;
            dataGridViewErabiltzailea.Size = new Size(780, 285);
            dataGridViewErabiltzailea.TabIndex = 22;
            // 
            // BtGehitu
            // 
            BtGehitu.Anchor = AnchorStyles.None;
            BtGehitu.BackColor = Color.Transparent;
            BtGehitu.BackgroundImage = (Image)resources.GetObject("BtGehitu.BackgroundImage");
            BtGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtGehitu.ForeColor = Color.Transparent;
            BtGehitu.Location = new Point(200, 400);
            BtGehitu.Name = "BtGehitu";
            BtGehitu.Size = new Size(170, 80);
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
            btAldatu.Location = new Point(385, 400);
            btAldatu.Name = "btAldatu";
            btAldatu.Size = new Size(185, 80);
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
            btEzabatu.Location = new Point(585, 400);
            btEzabatu.Name = "btEzabatu";
            btEzabatu.Size = new Size(200, 80);
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
            btAtzera.Location = new Point(800, 400);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(180, 80);
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
            TbGailuakGehitu.Location = new Point(340, 9);
            TbGailuakGehitu.Name = "TbGailuakGehitu";
            TbGailuakGehitu.Size = new Size(544, 41);
            TbGailuakGehitu.TabIndex = 27;
            TbGailuakGehitu.Text = "ERABILTZAILEAK KUDEATU";
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 533);
            Controls.Add(TbGailuakGehitu);
            Controls.Add(btAtzera);
            Controls.Add(btEzabatu);
            Controls.Add(btAldatu);
            Controls.Add(BtGehitu);
            Controls.Add(dataGridViewErabiltzailea);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form6";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Izarraitz";
            Load += Form6_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewErabiltzailea).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewErabiltzailea;
        private Button BtGehitu;
        private Button btAldatu;
        private Button btEzabatu;
        private Button btAtzera;
        private Label TbGailuakGehitu;
    }
}