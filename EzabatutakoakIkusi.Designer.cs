namespace Inbentarioa
{
    partial class EzabatutakoakIkusi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EzabatutakoakIkusi));
            label4 = new Label();
            DataGridViewEzabatutakoak = new DataGridView();
            TbGailuakGehitu = new Label();
            btAtzera = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridViewEzabatutakoak).BeginInit();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(84, 29);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(0, 32);
            label4.TabIndex = 9;
            // 
            // DataGridViewEzabatutakoak
            // 
            DataGridViewEzabatutakoak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewEzabatutakoak.Location = new Point(203, 80);
            DataGridViewEzabatutakoak.Name = "DataGridViewEzabatutakoak";
            DataGridViewEzabatutakoak.RowHeadersWidth = 51;
            DataGridViewEzabatutakoak.Size = new Size(780, 285);
            DataGridViewEzabatutakoak.TabIndex = 15;
            DataGridViewEzabatutakoak.CellContentClick += DataGridViewEzabatutakoak_CellContentClick;
            // 
            // TbGailuakGehitu
            // 
            TbGailuakGehitu.AutoSize = true;
            TbGailuakGehitu.BackColor = Color.Transparent;
            TbGailuakGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TbGailuakGehitu.ForeColor = SystemColors.ControlLightLight;
            TbGailuakGehitu.Location = new Point(384, 9);
            TbGailuakGehitu.Name = "TbGailuakGehitu";
            TbGailuakGehitu.Size = new Size(478, 41);
            TbGailuakGehitu.TabIndex = 18;
            TbGailuakGehitu.Text = "EZABATUTAKOAK IKUSI";
            TbGailuakGehitu.Click += TbGailuakGehitu_Click;
            // 
            // btAtzera
            // 
            btAtzera.Anchor = AnchorStyles.None;
            btAtzera.BackColor = Color.Transparent;
            btAtzera.BackgroundImage = (Image)resources.GetObject("btAtzera.BackgroundImage");
            btAtzera.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btAtzera.ForeColor = Color.Transparent;
            btAtzera.Location = new Point(500, 400);
            btAtzera.Name = "btAtzera";
            btAtzera.Size = new Size(180, 80);
            btAtzera.TabIndex = 28;
            btAtzera.UseVisualStyleBackColor = false;
            btAtzera.Click += btAtzera_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 533);
            Controls.Add(btAtzera);
            Controls.Add(TbGailuakGehitu);
            Controls.Add(DataGridViewEzabatutakoak);
            Controls.Add(label4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Izarraitz";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridViewEzabatutakoak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private DataGridView DataGridViewEzabatutakoak;
        private Label TbGailuakGehitu;
        private Button btAtzera;
    }
}