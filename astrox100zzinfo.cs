using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
using static project.loginwindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class astrox100zzinfo : Form
    {
        string unl = GlobalVariables.userlogin;
        int countnumber = 1;  //จำนวนสินค้า
        int newcountnumber;
        int countstock;
        int pricetocart;  //ราคาสินค้าตามจำนวนชิ้น
        int newpricetocart;

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        private item_frames _itemFrames;
        private Form1 _form1;
        private string _type;
        private string _brand;

        //รับค่าต่างๆจากหน้า item_frames Form1  และรับค่า type brand
        public astrox100zzinfo(item_frames item_Frames, Form1 form1, string type, string brand)
        {
            InitializeComponent();
            _itemFrames = item_Frames;
            _form1 = form1;
            _type = type;
            _brand = brand;

            //ดึงค่า รูปสินค้า ชื่อ ราคา จำนวนสินค้าในสต๊อก 
            pictureBox1.Image = _itemFrames.picture_;
            nameItem_text.Text = _itemFrames.name_item_;
            price_text.Text = Convert.ToString(_itemFrames.price_item_);
            countStock_text.Text = Convert.ToString(_itemFrames.item_count);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        //ปุ่มยืนยันส่งสินค้าเข้าตะกร้า
        private void button1_Click(object sender, EventArgs e)
        {
            if (unl == null || unl.Length == 0)
            {
                MessageBox.Show("คุณต้องทำการ Login ก่อน");
            }
            else
            {
                DialogResult result = MessageBox.Show("คุณต้องการเพิ่มสินค้าเข้าตะกร้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    using (MySqlConnection conn = DatabaseConnection())
                    {
                        conn.Open();

                        pricetocart = _itemFrames.price_item_ * countnumber;

                        string querynewcountitem = "SELECT itemcount FROM cartitem WHERE nameitem = @Nameiteme AND username = @username";
                        MySqlCommand cmd5 = new MySqlCommand(querynewcountitem, conn);
                        cmd5.Parameters.AddWithValue("@Nameiteme", _itemFrames.name_item_);
                        cmd5.Parameters.AddWithValue("@username", unl);
                        object result5 = cmd5.ExecuteScalar();
                        if (result5!=null)
                        {
                            int existingCount = Convert.ToInt32(result5);
                            newcountnumber = existingCount + countnumber;

                            newpricetocart = _itemFrames.price_item_ * newcountnumber;

                            if (newcountnumber > _itemFrames.item_count)
                            {
                                MessageBox.Show("ไม่สามารถเพิ่มสินค้าเพิ่มได้ เนื่องจากคุณมีสินค้าในตะกร้าแล้ว และคำสั่งซื้อใหม่รวมแล้วมากกว่าสินค้าในสต๊อก");
                            }
                            else
                            {
                                string upintocart = "UPDATE cartitem SET itemcount = @itemcount ,priceitem = @priceitem WHERE username = @username AND nameitem = @nameitem";

                                MySqlCommand cmdupintocart = new MySqlCommand(upintocart, conn);
                                // Add parameter values
                                cmdupintocart.Parameters.AddWithValue("@username", unl);
                                cmdupintocart.Parameters.AddWithValue("@nameitem", _itemFrames.name_item_);
                                cmdupintocart.Parameters.AddWithValue("@itemcount", newcountnumber);
                                cmdupintocart.Parameters.AddWithValue("@priceitem", newpricetocart);

                                cmdupintocart.ExecuteNonQuery();
                                MessageBox.Show("เพิ่มสินค้าเข้าตะกร้าแล้ว");
                                _form1.shownoti();
                            }
                            
                        }
                        else
                        {
                            string intocart = "INSERT INTO cartitem (username, nameitem, itemcount, priceitem) VALUES (@username, @nameitem, @itemcount, @priceitem)";

                            MySqlCommand cmdintocart = new MySqlCommand(intocart, conn);
                            // Add parameter values
                            cmdintocart.Parameters.AddWithValue("@username", unl);
                            cmdintocart.Parameters.AddWithValue("@nameitem", _itemFrames.name_item_);
                            cmdintocart.Parameters.AddWithValue("@itemcount", countnumber);
                            cmdintocart.Parameters.AddWithValue("@priceitem", pricetocart);

                            cmdintocart.ExecuteNonQuery();
                            MessageBox.Show("เพิ่มสินค้าเข้าตะกร้าแล้ว");
                            _form1.shownoti();
                        }  
                    }
                }
            }
        }

        //ฟังชั่นดึงค่า จำนวนสินค้ามนสต๊อก รูปspec ตามชื่อของสินค้า
        private void astrox100zzinfo_Load(object sender, EventArgs e)
        {
            
            using (MySqlConnection conn = DatabaseConnection())
            {
                conn.Open();
                string querycountitem = "SELECT countitem FROM iteminfo WHERE nameitem = @Nameiteme";
                MySqlCommand cmd1 = new MySqlCommand(querycountitem, conn);
                cmd1.Parameters.AddWithValue("@Nameiteme", _itemFrames.name_item_);
                object result1 = cmd1.ExecuteScalar();
                countstock = (result1 != null) ? Convert.ToInt32(result1) : 0;
                label1.Text = countstock.ToString();

                
                string queryspec = "SELECT specpic FROM iteminfo WHERE nameitem = @Nameiteme";
                MySqlCommand cmd2 = new MySqlCommand(queryspec, conn);
                cmd2.Parameters.AddWithValue("@Nameiteme", _itemFrames.name_item_);
                byte[] imageData = (byte[])cmd2.ExecuteScalar(); 

                ImageConverter converter = new ImageConverter();
                Image imagespec = (Image)converter.ConvertFrom(imageData);
                pictureBox2.Image = imagespec;

            }

            numitemcount.Text = "1";
        }


        //ปุ่มกลับไปหน้ากดหน้า โดยสร้างตาม type brand ก่อนหน้าที่ได้รับมา
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            racketyonex racketyonex = new racketyonex(_form1, _type, _brand);
            _form1.showpage(racketyonex);
        }
        

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //ปุ่มเพิ่มสินค้า
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(countnumber == _itemFrames.item_count)
            {
                MessageBox.Show("ไม่สามารถเพิ่มสินค้าเพิ่มได้");
            }
            else
            {
                countnumber++;
                numitemcount.Text = countnumber.ToString();
            }  
        }

        //ปุ่มลดสินค้า
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (countnumber > 1)
            {
                countnumber--;
                numitemcount.Text = countnumber.ToString();
            }
            else
            {
                MessageBox.Show("ไม่สามารถลดสินค้า");
            }
        }
    }   
}
