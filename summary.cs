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

namespace project
{
    public partial class summary : Form
    {
        
        public summary()
        {
            InitializeComponent();
            MySqlConnection conn = DatabaseConnection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        private void summary_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("");
            for (int i = 1; i <= 31; i++)
            {
                comboBox1.Items.Add(i.ToString("00"));
            }

            comboBox2.Items.Add("");
            for (int i = 1; i <= 12; i++)
            {
                comboBox2.Items.Add(i.ToString("00"));
            }

            comboBox3.Items.Add("");
            for (int year = 2567; year <= 2587; year++)
            {
                comboBox3.Items.Add(year.ToString());
            }
        }

        string conjunction;

        //ปุ่มยืนยันการเลือกช่วงที่ดู
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = DatabaseConnection();
            string day = comboBox1.SelectedItem?.ToString();
            string month = comboBox2.SelectedItem?.ToString();
            string year = comboBox3.SelectedItem?.ToString();

            string query = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM history WHERE ";
            string query1 = "SELECT nameitem FROM history WHERE ";

            if (!string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                query += $"dateorder = @day";
                query1 += $"dateorder = @day";
                conjunction = " AND ";
            }
            else if (string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                query += $"month = @month AND year = @year";
                query1 += $"month = @month AND year = @year";
                conjunction = " AND ";
            }
            else if (string.IsNullOrEmpty(day) && string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
            {
                query += $"year = @year";
                query1 += $"year = @year";
                conjunction = " AND ";
            }

            List<string> nameItems = new List<string>();

            try
            {
                conn.Open();

                // Create a MySqlCommand to execute the SQL query
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlCommand cmd1 = new MySqlCommand(query1, conn);

                if (!string.IsNullOrEmpty(day))
                {
                    cmd.Parameters.AddWithValue("@day", $"{day}-{month}-{year}"); // Assuming date format is yyyy-MM-dd

                    cmd1.Parameters.AddWithValue("@day", $"{day}-{month}-{year}");
                }
                if (string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);

                    cmd1.Parameters.AddWithValue("@month", month);
                    cmd1.Parameters.AddWithValue("@year", year);
                }
                if (string.IsNullOrEmpty(day) && string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(year))
                {
                    cmd.Parameters.AddWithValue("@year", year);

                    cmd1.Parameters.AddWithValue("@year", year);
                }

                using (MySqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nameItems.Add(reader["nameitem"].ToString());
                    }
                }

                Dictionary<string, int> nameItemCount = new Dictionary<string, int>();

                // นับจำนวนการเกิดของแต่ละ nameitem
                foreach (var item in nameItems)
                {
                    if (nameItemCount.ContainsKey(item))
                    {
                        nameItemCount[item]++;
                    }
                    else
                    {
                        nameItemCount[item] = 1;
                    }
                }

                // หา nameitem ที่ซ้ำกันมากที่สุด
                var topItems = nameItemCount.OrderByDescending(x => x.Value).Take(5).ToList();
                label9.Text = "";
                // แสดงผลลัพธ์ใน Label9
                if (topItems.Count > 0)
                {
                    for (int i = 0; i < topItems.Count; i++)
                    {
                        label9.Text += $"{i + 1}. {topItems[i].Key}  /  {topItems[i].Value} ชิ้น\n";
                    }
                }



                DataTable ds = new DataTable();

                // Fill the DataTable with data from the database
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);

                // Bind the DataTable to the DataGridView
                dataorderadmin.DataSource = ds.DefaultView;


               


                // Calculate the total sum
                int totalsumPrice = 0;

                // Iterate over each row in the dataset
                foreach (DataRow row in ds.Rows)
                {
                    // Check if the totalprice column value is not DBNull
                    if (row["totalprice"] != DBNull.Value)
                    {
                        // Convert the totalprice value to an integer and add it to the total price
                        totalsumPrice += Convert.ToInt32(row["totalprice"]);
                    }
                }

                // Display the total sum
                label6.Text = totalsumPrice.ToString();
            }
            catch
            {
                // Handle any exceptions that may occur
                MessageBox.Show("กรอก วัน เดือน ปี ให้ครบถ้วน");
            }
            finally
            {
                // Close the connection
                conn.Close();
            }

        }
    }
}
