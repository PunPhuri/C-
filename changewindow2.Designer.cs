namespace project
{
    partial class changewindow2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(changewindow2));
            this.Formloader = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.textboxnewpass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textboxconnewpass = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Formloader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Formloader
            // 
            this.Formloader.Controls.Add(this.pictureBox3);
            this.Formloader.Controls.Add(this.textboxnewpass);
            this.Formloader.Controls.Add(this.button1);
            this.Formloader.Controls.Add(this.textboxconnewpass);
            this.Formloader.Controls.Add(this.pictureBox1);
            this.Formloader.Location = new System.Drawing.Point(-9, -23);
            this.Formloader.Name = "Formloader";
            this.Formloader.Size = new System.Drawing.Size(1110, 637);
            this.Formloader.TabIndex = 2;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(37, 34);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(52, 45);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // textboxnewpass
            // 
            this.textboxnewpass.BackColor = System.Drawing.Color.Black;
            this.textboxnewpass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxnewpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textboxnewpass.ForeColor = System.Drawing.Color.White;
            this.textboxnewpass.Location = new System.Drawing.Point(426, 254);
            this.textboxnewpass.Multiline = true;
            this.textboxnewpass.Name = "textboxnewpass";
            this.textboxnewpass.Size = new System.Drawing.Size(285, 27);
            this.textboxnewpass.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("DB HelvethaicaMon X 75 Bd", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(436, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 46);
            this.button1.TabIndex = 2;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textboxconnewpass
            // 
            this.textboxconnewpass.BackColor = System.Drawing.Color.Black;
            this.textboxconnewpass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxconnewpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textboxconnewpass.ForeColor = System.Drawing.Color.White;
            this.textboxconnewpass.Location = new System.Drawing.Point(426, 403);
            this.textboxconnewpass.Multiline = true;
            this.textboxconnewpass.Name = "textboxconnewpass";
            this.textboxconnewpass.Size = new System.Drawing.Size(285, 27);
            this.textboxconnewpass.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(147, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(823, 654);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // changewindow2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 590);
            this.Controls.Add(this.Formloader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "changewindow2";
            this.Text = "changewindow2";
            this.Formloader.ResumeLayout(false);
            this.Formloader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Formloader;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox textboxnewpass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textboxconnewpass;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}