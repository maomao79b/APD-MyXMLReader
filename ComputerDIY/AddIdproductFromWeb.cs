using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerDIY
{
    public partial class AddIdproductFromWeb : Form
    {
        Object goodsAndSales;
        Jack context = new Jack();
        public AddIdproductFromWeb(Object goodsAndSales)
        {
            InitializeComponent();
            this.goodsAndSales = goodsAndSales;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("กรุณากรอกรหัสสินค้า");
            }
            else
            {
                try
                {
                    string url2 = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
                    HtmlWeb web2 = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc2 = web2.Load(url2);
                    var html = doc2.DocumentNode.Descendants("meta");
                    var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
                    var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
                    var priceblock = doc2.DocumentNode.Descendants("div");
                    var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
                        First().InnerText;
                    price = new string(price.Where(c => char.IsDigit(c)).ToArray());
                    var type = doc2.DocumentNode.SelectNodes("//div[@class=\"step_nav\"]//a");
                    var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
                    Random rdm = new Random();
                    P_Product product = new P_Product();
                    //product.Id = int.Parse(productId); // Id
                    product.Id = int.Parse(textBox1.Text); // Id
                    product.Name = title.GetAttributeValue("content", ""); // Name
                    product.Detail = description.GetAttributeValue("content", ""); // Detail
                    product.Price = decimal.Parse(price); // Price
                    product.Type = type[2].InnerText; // Type
                    product.Image = image.GetAttributeValue("content", ""); // Image
                    product.Amount = rdm.Next(0, 100); //Amount
                    context.P_Product.Add(product);
                    context.SaveChanges();
                    MessageBox.Show("Add Success");
                    this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Add Failed");
                }
            }
        }
    }
}
