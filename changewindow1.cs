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

namespace project
{
    public partial class changewindow1 : Form
    {
        public string target_email;

        public changewindow1()
        {
            InitializeComponent();
        }

        //ปุ่มกลับไปหน้า back กลับไปหน้า login
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            loginwindow logwin = new loginwindow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(logwin);
            logwin.Show();
        }


        //กำหนดตัวแปรเก็บค่าเลข OTP
        private string otpnumber;


        //สุ่มเลข otp 6 หลัก และส่งไปยัง email
        private void button2_Click(object sender, EventArgs e)
        {
            string recipientEmail = textboxemail.Text;
            target_email = recipientEmail;


    
            // ramdom เลข otp 6 หลัก
            Random random = new Random();
            otpnumber = "";

            for (int i = 0; i < 6; i++)
            {
                int digit = random.Next(0, 10); // Generates a random integer between 0 and 9
                otpnumber += digit.ToString(); // Concatenate the digit to the sequence
            }

            string fromMail = "pkbadmiintonshop@gmail.com";
            string fromPassword = "dqsd ovcs evka ohcr";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "รหัสกู้คืนบัญชีร้าน PKbadminton";
            message.To.Add(new MailAddress(recipientEmail));
            message.Body = "<html><body> สวัสดี เราได้รับคำขอให้รีเซ็ตรหัสผ่านของคุณ\nป้อนรหัสรีเซ็ตรหัสผ่านต่อไปนี้\nรหัส OTP : "+ otpnumber +  "</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

            MessageBox.Show("ส่ง OTP ไปยังอีเมลแล้ว");
            
        }



        private int incorrectAttempts = 0;
        //ปุ่มตรวจสอบ เลข otp ถ้าตรงจะไปหน้าเปลี่ยนรหัสผ่าน
        private void button1_Click(object sender, EventArgs e)
        {
            string checkotp = textboxotp.Text;
            
            // Check if the entered OTP matches the generated OTP
            if (checkotp == otpnumber)
            {
                this.Formloader.Controls.Clear();
                changewindow2 chwin2 = new changewindow2(this) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.Formloader.Controls.Add(chwin2);
                chwin2.Show();
            }
            else
            {
                incorrectAttempts++;

                // Check if incorrect attempts reach 3
                if (incorrectAttempts >= 3)
                {
                    MessageBox.Show("คุณป้อน OTP ไม่ถูกต้อง 3 ครั้งแล้ว  ระบบได้ทำการส่งรหัส OTP ใหม่ให้คุณ โปรดตรวจสอบ Email");

                    button2.PerformClick();
                    incorrectAttempts = 0; 
                }
                else
                {
                    MessageBox.Show("OTP ไม่ถูกต้อง");
                }
            }
        }
    }
}
