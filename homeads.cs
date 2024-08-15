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
    public partial class homeads : Form
    {
        public homeads()
        {
            InitializeComponent();
        }
        private int adsNumber = 1;

        //ฟังชั่นโชว์หน้าโฆษณา
        private void LoadNextAds()
        {
            if (adsNumber == 5)
            {
                adsNumber = 1;
            }
            pictureBox1.ImageLocation = string.Format(@"ads\{0}.jpg", adsNumber);
            adsNumber++;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadNextAds();
        }
    }
}
