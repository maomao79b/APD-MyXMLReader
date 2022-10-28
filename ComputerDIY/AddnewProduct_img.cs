using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerDIY
{
    public partial class AddnewProduct_img : Form
    {
        Goods goods;
        public AddnewProduct_img(Goods goods)
        {
            this.goods = goods;
            InitializeComponent();
        }

        private void AddnewProduct_img_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("กรุณาใส่ลิ้งค์รูปภาพที่ต้องการเปลี่ยน");
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show("ยืนยันการเปลี่ยนรูปใช่หรือไม่", "แจ้งเตือน", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        this.goods.imgLink = textBox1.Text;
                        this.goods.ImgChanged = true;
                        this.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ลิ้งค์ไม่ถูกต้อง");
                    }
                }
            }
        }
    }
}
