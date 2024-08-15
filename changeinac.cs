using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static project.loginwindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class changeinac : Form
    {

        public string unl = GlobalVariables.userlogin;

        public changeinac()
        {
            InitializeComponent();


            MySqlConnection conn = DatabaseConnection();
            conn.Open();

            label1.Text = GlobalVariables.userlogin.ToString();

            
            string queryemail = "SELECT email FROM useraccount WHERE username = @username";
            MySqlCommand cmd2 = new MySqlCommand(queryemail, conn);
            cmd2.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
            object result2 = cmd2.ExecuteScalar();

            email_regis.Text = result2.ToString();


            string queryname = "SELECT name FROM useraccount WHERE username = @username";
            MySqlCommand cmd3 = new MySqlCommand(queryname, conn);
            cmd3.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
            object result3 = cmd3.ExecuteScalar();

            name_regis.Text = result3.ToString();

            string querytel = "SELECT tel FROM useraccount WHERE username = @username";
            MySqlCommand cmd4 = new MySqlCommand(querytel, conn);
            cmd4.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
            object result4 = cmd4.ExecuteScalar();

            tel_regis.Text = result4.ToString();

            string queryaddress = "SELECT address FROM useraccount WHERE username = @username";
            MySqlCommand cmd5 = new MySqlCommand(queryaddress, conn);
            cmd5.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
            object result5 = cmd5.ExecuteScalar();

            address_regis.Text = result5.ToString();

            string querypassword = "SELECT password FROM useraccount WHERE username = @username";
            MySqlCommand cmd6 = new MySqlCommand(querypassword, conn);
            cmd6.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
            object result6 = cmd6.ExecuteScalar();

            password_regis.Text = result6.ToString();
            
        }

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        static void SendEmail(string recipientEmail)
        {
            string fromMail = "pkbadmiintonshop@gmail.com";
            string fromPassword = "dqsd ovcs evka ohcr";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "แจ้งเตือนการเปลี่ยนแปลงข้อมูลลูกค้า";
            message.To.Add(new MailAddress(recipientEmail));
            message.Body = "<html><body> สวัสดี ตอนนี้คุณได้เปลี่ยนข้อมูลส่วนตัวของคุณแล้ว </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

        }

        private bool IsAllDigits(string str)
        {
            return str.All(char.IsDigit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string emr = email_regis.Text;
                string pwr = password_regis.Text;
                string namer = name_regis.Text;
                string telr = tel_regis.Text;
                string addressre = address_regis.Text;

                // Check if any of the required fields are null or empty
                if (string.IsNullOrEmpty(emr) || string.IsNullOrEmpty(pwr) || string.IsNullOrEmpty(namer) || string.IsNullOrEmpty(telr) || string.IsNullOrEmpty(addressre))
                {
                    MessageBox.Show("กรุณากรอกข้อมูลทั้งหมด");
                    return;
                }
                else if (!IsAllDigits(telr) || telr.Length != 10)
                {
                    MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์ให้ถูกต้อง");
                    return;
                }

                else
                {
                    
                    if (emr.EndsWith(".com"))
                    {
                        MySqlConnection conn = DatabaseConnection();

                        conn.Open();

                        string query = "UPDATE useraccount SET name = @name, email = @email, password = @password, tel = @tel , address = @address WHERE username = @username";
                        MySqlCommand cmd3 = new MySqlCommand(query, conn);
                        cmd3.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
                        cmd3.Parameters.AddWithValue("@name", namer);
                        cmd3.Parameters.AddWithValue("@email", emr);
                        cmd3.Parameters.AddWithValue("@password", pwr);
                        cmd3.Parameters.AddWithValue("@tel", telr);
                        cmd3.Parameters.AddWithValue("@address", addressre);
                        cmd3.ExecuteNonQuery();

                        {
                            MessageBox.Show("อัปเดตข้อมูลเรียบร้อยแล้ว");
                        }

                        SendEmail(emr);
                        this.Formloader.Controls.Clear();
                        homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                        this.Formloader.Controls.Add(hasd);
                        hasd.Show();

                    }
                    else
                    {
                        MessageBox.Show("กรอก Email ให้ถูกต้อง");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        //ปุ่มกลับ
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(hasd);
            hasd.Show();
        }
    }
}
