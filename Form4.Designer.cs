namespace Inbentarioa
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            pictureBox1 = new PictureBox();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            TbGailuakGehitu = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(38, 29);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Papyrus", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(84, 29);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(0, 44);
            label4.TabIndex = 9;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(38, 93);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(683, 262);
            dataGridView1.TabIndex = 15;
            // 
            // button2
            // 
            button2.Font = new Font("Stencil", 19.8000011F, FontStyle.Italic, GraphicsUnit.Point, 0);
            button2.Location = new Point(342, 374);
            button2.Name = "button2";
            button2.Size = new Size(153, 51);
            button2.TabIndex = 17;
            button2.Text = "ATZERA";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TbGailuakGehitu
            // 
            TbGailuakGehitu.AutoSize = true;
            TbGailuakGehitu.BackColor = Color.Transparent;
            TbGailuakGehitu.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TbGailuakGehitu.ForeColor = SystemColors.ControlLightLight;
            TbGailuakGehitu.Location = new Point(170, 32);
            TbGailuakGehitu.Name = "TbGailuakGehitu";
            TbGailuakGehitu.Size = new Size(478, 41);
            TbGailuakGehitu.TabIndex = 18;
            TbGailuakGehitu.Text = "EZABATUTAKOAK IKUSI";
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TbGailuakGehitu);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Name = "Form4";
            Text = "Form4";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label4;
        private DataGridView dataGridView1;
        private Button button2;
        private Label TbGailuakGehitu;
    }
}