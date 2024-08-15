namespace project
{
    partial class item_frames
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(item_frames));
            this.picture = new System.Windows.Forms.PictureBox();
            this.name_item = new System.Windows.Forms.Label();
            this.price_item = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
            this.picture.Location = new System.Drawing.Point(0, 0);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(267, 372);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.Click += new System.EventHandler(this.picture_Click);
            this.picture.MouseEnter += new System.EventHandler(this.item_frames_MouseEnter);
            // 
            // name_item
            // 
            this.name_item.AutoSize = true;
            this.name_item.Cursor = System.Windows.Forms.Cursors.Hand;
            this.name_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.name_item.Location = new System.Drawing.Point(3, 379);
            this.name_item.Name = "name_item";
            this.name_item.Size = new System.Drawing.Size(57, 20);
            this.name_item.TabIndex = 2;
            this.name_item.Text = "Name";
            // 
            // price_item
            // 
            this.price_item.AutoSize = true;
            this.price_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.price_item.Location = new System.Drawing.Point(3, 420);
            this.price_item.Name = "price_item";
            this.price_item.Size = new System.Drawing.Size(53, 20);
            this.price_item.TabIndex = 4;
            this.price_item.Text = "Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(66, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Baht";
            // 
            // item_frames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.price_item);
            this.Controls.Add(this.name_item);
            this.Controls.Add(this.picture);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "item_frames";
            this.Size = new System.Drawing.Size(267, 490);
            this.MouseEnter += new System.EventHandler(this.item_frames_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.item_frames_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label name_item;
        private System.Windows.Forms.Label price_item;
        private System.Windows.Forms.Label label1;
    }
}
