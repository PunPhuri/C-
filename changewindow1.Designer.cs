namespace project
{
    partial class changewindow1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(changewindow1));
            this.Formloader = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textboxemail = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textboxotp = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Formloader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Formloader
            // 
            this.Formloader.Controls.Add(this.pictureBox3);
            this.Formloader.Controls.Add(this.button2);
            this.Formloader.Controls.Add(this.textboxemail);
            this.Formloader.Controls.Add(this.button1);
            this.Formloader.Controls.Add(this.textboxotp);
            this.Formloader.Controls.Add(this.pictureBox1);
            this.Formloader.Location = new System.Drawing.Point(-2, 0);
            this.Formloader.Name = "Formloader";
            this.Formloader.Size = new System.Drawing.Size(1110, 637);
            this.Formloader.TabIndex = 1;
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
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("DB HelvethaicaMon X 75 Bd", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button2.Location = new System.Drawing.Point(436, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(266, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "Send OTP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textboxemail
            // 
            this.textboxemail.BackColor = System.Drawing.Color.Black;
            this.textboxemail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxemail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textboxemail.ForeColor = System.Drawing.Color.White;
            this.textboxemail.Location = new System.Drawing.Point(436, 245);
            this.textboxemail.Multiline = true;
            this.textboxemail.Name = "textboxemail";
            this.textboxemail.Size = new System.Drawing.Size(364, 27);
            this.textboxemail.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("DB HelvethaicaMon X 75 Bd", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.button1.Location = new System.Drawing.Point(436, 470);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 46);
            this.button1.TabIndex = 2;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textboxotp
            // 
            this.textboxotp.BackColor = System.Drawing.Color.Black;
            this.textboxotp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxotp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textboxotp.ForeColor = System.Drawing.Color.White;
            this.textboxotp.Location = new System.Drawing.Point(436, 410);
            this.textboxotp.Multiline = true;
            this.textboxotp.Name = "textboxotp";
            this.textboxotp.Size = new System.Drawing.Size(285, 27);
            this.textboxotp.TabIndex = 1;
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
            // changewindow1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 637);
            this.Controls.Add(this.Formloader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "changewindow1";
            this.Text = "changewindow1";
            this.Formloader.ResumeLayout(false);
            this.Formloader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Formloader;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textboxemail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textboxotp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}