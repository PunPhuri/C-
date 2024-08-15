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

namespace project
{
    public partial class changewindow2 : Form
    {
        private changewindow1 _changeWindow1;
        private string targetEmail_fromWindow1;
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }


        //รับค่า email มาจากหน้า changewindow1
        public changewindow2(changewindow1 changeWindow1)
        {
            InitializeComponent();
            _changeWindow1 = changeWindow1;
            targetEmail_fromWindow1 = _changeWindow1.target_email;
        }


        //ปุ่มกลับไปหน้า back กลับไปหน้า login
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            loginwindow logwin = new loginwindow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(logwin);
            logwin.Show();
        }


        //ยืนยันการเปลี่ยนรหัสผ่าน โดยตรวจสอบรหัสต้องตรงกัน
        private void button1_Click(object sender, EventArgs e)
        {
            string newpass = textboxnewpass.Text;
            string connewpass = textboxconnewpass.Text;


            if (newpass == connewpass)
            {
                using (MySqlConnection conn = DatabaseConnection())
                {
                    conn.Open();
                    string sqlUpdate = "UPDATE useraccount SET password = @password WHERE email = @email";
                    MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@password", newpass);
                    cmdUpdate.Parameters.AddWithValue("@email", targetEmail_fromWindow1);

                    // Execute command
                    cmdUpdate.ExecuteNonQuery();
                }

                MessageBox.Show("เปลี่ยนรหัสผ่านแล้ว");

                string fromMail = "pkbadmiintonshop@gmail.com";
                string fromPassword = "dqsd ovcs evka ohcr";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "แจ้งเตือนการเปลี่ยนรหัสผ่าน";
                message.To.Add(new MailAddress(targetEmail_fromWindow1));
                message.Body = "<html><body> สวัสดี คุณได้ทำการเปลี่ยนรหัสแล้ว รหัสผ่านใหม่ของคุณคือ : "+ newpass + "</body></html>";
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);

                this.Formloader.Controls.Clear();
                loginwindow logwin = new loginwindow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.Formloader.Controls.Add(logwin);
                logwin.Show();
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ตรงกัน");
            }
        }
    }
}
