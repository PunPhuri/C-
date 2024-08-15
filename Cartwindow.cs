using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static project.loginwindow;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace project
{
    public partial class Cartwindow : Form
    {
        int countstock;
        int pricetocart;
        string nit;

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        private Form1 _form1;

        public Cartwindow(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            showcartitem();

            MySqlConnection conn = DatabaseConnection();

            // Create a DataSet to store the data
            DataSet ds = new DataSet();

            try
            {
                // Open the connection
                conn.Open();

                // Create a MySqlCommand to execute the SQL query
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM cartitem WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", GlobalVariables.userlogin);

                // Create a MySqlDataAdapter to fill the DataSet with data
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                // Fill the DataSet with data from the database
                adapter.Fill(ds);

                // Bind the DataGridView to the DataTable in the DataSet
                datacart1.DataSource = ds.Tables[0].DefaultView;
            }
            catch (MySqlException ex)
            {
                // Handle any exceptions that may occur
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //คลิ๊กรายการสินค้าเพื่อดึงข้อมูลมาโชว์
        private void datacart1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < datacart1.Rows.Count)
            {
                datacart1.CurrentRow.Selected = true;
                label1.Text = datacart1.Rows[e.RowIndex].Cells["nameitem"].FormattedValue.ToString();
                textBox1.Text = datacart1.Rows[e.RowIndex].Cells["itemcount"].FormattedValue.ToString();
                nit = label1.Text;

                MySqlConnection conn = DatabaseConnection();
                DataSet ds = new DataSet();
                conn.Open();

                string querypic = "SELECT itempic FROM iteminfo WHERE nameitem = @Nameitem";
                MySqlCommand cmd4 = new MySqlCommand(querypic, conn);
                cmd4.Parameters.AddWithValue("@Nameitem", nit);
                byte[] imageData = (byte[])cmd4.ExecuteScalar();
                using (MemoryStream ms = new MemoryStream(imageData))
                {

                    Bitmap bitmap = new Bitmap(ms);

                    pictureBox1.Image = bitmap;
                }

                string querycountitem = "SELECT countitem FROM iteminfo WHERE nameitem = @Nameitem";
                MySqlCommand cmd1 = new MySqlCommand(querycountitem, conn);
                cmd1.Parameters.AddWithValue("@Nameitem", nit); // Use @Nameitem instead of @Nameiteme
                object result1 = cmd1.ExecuteScalar();
                countstock = (result1 != null && result1 != DBNull.Value) ? Convert.ToInt32(result1) : 0;
                label5.Text = countstock.ToString();
            }
        }

        //อัพเดทตาราง
        private void showcartitem()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM cartitem WHERE username = @username";
            cmd.Parameters.AddWithValue("@username", GlobalVariables.userlogin);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            datacart1.DataSource = ds.Tables[0].DefaultView;

            int totalPrice = 0;

            // Iterate over each row in the dataset
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Check if the priceitem column value is not DBNull
                if (row["priceitem"] != DBNull.Value)
                {
                    // Convert the priceitem value to an integer and add it to the total price
                    totalPrice += Convert.ToInt32(row["priceitem"]);
                    label6.Text = totalPrice.ToString();
                    string imageUrl = "https://promptpay.io/0902830599/" + totalPrice.ToString() + ".png";
                    DisplayImage(imageUrl);
                }
            }
        }


        //ฟังชั่นแก้ไขจำนวนสินค้า
        private void UpdateCartItem()
        {
            using (MySqlConnection conn = DatabaseConnection())
            {
                conn.Open();
                string queryprice = "SELECT priceitem FROM iteminfo WHERE nameitem = @Nameitem";
                MySqlCommand cmd2 = new MySqlCommand(queryprice, conn);
                cmd2.Parameters.AddWithValue("@Nameitem", nit);
                object result2 = cmd2.ExecuteScalar();
                int pricecart = (result2 != null) ? Convert.ToInt32(result2) : 0;


                string querycountitem = "SELECT countitem FROM iteminfo WHERE nameitem = @Nameitem";
                MySqlCommand cmd1 = new MySqlCommand(querycountitem, conn);
                cmd1.Parameters.AddWithValue("@Nameitem", nit); // Use @Nameitem instead of @Nameiteme
                object result1 = cmd1.ExecuteScalar();
                countstock = (result1 != null && result1 != DBNull.Value) ? Convert.ToInt32(result1) : 0;


                if (!int.TryParse(textBox1.Text, out int itemCount))
                {
                    MessageBox.Show("กรุณาใส่จำนวนสินค้าให้ถูกต้อง");
                }
                else if (itemCount > countstock)
                {
                    MessageBox.Show("ไม่สามารถเพิ่มสินค้าได้ เนื่องจากจำนวนสินค้าที่เลือกมากกว่าจำนวนที่มีในสต๊อก");
                }
                else if (itemCount <= 0)
                {
                    DialogResult result = MessageBox.Show("คุณต้องการลบสินค้าออกจากตะกร้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        int selectedRow = datacart1.CurrentCell.RowIndex;
                        int fixid = Convert.ToInt32(datacart1.Rows[selectedRow].Cells["id"].Value);

                        string sql = "DELETE FROM cartitem WHERE id = @FixId";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@FixId", fixid);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("ลบสินค้าออกจากตะกร้าเรียบร้อยแล้ว");
                            showcartitem();
                            textBox1.Clear();
                            label5.Text = null;
                            label1.Text = null;
                            pictureBox1.Image = null;
                            _form1.shownoti();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data");
                        }
                    }

                }
                else
                {
                    pricetocart = pricecart * itemCount;
                    int selectedRow = datacart1.CurrentCell.RowIndex;
                    int fixid = Convert.ToInt32(datacart1.Rows[selectedRow].Cells["id"].Value);

                    string sql = "UPDATE cartitem SET itemcount = @ItemCount, priceitem = @PriceToCart WHERE id = @FixId";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ItemCount", itemCount);
                    cmd.Parameters.AddWithValue("@PriceToCart", pricetocart);
                    cmd.Parameters.AddWithValue("@FixId", fixid);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                        showcartitem();
                        textBox1.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update data");
                    }
                }
            }
        }

        //ปุ่มแก้ไขจำนวนสินค้า
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateCartItem();
        }


        //ปุ่มลบสินค้าออกจากตะกร้า
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คุณต้องการลบสินค้าออกจากตะกร้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int selectedRow = datacart1.CurrentCell.RowIndex;
                int fixid = Convert.ToInt32(datacart1.Rows[selectedRow].Cells["id"].Value);

                MySqlConnection conn = DatabaseConnection();
                conn.Open();
                string sql = "DELETE FROM cartitem WHERE id = @FixId";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FixId", fixid);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("ลบสินค้าออกจากตะกร้าเรียบร้อยแล้ว");
                    showcartitem();
                    textBox1.Clear();
                    label5.Text = null;
                    label1.Text = null;
                    pictureBox1.Image = null;
                    _form1.shownoti();
                }
                else
                {
                    MessageBox.Show("Failed to update data");
                }
            }
        }


        //โชว์ QR
        private void DisplayImage(string imageUrl)
        {
            try
            {
                // Download the image
                WebClient webClient = new WebClient();
                byte[] imageData = webClient.DownloadData(imageUrl);
                webClient.Dispose();

                // Load image from byte array
                MemoryStream memoryStream = new MemoryStream(imageData);
                Image image = Image.FromStream(memoryStream);

                // Convert image to grayscale if it's not already
                /*if (image.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    Bitmap bitmap = new Bitmap(image);
                    Bitmap grayscaleImage = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);
                    image = grayscaleImage;
                }*/

                // Resize image
                int newWidth = (int)(image.Width * 0.5);
                int newHeight = (int)(image.Height * 0.5);
                image = new Bitmap(image, newWidth, newHeight);

                // Display image in PictureBox
                pictureBox2.Image = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //ปุ่มเพิ่มรูปสลีป
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
                    string selectedFileName = openFileDialog.FileName;
                    //textBoxSelectedFile.Text = selectedFileName;

                    // Load the image from the selected file
                    Image image = Image.FromFile(selectedFileName);

                    // Display the image in the PictureBox
                    pictureBox3.Image = image;
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur while loading the image
                    MessageBox.Show("Error loading the image: " + ex.Message);
                }
            }
        }

        //ปุ่มเช็คสลีปว่าแนบหรือไม่
        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                DialogResult arkadd = MessageBox.Show("คุณต้องการยืนยันคำสั่งซื้อใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

                if (arkadd == DialogResult.Yes)
                {
                    try
                    {
                        int totalPrice = 0;

                        // Iterate through each row in the DataGridView to calculate the total price
                        foreach (DataGridViewRow row in datacart1.Rows)
                        {
                            if (!row.IsNewRow) // Skip the last empty row if any
                            {
                                // Retrieve the price of the current item (assuming it's stored in a column named "priceitem")
                                int itemPrice = Convert.ToInt32(row.Cells["priceitem"].Value);

                                // Add the price of the current item to the total price
                                totalPrice += itemPrice;
                            }
                        }

                        DateTime currentDateTime = DateTime.Now;
                        string currentDate = currentDateTime.ToString("dd-MM-yyyy"); // Date in yyyy-MM-dd format
                        string currentTime = currentDateTime.ToString("HH:mm:ss"); // Time in 24-hour HH:mm:ss format

                        string date = currentDateTime.ToString("dd");
                        string month = currentDateTime.ToString("MM");
                        string year = currentDateTime.ToString("yyyy");


                        Image slipimage = pictureBox3.Image;

                        // Convert the image to a byte array
                        byte[] slipimageBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            slipimage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Assuming the image format is JPEG
                            slipimageBytes = ms.ToArray();
                        }





                        using (MySqlConnection conn = DatabaseConnection())
                        {
                            conn.Open();

                            int orderidnext = 0;
                            string selectMaxQuery = "SELECT MAX(order_id) FROM orderverify";
                            MySqlCommand selectMaxCommand = new MySqlCommand(selectMaxQuery, conn);

                            object result = selectMaxCommand.ExecuteScalar();

                            if (result == null || result == DBNull.Value)
                            {
                                orderidnext = 1;
                            }
                            else
                            {
                                orderidnext = Convert.ToInt32(result) + 1;
                            }

                            while (true)
                            {
                                // Check if orderidnext exists in the history table
                                string selectFromHistoryQuery = "SELECT order_id FROM history WHERE order_id = @Value0";
                                MySqlCommand selectFromHistoryCommand = new MySqlCommand(selectFromHistoryQuery, conn);
                                selectFromHistoryCommand.Parameters.AddWithValue("@Value0", orderidnext);
                                object exists = selectFromHistoryCommand.ExecuteScalar();

                                // If orderidnext does not exist in the history table, exit the loop
                                if (exists == null || exists == DBNull.Value)
                                {
                                    break;
                                }

                                // Increment orderidnext
                                orderidnext++;
                            }


                            // Assuming DataGridView is named datacart1 and target table is named orderverify
                            string insertQuery = "INSERT INTO orderverify (order_id, username, nameitem, itemcount, priceitem, dateorder, timeorder, moneyslip, totalprice, date, month, year) VALUES (@Value0, @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11)";

                            MySqlCommand command = new MySqlCommand(insertQuery, conn);

                            foreach (DataGridViewRow row in datacart1.Rows)
                            {
                                if (!row.IsNewRow) // Skip the last empty row if any
                                {
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("@Value0", orderidnext);
                                    command.Parameters.AddWithValue("@Value1", row.Cells[1].Value);
                                    command.Parameters.AddWithValue("@Value2", row.Cells[2].Value);
                                    command.Parameters.AddWithValue("@Value3", row.Cells[3].Value);
                                    command.Parameters.AddWithValue("@Value4", row.Cells[4].Value);
                                    command.Parameters.AddWithValue("@Value5", currentDate);
                                    command.Parameters.AddWithValue("@Value6", currentTime);
                                    command.Parameters.AddWithValue("@Value7", slipimageBytes);
                                    command.Parameters.AddWithValue("@Value8", totalPrice);
                                    command.Parameters.AddWithValue("@Value9", date);
                                    command.Parameters.AddWithValue("@Value10", month);
                                    command.Parameters.AddWithValue("@Value11", year);
                                    command.ExecuteNonQuery();


                                    string updateStockQuery = "UPDATE iteminfo SET countitem = countitem - @Itemcount WHERE nameitem = @Itemname";
                                    MySqlCommand updateStockCommand = new MySqlCommand(updateStockQuery, conn);
                                    updateStockCommand.Parameters.AddWithValue("@Itemcount", row.Cells[3].Value); // Assuming itemcount is stored in the 4th column
                                    updateStockCommand.Parameters.AddWithValue("@Itemname", row.Cells[2].Value); // Assuming nameitem is stored in the 3rd column
                                    updateStockCommand.ExecuteNonQuery();

                                    
                                }
                            }
                            string deleteFromCartQuery = "DELETE FROM cartitem WHERE username = @Username";
                            MySqlCommand deleteFromCartCommand = new MySqlCommand(deleteFromCartQuery, conn);
                            deleteFromCartCommand.Parameters.AddWithValue("@Username", GlobalVariables.userlogin); // Provide the username for whom the items should be deleted
                            deleteFromCartCommand.ExecuteNonQuery();


                            _form1.shownoti();


                            this.panel1.Controls.Clear();
                            homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                            this.panel1.Controls.Add(hasd);
                            hasd.Show();


                            MessageBox.Show("ยืนยันคำสั่งซื้อเรียบร้อยแล้ว Order " + orderidnext);
                            


                            string queryemail = "SELECT email FROM useraccount WHERE username = @username";
                            MySqlCommand cmd1 = new MySqlCommand(queryemail, conn);
                            cmd1.Parameters.AddWithValue("@username", GlobalVariables.userlogin);
                            object result1 = cmd1.ExecuteScalar();

                            string eml = result1.ToString();

                            string fromMail = "pkbadmiintonshop@gmail.com";
                            string fromPassword = "dqsd ovcs evka ohcr";

                            MailMessage message = new MailMessage();
                            message.From = new MailAddress(fromMail);
                            message.Subject = "แจ้งเตือนการซื้อสินค้า";
                            message.To.Add(new MailAddress(eml));
                            message.Body = "<html><body> สวัสดี เราได้รับคำสั่งซื้อ Order " + orderidnext + " ของคุณแล้ว โปรดรอตรวจสอบการชำระเงิน และแจ้งผลผ่าน Email นี้อีกครั้ง ขอขอบคุณสำหรับคำสั่งซื้อ</body></html>";
                            message.IsBodyHtml = true;

                            var smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromMail, fromPassword),
                                EnableSsl = true,
                            };

                            smtpClient.Send(message);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }

            }
            else
            {
                MessageBox.Show("กรุณาแนบหลักฐานการโอนเงิน");
            }
        }



        //เคลียร์สินค้าออกจากตะกร้าทั้งหมด
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult arkadd = MessageBox.Show("คุณต้องการเคลียร์ตะกร้าใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

            if (arkadd == DialogResult.Yes)
            {
                MySqlConnection conn = DatabaseConnection();
                conn.Open();
                string deleteFromCartQuery = "DELETE FROM cartitem WHERE username = @Username";
                MySqlCommand deleteFromCartCommand = new MySqlCommand(deleteFromCartQuery, conn);
                deleteFromCartCommand.Parameters.AddWithValue("@Username", GlobalVariables.userlogin);
                deleteFromCartCommand.ExecuteNonQuery();

                MessageBox.Show("เคลียร์ตะกร้าเรียบร้อยแล้ว");
                

                this.panel1.Controls.Clear();
                homeads hasd = new homeads() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.panel1.Controls.Add(hasd);
                hasd.Show();

                _form1.shownoti()
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
