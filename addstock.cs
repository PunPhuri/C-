using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class addstock : Form
    {
        public addstock()
        {
            InitializeComponent();

            MySqlConnection conn = DatabaseConnection();

            DataSet ds = new DataSet();

            try
            {

                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM iteminfo ";


                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);
                dataitem.DataSource = ds.Tables[0].DefaultView;
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


        string itid;   //itemid


        //สามารถคลิ๊กเพื่อดึงข้อมูลสินค้าออกมาโชว์
        private void dataitem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataitem.Rows.Count)
            {
                dataitem.CurrentRow.Selected = true;
                itid = dataitem.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();

                MySqlConnection conn = DatabaseConnection();
                DataSet ds = new DataSet();

                conn.Open();

                string queryprice = "SELECT priceitem FROM iteminfo WHERE id = @itemid";
                MySqlCommand cmd2 = new MySqlCommand(queryprice, conn);
                cmd2.Parameters.AddWithValue("@itemid", itid);
                object result2 = cmd2.ExecuteScalar();

                textBox1.Text = result2.ToString();



                string querycount = "SELECT countitem FROM iteminfo WHERE id = @itemid";
                MySqlCommand cmd3 = new MySqlCommand(querycount, conn);
                cmd3.Parameters.AddWithValue("@itemid", itid);

                object result3 = cmd3.ExecuteScalar();
                textBox2.Text = result3.ToString();

            }
        }



        //ยืนยันการอัพเดตสินค้า

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คูณต้องการอัพเดตข้อมูลสินค้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MySqlConnection conn = DatabaseConnection();

                conn.Open();

                string query = "UPDATE iteminfo SET countitem = @countitem, priceitem = @priceitem WHERE id = @itemid";
                MySqlCommand cmd3 = new MySqlCommand(query, conn);
                cmd3.Parameters.AddWithValue("@itemid", itid);
                cmd3.Parameters.AddWithValue("@priceitem", textBox1.Text);
                cmd3.Parameters.AddWithValue("@countitem", textBox2.Text);
                cmd3.ExecuteNonQuery();

                {
                    MessageBox.Show("อัปเดตข้อมูลพัสดุเรียบร้อยแล้ว");
                }

                conn.Close();
            }
        }


        //ลบสินค้า
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คูรต้องการลบข้อมูลสินค้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MySqlConnection conn = DatabaseConnection();

                conn.Open();

                string queryde = "DELETE FROM iteminfo WHERE id = @itemid";
                MySqlCommand cmd4 = new MySqlCommand(queryde, conn);
                cmd4.Parameters.AddWithValue("@itemid", itid);
                cmd4.ExecuteNonQuery();

                {
                    MessageBox.Show("ลบข้อมูลพัสดุเรียบร้อยแล้ว");
                }

                conn.Close();
            }
        }
    }
}
