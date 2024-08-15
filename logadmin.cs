using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class logadmin : Form
    {
        public logadmin()
        {
            InitializeComponent();
        }

        //ปุ่มLogin admin
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("punphuri") && textBox2.Text.Equals("1234"))
            {
                this.Formloader.Controls.Clear();
                homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.Formloader.Controls.Add(hasd);
                hasd.Show();

                adminwindow adw = new adminwindow();
                adw.Show();


                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("รหัส ADMIN ไม่ถูกต้อง");
            }
        }
    }
}
