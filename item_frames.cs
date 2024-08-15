
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    //กำหนดค่าต่างๆที่จะดึงมาจาก database
    //รับค่าที่ปุ่มหน้า Form1 ส่งมา type brand

    public partial class item_frames : UserControl
    {
        private Form1 _form1;
        private string _type;
        private string _brand;
        public item_frames(Form1 form1, string type, string brand)
        {
            InitializeComponent();
            _form1 = form1;
            _type = type;
            _brand = brand;
        }

        private string _name_item;
        private int _price_item;
        private Image _picture;
        private int _item_count;


        //กำหนดค่าต่างๆเป็น public


        public string name_item_
        {
            get { return _name_item; } 
            set { _name_item = value; name_item.Text = value; } 
        }

        public int price_item_
        {
            get { return _price_item; }
            set { _price_item = value; price_item.Text = value.ToString(); }
            }
        public Image picture_
        {
            get { return _picture; }
            set { _picture = value; picture.Image = value; }
        }
        public int item_count
        {
            get { return _item_count; }
            set { _item_count = value; }
        }

        private void item_frames_MouseEnter(object sender, EventArgs e)
        {

        }

        private void item_frames_MouseLeave(object sender, EventArgs e)
        {

        }

        //หากคลิกที่รูป จะส่งชื่อสินค้าไปหน้า astrox100zz เพื่อสร้างหน้าต่าง รายละเอียดสินค้าแบบละเอียด
        private void picture_Click(object sender, EventArgs e)
        {
            astrox100zzinfo _astrox100z = new astrox100zzinfo(this, _form1,_type,_brand);
            _form1.showpage(_astrox100z);
        }
    }
}
