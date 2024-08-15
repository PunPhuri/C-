using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static project.loginwindow;

namespace project
{
    public partial class adminwindow : Form
    {
        public static adminwindow instance;
        public adminwindow()
        {
            InitializeComponent();
            instance = this;
            shownotiadmin();
        }

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }


        //ปุ่มออกจากหน้าADMIN
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คุณต้องการออกจากหน้า ADMIN ใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }


        //ปุ่มหน้าตรวจสอบคำสั่งซื้อ
        private void label3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            adminverify adver = new adminverify(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(adver);
            adver.Show();
        }

        //ปุ่มหน้าประวัติคำสั่งซื้อสำเร็จ
        private void label4_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            historyadmin hisad = new historyadmin() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hisad);
            hisad.Show();
        }

        //ปุ่มหน้าตรวจสอบคำสั่งซื้อที่ถูกยกเลิก
        private void label5_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            historyrejectadmin hisread = new historyrejectadmin() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hisread);
            hisread.Show();
        }

        //ปุ่มหน้าตรวจสอบรายได้รวม
        private void label2_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            summary summer = new summary() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(summer);
            summer.Show();
        }

        //แสดงตัวเลขจำนวนในหน้าต่างๆ
        public void shownotiadmin()
        {

            using (MySqlConnection conn = DatabaseConnection())
            {
                conn.Open();
                string query1 = "SELECT COUNT(DISTINCT order_id) FROM orderverify";

                using (MySqlCommand cmd1 = new MySqlCommand(query1, conn))
                {
                    object result1 = cmd1.ExecuteScalar();

                    if (result1 != null && result1 != DBNull.Value)
                    {
                        int verCountad = Convert.ToInt32(result1);

                        if (verCountad != 0)
                        {
                            label7.Text = verCountad.ToString();
                            label7.Visible = true;
                        }
                        else
                        {
                            label7.Visible = false;
                        }
                    }
                    else
                    {
                        label7.Visible = false;
                    }
                }
            }
        }

        //เพิ่มสินค้าใหม่
        private void label6_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            additemad add = new additemad() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(add);
            add.Show();
        }

        //เพิ่มสต๊อกสินค้า
        private void label8_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            addstock addst = new addstock() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(addst);
            addst.Show();
        }
    }
}
