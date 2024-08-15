using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static project.loginwindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class historyadmin : Form
    {
        public historyadmin()
        {
            InitializeComponent();

            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM history ";


                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);
                dataorderadmin.DataSource = ds.Tables[0].DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            MySqlConnection conn = new MySqlConnection(connectionString);

            return conn;
        }

        string odid;

        //คลิ๊กเพื่อดึงข้อมูลออกมาโชว์
        private void dataorderadmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataorderadmin.Rows.Count)
            {
                dataorderadmin.CurrentRow.Selected = true;
                odid = dataorderadmin.Rows[e.RowIndex].Cells["order_id"].FormattedValue.ToString();
                comboBox1.Items.Clear();

                MySqlConnection conn = DatabaseConnection();
                DataSet ds = new DataSet();

                conn.Open();

                string queryhistory = "SELECT nameitem, itemcount, priceitem FROM history WHERE order_id = @orderid";
                MySqlCommand cmd9 = new MySqlCommand(queryhistory, conn);
                cmd9.Parameters.AddWithValue("@orderid", odid);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd9);
                adapter.Fill(ds);
                dataitemlistadmin.DataSource = ds.Tables[0].DefaultView;

                string querypic = "SELECT moneyslip FROM history WHERE order_id = @orderid";
                MySqlCommand cmd1 = new MySqlCommand(querypic, conn);
                cmd1.Parameters.AddWithValue("@orderid", odid);

                byte[] imageData1 = (byte[])cmd1.ExecuteScalar();

                if (imageData1 != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageData1))
                    {
                        Bitmap bitmap = new Bitmap(ms);
                        pictureBox3.Image = bitmap;
                    }
                }
                else
                {
                    pictureBox3.Image = null;
                }

                string querytotal = "SELECT totalprice FROM history WHERE order_id = @orderid";
                MySqlCommand cmd2 = new MySqlCommand(querytotal, conn);
                cmd2.Parameters.AddWithValue("@orderid", odid);

                // Execute the query and retrieve the item picture
                object result2 = cmd2.ExecuteScalar();

                label6.Text = result2.ToString();


                string querystatus = "SELECT status FROM history WHERE order_id = @orderid";
                MySqlCommand cmd3 = new MySqlCommand(querystatus, conn);
                cmd3.Parameters.AddWithValue("@orderid", odid);
                object result3 = cmd3.ExecuteScalar();

                string status = result3.ToString();
                comboBox1.Items.Add(status);
                comboBox1.Items.Add("จัดส่งพัสดุแล้ว");
                comboBox1.SelectedIndex = 0;

                string querytrack = "SELECT tracknum FROM history WHERE order_id = @orderid";
                MySqlCommand cmd4 = new MySqlCommand(querytrack, conn);
                cmd4.Parameters.AddWithValue("@orderid", odid);
                object result4 = cmd4.ExecuteScalar();

                textBox1.Text = result4.ToString(); 

                conn.Close();
            }
        }

        //ปุ่มยืนยัน
        private void button1_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = DatabaseConnection();

            conn.Open();

            string querystatus = "UPDATE history SET status = @status, tracknum = @tracknum WHERE order_id = @orderid";
            MySqlCommand cmd3 = new MySqlCommand(querystatus, conn);
            cmd3.Parameters.AddWithValue("@orderid", odid);
            cmd3.Parameters.AddWithValue("@status", comboBox1.SelectedItem.ToString());
            cmd3.Parameters.AddWithValue("@tracknum", textBox1.Text);
            cmd3.ExecuteNonQuery();

            {
                MessageBox.Show("อัปเดตสถานะพัสดุเรียบร้อยแล้ว");
            }

            conn.Close();
        }



        //ค้นหา user
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            comboBox1.Items.Clear();
            textBox1.Clear();
            label6.Text = "0";

            string use = textBox2.Text;

            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM history WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", use);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);
                dataorderadmin.DataSource = ds.Tables[0].DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



        //เคลียร์ user
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            comboBox1.Items.Clear();
            textBox1.Clear();
            label6.Text = "0";
            textBox2.Clear();

            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM history ";


                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);
                dataorderadmin.DataSource = ds.Tables[0].DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
