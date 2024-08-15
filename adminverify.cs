using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static project.loginwindow;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Org.BouncyCastle.Utilities.Net;





namespace project
{
    public partial class adminverify : Form
    {
        string nfm;
        private adminwindow _adminwin;
        public adminverify(adminwindow adminwin)
        {
            InitializeComponent();
            _adminwin = adminwin;

            MySqlConnection conn = DatabaseConnection();

            // Create a DataSet to store the data
            DataSet ds = new DataSet();

            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM orderverify ";

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(ds);

                dataorderadmin.DataSource = ds.Tables[0].DefaultView;
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

        private MySqlConnection DatabaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=pkdatabase";

            // Create MySqlConnection instance
            MySqlConnection conn = new MySqlConnection(connectionString);

            // Return the MySqlConnection instance
            return conn;
        }

        string odid;   //orderid



        //คลิ๊กเพื่อดึงข้อมูลออเดอร์ออกมาโชว์
        private void dataorder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataorderadmin.Rows.Count)
            {
                dataorderadmin.CurrentRow.Selected = true;
                odid = dataorderadmin.Rows[e.RowIndex].Cells["order_id"].FormattedValue.ToString();

                MySqlConnection conn = DatabaseConnection();
                DataSet ds = new DataSet();


                conn.Open();


                // Create MySqlCommand to execute the SQL query
                string queryverify = "SELECT * FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd4 = new MySqlCommand(queryverify, conn);
                cmd4.Parameters.AddWithValue("@orderid", odid);

                // Execute the query and retrieve the item picture
                object result1 = cmd4.ExecuteScalar();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd4);
                adapter.Fill(ds);

                dataitemlistadmin.DataSource = ds.Tables[0].DefaultView;


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


