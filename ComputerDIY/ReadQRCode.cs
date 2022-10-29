using AForge.Controls;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ComputerDIY
{
    public partial class ReadQRCode : Form
    {
        FilterInfoCollection webcams;
        VideoCaptureDevice videoIn;
        Shop shop;
        Jack context = new Jack();
        bool open = false;
        public ReadQRCode(Shop shop)
        {
            this.shop = shop;
            InitializeComponent();
        }

        private void ReadQRCode_Load(object sender, EventArgs e)
        {
            webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo webcam in webcams)
                comboBox_select.Items.Add(webcam.Name);
        }

        private void btnON_Click(object sender, EventArgs e)
        {
            if (!open)
            {
                try
                {
                    videoIn = new VideoCaptureDevice(webcams[comboBox_select.SelectedIndex].MonikerString);
                    videoSourcePlayer.VideoSource = videoIn;
                    videoSourcePlayer.Start();
                    timer1.Start();
                    open = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Selected Camera");
                }
            }
            else
            {
                MessageBox.Show("กล้องเปิดอยู่");
            }
          
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            if(videoIn != null && videoIn.IsRunning)
            {
                open = false;
                videoIn.Stop();
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var capture = videoSourcePlayer.GetCurrentVideoFrame(); 
            if(capture != null)
            {
                BarcodeReader reader = new BarcodeReader();
                var result1 = reader.Decode(capture);
                if (result1 != null)
                {
                    try
                    {
                        int id = int.Parse(result1.Text);//รหัสสินค้า
                        var result2 = context.P_Product.Where(pd => pd.Id == id).First();
                        if (result2.Amount == 0)
                        {
                            MessageBox.Show("สินค้าหมด");
                        }
                        else
                        {
                            try
                            {
                                var result = shop.listView1.FindItemWithText(result1.Text);
                                if (result != null)
                                {
                                    int ChkAmoun = int.Parse(result.SubItems[4].Text) + 1;
                                    if (ChkAmoun > result2.Amount)
                                    {
                                        MessageBox.Show("จำนวนสินค้าไม่เพียงพอ สินค้า ID: " + id + " มีจำนวน " + result2.Amount + " ชิ้น");
                                    }
                                    else
                                    {
                                        decimal amount = decimal.Parse(result.SubItems[4].Text) + 1;
                                        result.SubItems[4].Text = amount.ToString();
                                        decimal total = amount * decimal.Parse(result.SubItems[3].Text);
                                        result.SubItems[5].Text = total.ToString();
                                        shop.label11.Text = shop.calculateTotal(shop.listView1.Items).ToString();
                                    }

                                }
                                else
                                {
                                    try
                                    {
                                        var f = context.P_Product.Where(p => p.Id == id).First();
                                        string[] items = new string[]
                                        {
                                            result1.Text,
                                            f.Name,
                                            f.Type,
                                            f.Price.ToString(),
                                            "1",
                                            f.Price.ToString()
                                        };
                                        shop.listView1.Items.Add(new ListViewItem(items));
                                        shop.label11.Text = shop.calculateTotal(shop.listView1.Items).ToString();
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("มีข้อผิดพลาดโปรดแสกนใหม่");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ไม่มีสินค้านี้");
                    }
                }
            }
        }
    }
}
