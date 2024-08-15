using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static project.loginwindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace project
{

    public partial class userverify : Form
    {
        public userverify()
        {
            InitializeComponent();

            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM orderverify WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", GlobalVariables.userlogin);


                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);
                dataorder.DataSource = ds.Tables[0].DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
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



        private void userverify_Load(object sender, EventArgs e)
        {

        }

        
        private void dataorder1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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


                // Create MySqlCommand to execute the SQL query
                string queryverify = "SELECT nameitem,itemcount,priceitem FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd4 = new MySqlCommand(queryverify, conn);
                cmd4.Parameters.AddWithValue("@orderid", odid);

                // Execute the query and retrieve the item picture
                object result1 = cmd4.ExecuteScalar();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd4);
                adapter.Fill(ds);



                dataitemlist.DataSource = ds.Tables[0].DefaultView;

                string querypic = "SELECT moneyslip FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd1 = new MySqlCommand(querypic, conn);
                cmd1.Parameters.AddWithValue("@orderid", odid);
                byte[] imageData = (byte[])cmd1.ExecuteScalar();

                // Display money slip image
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Bitmap bitmap = new Bitmap(ms);
                    pictureBox3.Image = bitmap;
                }
                

                string querytotal = "SELECT totalprice FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd2 = new MySqlCommand(querytotal, conn);
                cmd2.Parameters.AddWithValue("@orderid", odid);

                // Execute the query and retrieve the item picture
                object result2 = cmd2.ExecuteScalar();

                label6.Text = result2.ToString();

                conn.Close();
            }
        }
    }
}
