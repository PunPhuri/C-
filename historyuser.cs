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

namespace project
{
    public partial class historyuser : Form
    {
        public historyuser()
        {
            InitializeComponent();

            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice , status FROM history WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", GlobalVariables.userlogin);


                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);
                dataorder.DataSource = ds.Tables[0].DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        

        private void dataorderhistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        string odid;
        private void dataorder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < dataorder.Rows.Count)
            {
                dataorder.CurrentRow.Selected = true;
                odid = dataorder.Rows[e.RowIndex].Cells["order_id"].FormattedValue.ToString();

                MySqlConnection conn = DatabaseConnection();
                DataSet ds = new DataSet();

                conn.Open();

                string queryhistory = "SELECT nameitem, itemcount, priceitem FROM history WHERE order_id = @orderid";
                MySqlCommand cmd9 = new MySqlCommand(queryhistory, conn);
                cmd9.Parameters.AddWithValue("@orderid", odid);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd9);
                adapter.Fill(ds);
                dataitemlist.DataSource = ds.Tables[0].DefaultView;

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

                // Execute the query and retrieve the item picture
                object result3 = cmd3.ExecuteScalar();
                label7.Text = result3.ToString();

                string querytrack = "SELECT tracknum FROM history WHERE order_id = @orderid";
                MySqlCommand cmd4 = new MySqlCommand(querytrack, conn);
                cmd4.Parameters.AddWithValue("@orderid", odid);

                object result4 = cmd4.ExecuteScalar();
                label9.Text = result4.ToString();



                conn.Close();
            }
        }
    }
}
