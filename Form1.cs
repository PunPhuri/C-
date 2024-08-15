using iTextSharp.text.pdf.parser;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static project.loginwindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace project
{
    public partial class Form1 : Form
    {

        public static Form1 instance;
        public Label usenamelog;
        public string unl = GlobalVariables.userlogin;


        //กำหนดบูลีน ปุ่มเลื่อน

        bool racketcollapse;
        bool shuttlecockcollapse;
        bool stringcollapse;

        public Form1()
        {
            InitializeComponent();
            instance = this;
            usenamelog = label1;


            //ตั้งให้โชว์หน้าโฆษณาตั้งแต่เริ่มโปรแกรม
            this.Formloader.Controls.Clear();
            homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hasd);
            hasd.Show();
            logoutbutton.Visible = false;
        }

        //เชื่อมsql
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }


        //ฟั่งชั่นเปลี่ยนหน้า โชว์สินค้า
        public void showpage(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            Formloader.Controls.Clear();
            Formloader.Controls.Add(form);
            form.Show();
        }


        //ฟั่งชั่นหลัง login สำเร็จ
        public void HideLoginButton()
        {
            logbutton.Visible = false;
            logoutbutton.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            shownoti();

        }


        //ฟั่งชั่นหลังกดปุ่ม logout 
        public void HideLogoutButton()
        {
            label1.Text = null;
            unl = GlobalVariables.userlogin = null;
            logbutton.Visible = true;
            logoutbutton.Visible = false;
            pictureBox2.Visible= false;
            pictureBox3.Visible= false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;

            this.Formloader.Controls.Clear();
            homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hasd);
            hasd.Show();
        }


        //ฟังชั่น โชว์การแจ้งเตือนจำนวนสินค้าในตะกร้าและรอการตรวจสอบ
        public void shownoti()
        {
            MySqlConnection conn = DatabaseConnection();
            string query = "SELECT COUNT(*) FROM cartitem WHERE username = @username";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    int cartCount = Convert.ToInt32(result);
                    if (cartCount != 0)
                    {
                        label6.Text = cartCount.ToString();
                        label6.Visible = true;
                    }
                    else
                    {
                        label6.Visible = false;
                    }
                }
                else
                {
                    label6.Visible = false;
                }
            }

            string query1 = "SELECT COUNT(DISTINCT order_id) FROM orderverify WHERE username = @username";
            using (MySqlCommand cmd1 = new MySqlCommand(query1, conn))
            {
                cmd1.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
                object result1 = cmd1.ExecuteScalar();
                if (result1 != null && result1 != DBNull.Value)
                {
                    int verCount = Convert.ToInt32(result1);
                    if (verCount != 0)
                    {
                        label7.Text = verCount.ToString();
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

        //ปุ่มปิดโปรแกรม
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult outcheck = MessageBox.Show("คุณต้องการออกจากโปรแกรมใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (outcheck == DialogResult.Yes)
            {
                this.Close();
            }
        }


        //ฟังชั่นปุ่มสไลด์ หมวดหมู่ไม้แบด
        private void racket_Tick(object sender, EventArgs e)
        {
            if (racketcollapse)
            {
                button3.BackColor = Color.White;
                button3.ForeColor = Color.Black;
                panel3.Height += 10;
                if (panel3.Height == panel3.MaximumSize.Height)
                {
                    racketcollapse = false;
                    racket.Stop();
                }
            }
            else
            {
                button3.ForeColor = Color.White;
                button3.BackColor = Color.Black;
                panel3.Height -= 10;
                if (panel3.Height == panel3.MinimumSize.Height)
                {
                    racketcollapse = true;
                    racket.Stop();
                }
            }
        }

        //ปุ่มกด ไม้แบด เพื่อให้ฟังชั่นสไลด์ทำงาน
        private void button3_Click(object sender, EventArgs e)
        {
            racket.Start();
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        //ฟังชั่นปุ่มสไลด์ หมวดหมู่ลูกแบด
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (shuttlecockcollapse)
            {
                button7.BackColor = Color.White;
                button7.ForeColor = Color.Black;
                panel2.Height += 10;
                if (panel2.Height == panel2.MaximumSize.Height)
                {
                    shuttlecockcollapse = false;
                    timer1.Stop();
                }
            }
            else
            {
                button7.ForeColor = Color.White;
                button7.BackColor = Color.Black;
                panel2.Height -= 10;
                if (panel2.Height == panel2.MinimumSize.Height)
                {
                    shuttlecockcollapse = true;
                    timer1.Stop();
                }
            }
        }

        //ปุ่มกด ลูกแบด เพื่อให้ฟังชั่นสไลด์ลูกแบดทำงาน
        private void button7_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        //ฟังชั่นปุ่มสไลด์ หมวดหมู่เอ็นไม้แบด
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (stringcollapse)
            {
                button12.BackColor = Color.White;
                button12.ForeColor = Color.Black;
                panel4.Height += 10;
                if (panel4.Height == panel4.MaximumSize.Height)
                {
                    stringcollapse = false;
                    timer2.Stop();
                }
            }
            else
            {
                button12.ForeColor = Color.White;
                button12.BackColor = Color.Black;
                panel4.Height -= 10;
                if (panel4.Height == panel4.MinimumSize.Height)
                {
                    stringcollapse = true;
                    timer2.Stop();
                }
            }
        }

        //ปุ่มกด เอ็นไม้แบด เพื่อให้ฟังชั่นสไลด์ปุ่มเอ็นไม้แบดทำงาน
        private void button12_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        

        //ฟังชั่น หน้าโชว์สินค้า
        private void ShowPage(string type, string brand)
        {
            racketyonex rky = new racketyonex(this,type,brand);
            showpage(rky);
        }


        //ปุ่มยี่ห้อไม้แบด และส่งค่า ประเภทและยี่ห้อ ไปฟังชั่น ShowPage เพื่อสร้างหน้าตามประเภทและยี่ห้อสินค้า
        private void racketyonex_Click(object sender, EventArgs e)
        {
            ShowPage("racket", "yonex");
        }

        
        private void racketvictor_Click(object sender, EventArgs e)
        {
            ShowPage("racket", "victor");
        }

        private void racketlining_Click(object sender, EventArgs e)
        {
            ShowPage("racket", "lining");
        }
        

        
        //ลูก
        private void button6_Click(object sender, EventArgs e)
        {
            ShowPage("shuttle", "yonex");
        }

        private void shuttlevictor_Click(object sender, EventArgs e)
        {
            ShowPage("shuttle", "victor");
        }

        private void shuttlersl_Click(object sender, EventArgs e)
        {
            ShowPage("shuttle", "rsl");
        }



        //เอ็น
        private void button11_Click(object sender, EventArgs e)
        {
            ShowPage("string", "yonex");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ShowPage("string", "victor");
        }




        //ปุ่มHome เมื่อกดจะนำหน้าโฆษณามาโชว์
        private void button1_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hasd);
            hasd.Show();
        }


        ////// ปุ่มlogin user
        private void button4_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            loginwindow logwin = new loginwindow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(logwin);
            logwin.Show();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        //ปุ่ม logout หากยืนยันจะทำฟังชั่น HideLogoutButton
        private void logoutbutton_Click(object sender, EventArgs e)
        {
            DialogResult logoutcheck = MessageBox.Show("Do you want to Logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (logoutcheck == DialogResult.Yes)
            {
                this.HideLogoutButton();
            }
        }



        //ปุ่มโชว์หน้าตะกร้า
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            Cartwindow cwin = new Cartwindow(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(cwin);
            cwin.Show();
        }


        //ปุ่มโชว์หน้า login admin
        private void admin_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            logadmin loadm = new logadmin() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(loadm);
            loadm.Show();
        }

        //ปุ่มโชว์หน้า รายการสินค้าที่รอการตรวจสอบการชำระเงินสำหรับ user
        private void label3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            userverify uver = new userverify() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(uver);
            uver.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void Formloader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //ปุ่มโชว์หน้าประวัติคำสั่งซื้อสำเร็จ user
        private void label4_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            historyuser hiser = new historyuser() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hiser);
            hiser.Show();
        }

        //ปุ่มโชว์หน้าประวัติคำสั่งซื้อที่ถูกยกเลิก user
        private void label5_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            hisroryrejectuser hisreer = new hisroryrejectuser() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hisreer);
            hisreer.Show();
        }

        //ปุ่มโชว์หน้า เกี่ยวกับผู้พัฒนา
        private void label8_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            aboutdev abdev = new aboutdev() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(abdev);
            abdev.Show();
        }

        //แก้ไขข้อมูล user
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            changeinac chac = new changeinac(){ Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(chac);
            chac.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
