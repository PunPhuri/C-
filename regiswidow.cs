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
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class regiswidow : Form
    {
        //ฟังชั่นเชื่อม sql
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        public regiswidow()
        {
            InitializeComponent();
        }


        //ฟังชั่นส่ง email แจ้งเตือนการลงทะเบียนสำเร็จ
        static void SendEmail(string recipientEmail)
        {
            string fromMail = "pkbadmiintonshop@gmail.com";
            string fromPassword = "dqsd ovcs evka ohcr";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "แจ้งเตือนการสมัครสมาชิก";
            message.To.Add(new MailAddress(recipientEmail));
            message.Body = "<html><body> สวัสดี ตอนนี้คุณได้เป็นสมาชิกร้านค้า Pkbadminton shop ขอขอบคุณการสมัครสมาชิก ขอให้สนุกกับการเลือกซื้อสินค้าในร้านของเรา </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

        }

        //ตรวจสอบเบอร์โทรศัพท์
        private bool IsAllDigits(string str)
        {
            return str.All(char.IsDigit);
        }


        //ปุ่มกด register 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string usnr = username_regis.Text;
                string emr = email_regis.Text;
                string pwr = password_regis.Text;
                string namer = name_regis.Text;
                string telr = tel_regis.Text;
                string addressre = address_regis.Text;

                // Check if any of the required fields are null or empty
                if (string.IsNullOrEmpty(usnr) || string.IsNullOrEmpty(emr) || string.IsNullOrEmpty(pwr) || string.IsNullOrEmpty(namer) || string.IsNullOrEmpty(telr) || string.IsNullOrEmpty(addressre))
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
                    using (MySqlConnection conn = DatabaseConnection())
                    {
                        conn.Open();
                        string queryusername = "SELECT username FROM useraccount WHERE username = @Username";
                        using (MySqlCommand cmd = new MySqlCommand(queryusername, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", usnr);

                            object result = cmd.ExecuteScalar();
                            if (result == null)
                            {
                                string queryemail = "SELECT email FROM useraccount WHERE email = @Email";
                                MySqlCommand cmd1 = new MySqlCommand(queryemail, conn);
                                cmd1.Parameters.AddWithValue("@Email", emr);
                                object result1 = cmd1.ExecuteScalar();
                                if (result1 == null)
                                {
                                    Regex regex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$");
                                    bool isvalid = regex.IsMatch(emr);
                                    if (isvalid) 
                                    {
                                        string queryname = "SELECT name FROM useraccount WHERE name = @Name";
                                        MySqlCommand cmd2 = new MySqlCommand(queryname, conn);
                                        cmd2.Parameters.AddWithValue("@Name", namer);
                                        object result2 = cmd2.ExecuteScalar();
                                        if (result2 == null)
                                        {
                                            string querytel = "SELECT tel FROM useraccount WHERE tel = @Tel";
                                            MySqlCommand cmd3 = new MySqlCommand(querytel, conn);
                                            cmd3.Parameters.AddWithValue("@Tel", telr);
                                            object result3 = cmd3.ExecuteScalar();
                                            if (result3 == null)
                                            {
                                                string sql2 = "INSERT INTO useraccount (username, email, password, name, tel, address) VALUES (@username, @email, @password, @name, @tel, @address)";

                                                MySqlCommand cmdin = new MySqlCommand(sql2, conn);
                                                // Add parameter values
                                                cmdin.Parameters.AddWithValue("@username", usnr);
                                                cmdin.Parameters.AddWithValue("@email", emr);
                                                cmdin.Parameters.AddWithValue("@password", pwr);
                                                cmdin.Parameters.AddWithValue("@name", namer);
                                                cmdin.Parameters.AddWithValue("@tel", telr);
                                                cmdin.Parameters.AddWithValue("@address", addressre);


                                                // Execute command
                                                cmdin.ExecuteNonQuery();

                                                // Display success message or perform any other actions
                                                MessageBox.Show("Register Successful!");

                                                SendEmail(emr);
                                                this.Formloader.Controls.Clear();
                                                loginwindow logwin = new loginwindow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                                                this.Formloader.Controls.Add(logwin);
                                                logwin.Show();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Tel นี้มีในระบบแล้ว");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Name นี้มีในระบบแล้ว");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("กรอก Email ให้ถูกต้อง");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Email นี้มีในระบบแล้ว");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username นี้มีในระบบแล้ว");
                            } 
                        }
                    }
                }
            }
            catch (Exception ex)
                {
                // Handle any exceptions
            MessageBox.Show("Error: " + ex.Message);
            }
        }

        //ปุ่มกลับไปหน้า back กลับไปหน้า login
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            loginwindow logwin = new loginwindow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(logwin);
            logwin.Show();
        }
    }
}