        //ยืนยันคำสั่งซื้อ
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult arkcon = MessageBox.Show("คำสั่งซื้อสำเร็จใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

            if (arkcon == DialogResult.Yes)
            {
                MySqlConnection conn = DatabaseConnection();
                conn.Open();
                string insertQuery = "INSERT INTO history (order_id, username, nameitem, itemcount, priceitem, dateorder, timeorder, moneyslip, totalprice, date, month, year, status, tracknum) VALUES ( @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13, @Value14)";


                MySqlCommand command = new MySqlCommand(insertQuery, conn);

                foreach (DataGridViewRow row in dataitemlistadmin.Rows)
                {
                    if (!row.IsNewRow) // Skip the last empty row if any
                    {

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Value1", row.Cells[1].Value);
                        command.Parameters.AddWithValue("@Value2", row.Cells[2].Value);
                        command.Parameters.AddWithValue("@Value3", row.Cells[3].Value);
                        command.Parameters.AddWithValue("@Value4", row.Cells[4].Value);
                        command.Parameters.AddWithValue("@Value5", row.Cells[5].Value);
                        command.Parameters.AddWithValue("@Value6", row.Cells[6].Value);
                        command.Parameters.AddWithValue("@Value7", row.Cells[7].Value);
                        command.Parameters.AddWithValue("@Value8", row.Cells[8].Value);
                        command.Parameters.AddWithValue("@Value9", row.Cells[9].Value);
                        command.Parameters.AddWithValue("@Value10", row.Cells[10].Value);
                        command.Parameters.AddWithValue("@Value11", row.Cells[11].Value);
                        command.Parameters.AddWithValue("@Value12", row.Cells[12].Value);
                        command.Parameters.AddWithValue("@Value13", "เตรียมจัดส่งพัสดุ");
                        command.Parameters.AddWithValue("@Value14", "-");
                        command.ExecuteNonQuery();


                    }
                }

                MessageBox.Show("ยืนยันคำสั่งซื้อเรียบร้อยแล้ว");



                string queryusername = "SELECT DISTINCT username FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd1 = new MySqlCommand(queryusername, conn);
                cmd1.Parameters.AddWithValue("@orderid", odid);
                object result1 = cmd1.ExecuteScalar();

                nfm = result1.ToString();


                string queryemail = "SELECT email FROM useraccount WHERE username = @username";
                MySqlCommand cmd2 = new MySqlCommand(queryemail, conn);
                cmd2.Parameters.AddWithValue("@username", nfm);
                object result2 = cmd2.ExecuteScalar();

                string eml = result2.ToString();


                string queryname = "SELECT name FROM useraccount WHERE username = @username";
                MySqlCommand cmd3 = new MySqlCommand(queryname, conn);
                cmd3.Parameters.AddWithValue("@username", nfm);
                object result3 = cmd3.ExecuteScalar();

                string name = result3.ToString();


                string querytel = "SELECT tel FROM useraccount WHERE username = @username";
                MySqlCommand cmd4 = new MySqlCommand(querytel, conn);
                cmd4.Parameters.AddWithValue("@username", nfm);
                object result4 = cmd4.ExecuteScalar();

                string tel = result4.ToString();


                string queryaddress = "SELECT address FROM useraccount WHERE username = @username";
                MySqlCommand cmd5 = new MySqlCommand(queryaddress, conn);
                cmd5.Parameters.AddWithValue("@username", nfm);
                object result5 = cmd5.ExecuteScalar();

                string address = result5.ToString();



                List<string> productList = new List<string>();
                string queryProducts = "SELECT nameitem,itemcount,priceitem FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmdProducts = new MySqlCommand(queryProducts, conn);
                cmdProducts.Parameters.AddWithValue("@orderid", odid);

                using (MySqlDataReader reader = cmdProducts.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productEntry = $"{reader["nameitem"]}                                                                  {reader["itemcount"]}                                     {reader["priceitem"]}";
                        productList.Add(productEntry);
                    }
                }

                string querytotal = "SELECT totalprice FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd6 = new MySqlCommand(querytotal, conn);
                cmd6.Parameters.AddWithValue("@orderid", odid);

                object result6 = cmd6.ExecuteScalar();

                string totalPriceString = result6.ToString();


                string querydate = "SELECT dateorder FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd7 = new MySqlCommand(querydate, conn);
                cmd7.Parameters.AddWithValue("@orderid", odid);
                object result7 = cmd7.ExecuteScalar();

                string orderdate = result7.ToString();

                string querytime = "SELECT timeorder FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd8 = new MySqlCommand(querytime, conn);
                cmd8.Parameters.AddWithValue("@orderid", odid);
                object result8 = cmd8.ExecuteScalar();

                string ordertime = result8.ToString();




                double totalPrice;
                if (double.TryParse(totalPriceString, out totalPrice))
                {
                    double totalPriceExcludingVAT = totalPrice / 1.07;
                    string totalpricenovat = totalPriceExcludingVAT.ToString(".00");

                    double vat = totalPriceExcludingVAT * 0.07;
                    string vatString = vat.ToString(".00");


                    string imagePath = "C:\\Users\\phuri\\source\\repos\\project\\Photo\\Logo1.png";
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                    img.ScaleToFit(200f, 200f);



                    // Add the paragraph to the document
                    


                    Document doc = new Document();

                    string filePath = "receipt " + odid + ".pdf";
                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    // Add content to PDF
                    doc.Add(img);
                    doc.Add(new Paragraph("                                                                                                             RECEIPT"));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("PKBADMINTON COMPANY "));
                    doc.Add(new Paragraph("TAX ID : 241452147592"));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("Customer "));
                    doc.Add(new Paragraph($"Name: {name}"));
                    doc.Add(new Paragraph($"Username : {nfm}"));
                    doc.Add(new Paragraph($"Email : {eml}"));
                    doc.Add(new Paragraph($"Tel : {tel}"));
                    doc.Add(new Paragraph($"Address : {address}"));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Order Date : {orderdate}" + "                                                                            " + $"Order Time : {ordertime}"));
                    //doc.Add(new Paragraph("............................................................................................................................................................"));
                    doc.Add(new Paragraph("______________________________________________________________________________"));
                    doc.Add(new Paragraph("Item List                                                                          Count                                    Price"));
                    doc.Add(new Paragraph("______________________________________________________________________________"));
                    doc.Add(new Paragraph(" "));
                    
                    foreach (string product in productList)
                    {
                        doc.Add(new Paragraph(product));
                    }

