using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace project
{
    public partial class racketyonex : Form
    {

        //รับค่าต่างๆจาก Form1 เพื่อสร้างหน้าต่างโชว์สินค้าตาม type brand

        Form1 _form1;
        private string _type;
        private string _brand;
        public racketyonex(Form1 form1,string type, string brand)
        {
            InitializeComponent();
            _form1 = form1;
            _type = type;
            _brand = brand;

            label1.Text = _brand + " " + _type;
        }

       
        private void panel5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Click(object sender, EventArgs e)
        {

        }

        private void panel12_Click(object sender, EventArgs e)
        {

        }

        private void panel14_Click(object sender, EventArgs e)
        {

        }

        private void panel18_Click(object sender, EventArgs e)
        {

        }

        private void panel20_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //คำสั่งใช้ฟังชั่น item_show
        private void racketyonex_Load(object sender, EventArgs e)
        {
            item_show();
        }

        //ฟังชั่นดึงข้อมูลจาก sql ออกมาโชว์ใน item_frames และส่งมาที่ flowLayoutPanel1  loop สร้างไปเรื่อยๆจนครบทั้ง sql ตาม type brand ที่ได้รับมา
        private void item_show()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";
            MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                con.Open();

                MySqlCommand countCommand = new MySqlCommand("SELECT COUNT(nameitem) FROM iteminfo;", con);
                int count_item = Convert.ToInt32(countCommand.ExecuteScalar());

                MySqlCommand Show_item = new MySqlCommand("SELECT * FROM iteminfo WHERE branditem = @branditem AND type = @type;", con);
                Show_item.Parameters.AddWithValue("@branditem", _brand);
                Show_item.Parameters.AddWithValue("@type", _type);



                using (MySqlDataReader reader = Show_item.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item_frames item_Frame = new item_frames(_form1,_type,_brand);
                        item_Frame.Show();

                        item_Frame.name_item_ = reader.GetString(1);
                        item_Frame.item_count = reader.GetInt32(2);
                        item_Frame.price_item_ = reader.GetInt32(3);

                        using (MemoryStream ms = new MemoryStream((byte[])reader["itempic"]))
                        {
                            item_Frame.picture_ = new Bitmap(ms);
                        }

                        flowLayoutPanel1.Controls.Add(item_Frame);

                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
