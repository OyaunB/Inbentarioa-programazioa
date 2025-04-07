namespace Inbentarioa
{
    partial class MintegienKudeaketa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MintegienKudeaketa));
            DataGridViewMintegiak = new DataGridView();
            TbGailuakGehitu = new Label();
            BtGehitu = new Button();
            btAldatu = new Button();
            btEzabatu = new Button();
            btAtzera = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridViewMintegiak).BeginInit();
            SuspendLayout();
            // 
            // DataGridViewMintegiak
            // 
            DataGridViewMintegiak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewMintegiak.Location = new Point(203, 80);
            DataGridViewMintegiak.Name = "DataGridViewMintegiak";
            DataGridViewMintegiak.RowHeadersWidth = 51;
            DataGridViewMintegiak.Size = new Size(780, 285);
            DataGridViewMintegiak.TabIndex = 22;
            // 
            // TbGailuakGehitu
            // 
            TbGailuakGehitu.AutoSize = true;
            TbGailuakGehitu.BackColor = Color.Transparent;
            TbGailuakGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TbGailuakGehitu.ForeColor = SystemColors.ControlLightLight;
            TbGailuakGehitu.Location = new Point(400, 9);
            TbGailuakGehitu.Name = "TbGailuakGehitu";
            TbGailuakGehitu.Size = new Size(437, 41);
            TbGailuakGehitu.TabIndex = 23;
            TbGailuakGehitu.Text = "MINTEGIAK KUDEATU";
            // 
            // BtGehitu
            // 
            BtGehitu.Anchor = AnchorStyles.None;
            BtGehitu.BackColor = Color.Transparent;
            BtGehitu.BackgroundImage = (Image)resources.GetObject("BtGehitu.BackgroundImage");
            BtGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtGehitu.ForeColor = Color.Transparent;
            BtGehitu.Location = new Point(203, 400);
            BtGehitu.Name = "BtGehitu";
            BtGehitu.Size = new Size(174, 80);
            BtGehitu.TabIndex = 24;
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
            btAldatu.Location = new Point(383, 400);
            btAldatu.Name = "btAldatu";
            btAldatu.Size = new Size(185, 80);
            btAldatu.TabIndex = 25;
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
            btEzabatu.Location = new Point(583, 400);
            btEzabatu.Name = "btEzabatu";
            btEzabatu.Size = new Size(200, 80);
            btEzabatu.TabIndex = 26;
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
            btAtzera.Location = new Point(800, 400);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(183, 80);
            btAtzera.TabIndex = 27;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 533);
            Controls.Add(btAtzera);
            Controls.Add(btEzabatu);
            Controls.Add(btAldatu);
            Controls.Add(BtGehitu);
            Controls.Add(TbGailuakGehitu);
            Controls.Add(DataGridViewMintegiak);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Izarraitz";
            Load += Form5_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewMintegiak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DataGridViewMintegiak;
        private Label TbGailuakGehitu;
        private Button BtGehitu;
        private Button btAldatu;
        private Button btEzabatu;
        private Button btAtzera;
    }
}