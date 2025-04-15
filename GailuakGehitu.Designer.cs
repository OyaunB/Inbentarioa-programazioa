namespace Inbentarioa
{
    partial class GailuakGehitu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GailuakGehitu));
            TbGailuakGehitu = new Label();
            dataGridViewGailuakGehitu = new DataGridView();
            BtGehitu = new Button();
            btAldatu = new Button();
            btEzabatu = new Button();
            btAtzera = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGailuakGehitu).BeginInit();
            SuspendLayout();
            // 
            // TbGailuakGehitu
            // 
            TbGailuakGehitu.AutoSize = true;
            TbGailuakGehitu.BackColor = Color.Transparent;
            TbGailuakGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TbGailuakGehitu.ForeColor = SystemColors.ControlLightLight;
            TbGailuakGehitu.Location = new Point(710, 17);
            TbGailuakGehitu.Name = "TbGailuakGehitu";
            TbGailuakGehitu.Size = new Size(354, 41);
            TbGailuakGehitu.TabIndex = 11;
            TbGailuakGehitu.Text = "GAILUAK GEHITU";
            // 
            // dataGridViewGailuakGehitu
            // 
            dataGridViewGailuakGehitu.AllowUserToOrderColumns = true;
            dataGridViewGailuakGehitu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGailuakGehitu.Location = new Point(12, 80);
            dataGridViewGailuakGehitu.Name = "dataGridViewGailuakGehitu";
            dataGridViewGailuakGehitu.RowHeadersWidth = 51;
            dataGridViewGailuakGehitu.Size = new Size(1413, 285);
            dataGridViewGailuakGehitu.TabIndex = 12;
            dataGridViewGailuakGehitu.CellContentClick += dataGridViewGailuakGehitu_CellContentClick;
            // 
            // BtGehitu
            // 
            BtGehitu.Anchor = AnchorStyles.None;
            BtGehitu.BackColor = Color.Transparent;
            BtGehitu.BackgroundImage = (Image)resources.GetObject("BtGehitu.BackgroundImage");
            BtGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtGehitu.ForeColor = Color.Transparent;
            BtGehitu.Location = new Point(332, 400);
            BtGehitu.Name = "BtGehitu";
            BtGehitu.Size = new Size(170, 80);
            BtGehitu.TabIndex = 13;
            BtGehitu.UseVisualStyleBackColor = false;
            BtGehitu.Click += BtGehitu_Click;
            // 
            // btAldatu
            // 
            btAldatu.Anchor = AnchorStyles.None;
            btAldatu.BackColor = Color.Transparent;
            btAldatu.BackgroundImage = (Image)resources.GetObject("btAldatu.BackgroundImage");
            btAldatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAldatu.ForeColor = Color.Transparent;
            btAldatu.Location = new Point(515, 400);
            btAldatu.Name = "btAldatu";
            btAldatu.Size = new Size(185, 80);
            btAldatu.TabIndex = 14;
            btAldatu.UseVisualStyleBackColor = false;
            btAldatu.Click += btAldatu_Click;
            // 
            // btEzabatu
            // 
            btEzabatu.Anchor = AnchorStyles.None;
            btEzabatu.BackColor = Color.Transparent;
            btEzabatu.BackgroundImage = (Image)resources.GetObject("btEzabatu.BackgroundImage");
            btEzabatu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btEzabatu.ForeColor = Color.Transparent;
            btEzabatu.Location = new Point(710, 400);
            btEzabatu.Name = "btEzabatu";
            btEzabatu.Size = new Size(200, 80);
            btEzabatu.TabIndex = 15;
            btEzabatu.UseVisualStyleBackColor = false;
            btEzabatu.Click += btEzabatu_Click;
            // 
            // btAtzera
            // 
            btAtzera.Anchor = AnchorStyles.None;
            btAtzera.BackColor = Color.Transparent;
            btAtzera.BackgroundImage = (Image)resources.GetObject("btAtzera.BackgroundImage");
            btAtzera.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAtzera.ForeColor = Color.Transparent;
            btAtzera.Location = new Point(925, 400);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(179, 80);
            btAtzera.TabIndex = 16;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // GailuakGehitu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1432, 533);
            Controls.Add(btAtzera);
            Controls.Add(btEzabatu);
            Controls.Add(btAldatu);
            Controls.Add(BtGehitu);
            Controls.Add(dataGridViewGailuakGehitu);
            Controls.Add(TbGailuakGehitu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GailuakGehitu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Izarraitz";
            Load += GailuakGehitu_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewGailuakGehitu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label TbGailuakGehitu;
        private DataGridView dataGridViewGailuakGehitu;
        private Button BtGehitu;
        private Button btAldatu;
        private Button btEzabatu;
        private Button btAtzera;
    }
}