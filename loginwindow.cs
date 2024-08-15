using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace project
{
    public partial class loginwindow : Form
    {
        //ฟังชั่นรับส่งค่า userlogin ไปยัง form อื่นๆ
        public static class GlobalVariables
        {
            public static string userlogin { get; set; }
        }

        public static loginwindow instance;
        public Label usenamelog;


        public loginwindow()
        {
            InitializeComponent();
            instance = this;
        }

        //ฟังชั่นปุ่มกด ตรวจสอบการ login
        private void button1_Click(object sender, EventArgs e)
        {
            string mysqlcon = "server=127.0.0.1;port=3306;user=root;password=;database=pkdatabase";

            using (MySqlConnection mySqlConnection = new MySqlConnection(mysqlcon))
            {
                string usernameToSearch = textBox1.Text;
                string passwordToSearch = textBox2.Text;

                mySqlConnection.Open();

                string queryusername = "SELECT username FROM useraccount WHERE username = @Username";
                MySqlCommand cmd1 = new MySqlCommand(queryusername, mySqlConnection);
                cmd1.Parameters.AddWithValue("@Username", usernameToSearch);
                object result1 = cmd1.ExecuteScalar();

                if (result1 != null)
                {
                    // Username found, proceed with password validation
                    string query = "SELECT password FROM useraccount WHERE username = @Username";
                    using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@Username", usernameToSearch);
                        object result = cmd.ExecuteScalar();

                        if (result != null && passwordToSearch.Equals(result.ToString()))
                        {
                            // Password matches, login successful
                            MessageBox.Show("Login Successful!\n" + "\nUsername : " + usernameToSearch);
                            GlobalVariables.userlogin = result1.ToString();
                            Form1.instance.usenamelog.Text = result1.ToString();
                            Form1.instance.HideLoginButton();

                            this.Formloader.Controls.Clear();
                            homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                            this.Formloader.Controls.Add(hasd);
                            hasd.Show();
                        }
                        else
                        {
                            MessageBox.Show("Username or Password is wrong.");
                        }
                    }
                }
                else
                {
                    // Username not found, check by email
                    string queryemail = "SELECT username FROM useraccount WHERE email = @Email";
                    MySqlCommand cmd2 = new MySqlCommand(queryemail, mySqlConnection);
                    cmd2.Parameters.AddWithValue("@Email", usernameToSearch);
                    object result2 = cmd2.ExecuteScalar();

                    if (result2 != null)
                    {
                        // Email found, proceed with password validation
                        string query = "SELECT password FROM useraccount WHERE username = @Username";
                        using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@Username", result2.ToString());
                            object result = cmd.ExecuteScalar();

                            if (result != null && passwordToSearch.Equals(result.ToString()))
                            {
                                // Password matches, login successful
                                MessageBox.Show("Login Successful!\n" + "\nUsername : " + result2.ToString());
                                GlobalVariables.userlogin = result2.ToString();
                                Form1.instance.usenamelog.Text = result2.ToString();
                                Form1.instance.HideLoginButton();

                                this.Formloader.Controls.Clear();
                                homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                                this.Formloader.Controls.Add(hasd);
                                hasd.Show();
                            }
                            else
                            {
                                MessageBox.Show("Username or Password is wrong.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is wrong.");
                    }
                }
            }
        }


        //ปุ่มโชว์ลงทะเบียน
        private void label1_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            regiswidow rewin = new regiswidow() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(rewin);
            rewin.Show();
        }

        //ปุ่มโชว์หน้าลืมรหัสผ่าน
        private void label2_Click(object sender, EventArgs e)
        {
            this.Formloader.Controls.Clear();
            changewindow1 chwin1 = new changewindow1() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Formloader.Controls.Add(chwin1);
            chwin1.Show();
        }
    }
}
