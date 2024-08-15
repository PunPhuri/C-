using iTextSharp.text;
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
using static System.Net.Mime.MediaTypeNames;

namespace project
{
    public partial class additemad : Form
    {
        public additemad()
        {
            InitializeComponent();
        }
        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }


        //ปุ่มเพิ่มรูปสินค้า
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter to allow only image files
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            // Show the dialog
            DialogResult result = openFileDialog.ShowDialog();

            // If the user selects an image file
            if (result == DialogResult.OK)
            {
                try
                {
                    // Get the selected file name and display it in a TextBox (optional)
                    string selectedFileName1 = openFileDialog.FileName;
                    //textBoxSelectedFile.Text = selectedFileName;

                    // Load the image from the selected file
                    // Load the image from the selected file
                    System.Drawing.Image image1 = System.Drawing.Image.FromFile(selectedFileName1);


                    // Display the image in the PictureBox
                    pictureBox1.Image = image1;
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur while loading the image
                    MessageBox.Show("Error loading the image: " + ex.Message);
                }
            }
        }


        //ปุ่มเพิ่มรูปสเปคสินค้า
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter to allow only image files
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            // Show the dialog
            DialogResult result = openFileDialog.ShowDialog();

            // If the user selects an image file
            if (result == DialogResult.OK)
            {
                try
                {
                    // Get the selected file name and display it in a TextBox (optional)
                    string selectedFileName2 = openFileDialog.FileName;
                    //textBoxSelectedFile.Text = selectedFileName;

                    // Load the image from the selected file
                    // Load the image from the selected file
                    System.Drawing.Image image2 = System.Drawing.Image.FromFile(selectedFileName2);


                    // Display the image in the PictureBox
                    pictureBox2.Image = image2;
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur while loading the image
                    MessageBox.Show("Error loading the image: " + ex.Message);
                }
            }
        }

        //เริ่มหน้าโดยให้ประเภทสินค้ามี 3 ประเภท
        private void additemad_Load(object sender, EventArgs e)
        {
            combotypeadd.Items.Add("racket");
            combotypeadd.Items.Add("shuttle");
            combotypeadd.Items.Add("string");
        }

        //หาก เลือกประเภทใด  ยี่ห้อจะมีตามประเภทนั้น
        private void combotypeadd_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobrandadd.Items.Clear();

            if (combotypeadd.SelectedItem.ToString() == "racket")
            {
                combobrandadd.Items.Add("yonex");
                combobrandadd.Items.Add("victor");
                combobrandadd.Items.Add("lining");
            }

            if (combotypeadd.SelectedItem.ToString() == "shuttle")
            {
                combobrandadd.Items.Add("yonex");
                combobrandadd.Items.Add("victor");
                combobrandadd.Items.Add("rsl");
            }

            if (combotypeadd.SelectedItem.ToString() == "string")
            {
                combobrandadd.Items.Add("yonex");
                combobrandadd.Items.Add("victor");
            }
        }


        //ปุ่มเพิ่มสินค้าเข้าDB
        private void button1_Click(object sender, EventArgs e)
        {
            string _nameitemadd = nameitemadd.Text;
            string _countitemadd = countitemadd.Text;
            string _priceitemadd = priceitemadd.Text;

            string _brand = combobrandadd.SelectedItem != null ? combobrandadd.SelectedItem.ToString() : "";
            string _type = combotypeadd.SelectedItem != null ? combotypeadd.SelectedItem.ToString() : "";

            System.Drawing.Image imageitem = pictureBox1.Image;

            // Convert the image to a byte array
            byte[] imageitemBytes = null;
            if (imageitem != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageitem.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Assuming the image format is JPEG
                    imageitemBytes = ms.ToArray();
                }
            }

            System.Drawing.Image imagespec = pictureBox2.Image;

            // Convert the image to a byte array
            byte[] imagespecBytes = null;
            if (imagespec != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imagespec.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Assuming the image format is JPEG
                    imagespecBytes = ms.ToArray();
                }
            }

            if (string.IsNullOrEmpty(_nameitemadd) || string.IsNullOrEmpty(_countitemadd) || string.IsNullOrEmpty(_priceitemadd))
            {
                MessageBox.Show("กรุณากรอกข้อมูลทั้งหมด");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("คุณต้องการเพิ่มข้อมูลสินค้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = DatabaseConnection())
                    {
                        conn.Open();
                        string insertQuery = "INSERT INTO iteminfo (nameitem, countitem, priceitem, branditem, type, itempic, specpic) VALUES (@Name, @Count, @Price, @brand, @type, @Image1, @Image2)";

                        MySqlCommand command = new MySqlCommand(insertQuery, conn);
                        command.Parameters.AddWithValue("@Name", _nameitemadd);
                        command.Parameters.AddWithValue("@Count", _countitemadd);
                        command.Parameters.AddWithValue("@Price", _priceitemadd);
                        command.Parameters.AddWithValue("@brand", _brand);
                        command.Parameters.AddWithValue("@type", _type);
                        command.Parameters.AddWithValue("@Image1", imageitemBytes); // Convert image to byte array
                        command.Parameters.AddWithValue("@Image2", imagespecBytes); // Convert image to byte array

                        command.ExecuteNonQuery();

                        MessageBox.Show("เพิ่มข้อมูลสินค้า " + _nameitemadd + " เรียบร้อยแล้ว");
                        nameitemadd.Clear();
                        countitemadd.Clear();
                        priceitemadd.Clear();
                        combotypeadd.Text = "";
                        combobrandadd.Text = ""; // Clear the text, not the items
                        pictureBox1.Image = null;
                        pictureBox2.Image = null;
                    }
                }
            }

        }
    }
}