                    doc.Add(new Paragraph("______________________________________________________________________________"));
                    // Add total price as another paragraph
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("                                                                                      VAT 7%                            " + $"{vatString}"+"   Baht"));
                    doc.Add(new Paragraph("                                                                            Excluding vat                            " + $"{totalpricenovat}" + "   Baht"));
                    doc.Add(new Paragraph($"                                                                            Total Price                                {totalPriceString}" + "   Baht"));

                    // Close document
                    doc.Close();

                    string fromMail = "pkbadmiintonshop@gmail.com";
                    string fromPassword = "dqsd ovcs evka ohcr";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = "แจ้งเตือนคำสั่งซื้อสำเร็จ";
                    message.To.Add(new MailAddress(eml));
                    message.Body = "<html><body> ใบเสร็จแจ้งการซื้อสินค้าสำเร็จ จากร้าน Pkbadminton shop Order : "+ odid + " ขอขอบคุณสำหรับการซื้อสินค้าในร้านของเรา"+"</body></html>";
                    message.IsBodyHtml = true;

                    Attachment attachment = new Attachment(filePath);
                    message.Attachments.Add(attachment);

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };

                    smtpClient.Send(message);

                    string deletefromorderverifyQuery = "DELETE FROM orderverify WHERE order_id = @orderid";
                    MySqlCommand deletefromorderverify = new MySqlCommand(deletefromorderverifyQuery, conn);
                    deletefromorderverify.Parameters.AddWithValue("@orderid", odid); // Provide the username for whom the items should be deleted
                    deletefromorderverify.ExecuteNonQuery();

                    showcartitem();
                    _adminwin.shownotiadmin();
                }

            }
        }


        //ยกเลิกคำสั่งซื้อ
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult arkcon1 = MessageBox.Show("ยกเลิกคำสั่งซื้อใช่หรือไม่?", "Confirmation", MessageBoxButtons.YesNo);

            if (arkcon1 == DialogResult.Yes)
            {
                string reasontext = textBox1.Text;
                MySqlConnection conn = DatabaseConnection();
                conn.Open();
                string insertQuery = "INSERT INTO historyreject (order_id, username, nameitem, itemcount, priceitem, dateorder, timeorder, moneyslip, totalprice, date, month, year, reason) VALUES ( @Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13)";


                MySqlCommand command = new MySqlCommand(insertQuery, conn);

                foreach (DataGridViewRow row in dataitemlistadmin.Rows)
                {
                    if (!row.IsNewRow) // Skip the last empty row if any
                    {

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Value1", row.Cells[1].Value);
                        command.Parameters.AddWithValue("@Value2", row.Cells[2].Value);
                        command.Parameters.AddWithValue("@Value3", row.Cells[3].Value);
                        command.Parameters.AddWithValue("@Value4", row.Cells[4].Value);
                        command.Parameters.AddWithValue("@Value5", row.Cells[5].Value);
                        command.Parameters.AddWithValue("@Value6", row.Cells[6].Value);
                        command.Parameters.AddWithValue("@Value7", row.Cells[7].Value);
                        command.Parameters.AddWithValue("@Value8", row.Cells[8].Value);
                        command.Parameters.AddWithValue("@Value9", row.Cells[9].Value);
                        command.Parameters.AddWithValue("@Value10", row.Cells[10].Value);
                        command.Parameters.AddWithValue("@Value11", row.Cells[11].Value);
                        command.Parameters.AddWithValue("@Value12", row.Cells[12].Value);
                        command.Parameters.AddWithValue("@Value13", reasontext);
                        command.ExecuteNonQuery();


                        string itemName = row.Cells[3].Value.ToString(); // Assuming the item name is in the third column
                        int returnedItemCount = Convert.ToInt32(row.Cells[4].Value); // Assuming the returned item count is in the fourth column

                        string updateItemInfoQuery = "UPDATE iteminfo SET countitem = countitem + @ReturnedItemCount WHERE nameitem = @ItemName";
                        MySqlCommand updateItemInfoCommand = new MySqlCommand(updateItemInfoQuery, conn);
                        updateItemInfoCommand.Parameters.AddWithValue("@ReturnedItemCount", returnedItemCount);
                        updateItemInfoCommand.Parameters.AddWithValue("@ItemName", itemName);
                        updateItemInfoCommand.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("ยกเลิกคำสั่งซื้อเรียบร้อยแล้ว");


                string queryusername = "SELECT DISTINCT username FROM orderverify WHERE order_id = @orderid";
                MySqlCommand cmd1 = new MySqlCommand(queryusername, conn);
                cmd1.Parameters.AddWithValue("@orderid", odid);
                object result1 = cmd1.ExecuteScalar();

                nfm = result1.ToString();


                string queryemail = "SELECT email FROM useraccount WHERE username = @username";
                MySqlCommand cmd2 = new MySqlCommand(queryemail, conn);
                cmd2.Parameters.AddWithValue("@username", nfm);
                object result2 = cmd2.ExecuteScalar();

                string eml = result2.ToString();

                string fromMail = "pkbadmiintonshop@gmail.com";
                string fromPassword = "dqsd ovcs evka ohcr";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "คำสั่งซื้อถูกยกเลิก";
                message.To.Add(new MailAddress(eml));
                message.Body = "<html><body> สวัสดี คำสั่งซื้อ Order " + odid + " ของคุณถูกยกเลิก เนื่องจาก "+ reasontext + " ขอขอบคุณสำหรับคำสั่งซื้อ</body></html>";
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);


                string deletefromorderverifyQuery = "DELETE FROM orderverify WHERE order_id = @orderid";
                MySqlCommand deletefromorderverify = new MySqlCommand(deletefromorderverifyQuery, conn);
                deletefromorderverify.Parameters.AddWithValue("@orderid", odid); // Provide the username for whom the items should be deleted
                deletefromorderverify.ExecuteNonQuery();

                showcartitem();
                textBox1.Clear();

            }
        }

        //อัพเดทตาราง
        private void showcartitem()
        {
            MySqlConnection conn = DatabaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DISTINCT order_id, username, dateorder, timeorder, moneyslip, totalprice FROM orderverify ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            adapter.Fill(ds);

            dataorderadmin.DataSource = ds.Tables[0].DefaultView;

            label6.Text = "";
            pictureBox3.Image = null;

        }
    }
}
