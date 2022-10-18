using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace ComputerDIY
{
    public partial class Goods : Form
    {
        Jack context = new Jack();
        Login login;
        public string imgLink = "KO";

        public Goods(Login login)
        {
            this.login = login;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = context.P_Product.ToList();
            foreach (var item in result)
            {
                if (comboBox1.Items.IndexOf(item.Type) < 0)
                    comboBox1.Items.Add(item.Type);
            }
        }
        private Image LoadImage(string url)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                Bitmap bmp = new Bitmap(streamResponse);
                streamResponse.Dispose();
                return bmp;

            }
            catch (Exception)
            {
                return null;
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
        //    https://www.jib.co.th/web/product/product_list/2/43
        //    HtmlWeb web = new HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument doc = web.Load(url);
        //    var html = doc.DocumentNode.Descendants("meta");
        //    var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
        //    textBox2.Text = title.GetAttributeValue("content", "");
        //    var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
        //    textBox3.Text = description.GetAttributeValue("content", "");
        //    var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
        //    pictureBox1.Image = LoadImage(image.GetAttributeValue("content", ""));

        //    var priceblock = doc.DocumentNode.Descendants("div");
        //    var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
        //        First().InnerText;
        //    price = new string(price.Where(c => char.IsDigit(c)).ToArray());
        //    textBox4.Text = price;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            //string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
            listView1.Items.Clear();
            string url = textBox5.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            var numberOfPage = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"]//span");
            textBox6.Text = numberOfPage[1].InnerText;
            textBox8.Text = numberOfPage[0].InnerText;
            // type
            var typeOfProduct = doc.DocumentNode.SelectNodes("//span[@class=\"en\"]");
            textBox7.Text = typeOfProduct[1].InnerText;

            //textBox2.Text = titleNode.Attributes["content"].Value;

            //HtmlNode desciptionNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:description\"]");
            //textBox3.Text = desciptionNode.Attributes["content"].Value;

            //HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:image\"]");
            //pictureBox1.Image = LoadImage(imageNode.GetAttributeValue("content", ""));

            //HtmlNode priceNode = doc.DocumentNode.SelectSingleNode("//div[@class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
            //string price = priceNode.InnerText;
            //price = new string(price.Where(c => char.IsDigit(c)).ToArray());
            //textBox4.Text = price;

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            int codepage = 0;
            int count = 0;
            while (true)
            {
                string url = textBox5.Text+"/"+codepage;
                //Console.WriteLine(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                var titleNode = doc.DocumentNode.SelectNodes("//div[@class=\"cart_modal buy_promo\"]");
                if (titleNode == null)
                {
                    break;
                }
                foreach (HtmlNode node in titleNode)
                {
                    string[] data = new string[]
                    {
                        node.Attributes["data-id"].Value,
                        node.Attributes["data-name"].Value
                    };
                    listView1.Items.Add(new ListViewItem(data));
                    count++;
                }
                Console.WriteLine(count);
                codepage += 100;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //    https://www.jib.co.th/web/product/product_list/2/43
            //Console.WriteLine(listView1.SelectedItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].Text;
            // id
            string url = "https://www.jib.co.th/web/product/readProduct/" + textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            var html = doc.DocumentNode.Descendants("meta");
            // name
            var title = html.Where(m => m.GetAttributeValue("property", "") == "og:title").First();
            textBox2.Text = title.GetAttributeValue("content", "");
            // detial
            var description = html.Where(m => m.GetAttributeValue("property", "") == "og:description").First();
            textBox3.Text = description.GetAttributeValue("content", "");
            // image
            var image = html.Where(m => m.GetAttributeValue("property", "") == "og:image").First();
            //string i = image.GetAttributeValue("content", "");
            //Console.WriteLine(i);
            pictureBox1.Image = LoadImage(image.GetAttributeValue("content", ""));
            // price
            var priceblock = doc.DocumentNode.Descendants("div");
            var price = priceblock.Where(p => p.GetAttributeValue("class", "") == "col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block").
                First().InnerText;
            price = new string(price.Where(c => char.IsDigit(c)).ToArray());
            textBox4.Text = price;
            var type = doc.DocumentNode.SelectNodes("//div[@class=\"step_nav\"]//a");
            textBox7.Text = type[2].InnerText;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codepage = 0;
            int count = 0;
            while (true)
            {
                string url = textBox5.Text + "/" + codepage;
                //Console.WriteLine(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);
                var titleNode = doc.DocumentNode.SelectNodes("//div[@class=\"cart_modal buy_promo\"]");
                if (titleNode == null)
                {
                    break;
                }
                //int i = 0;
                foreach (HtmlNode node in titleNode)
                {
                    P_Product product = new P_Product();
                    //string productId = node.Attributes["data-id"].Value;
                    int productId = int.Parse(node.Attributes["data-id"].Value);
                    //int productId = int.Parse(listView1.Items[i].SubItems[0].Text);
                    Console.WriteLine("Id : "+productId);
                    try
                    {
                        var checkId = context.P_Product.Where(p => p.Id == productId).First();
                        //i++;
                        continue;
                    }
                    catch (Exception)
                    {
                        string url2 = "https://www.jib.co.th/web/product/readProduct/" + productId;
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
                        //product.Id = int.Parse(productId); // Id
                        product.Id = productId; // Id
                        product.Name = title.GetAttributeValue("content", ""); // Name
                        product.Detail = description.GetAttributeValue("content", ""); // Detail
                        product.Price = decimal.Parse(price); // Price
                        product.Type = type[2].InnerText; // Type
                        product.Image = image.GetAttributeValue("content", ""); // Image
                        product.Amount = rdm.Next(0, 100); //Amount
                        context.P_Product.Add(product);
                        context.SaveChanges();
                        //i++;
                        count++;
                        
                    }
                    
                }
                codepage += 100;
            }
            //Console.WriteLine(count);
            //int result = context.SaveChanges();
            //MessageBox.Show(result+" record");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.login.Visible = true;
            //this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.login.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pProductBindingSource.DataSource = context.P_Product.ToList();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(textBox11.Text);
            var f = context.P_Product.Where(p => p.Id == id).First();
            pictureBox2.Image = LoadImage(f.Image);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddnewProduct_img newProductImg = new AddnewProduct_img(this);
            newProductImg.ShowDialog();
            try
            {
                pictureBox2.Image = LoadImage(this.imgLink);
            }
            catch (Exception)
            {
                MessageBox.Show("error link");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string tx15 = textBox15.Text;

            var result = from pro in context.P_Product
                         where pro.Id.ToString().Contains(tx15) ||
                         pro.Name.Contains(tx15) ||
                         pro.Detail.Contains(tx15) ||
                         pro.Type.Contains(tx15)
                         select pro;
            pProductBindingSource.DataSource = result.ToList();
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            int pro_id = int.Parse(textBox11.Text);
            string type = textBox13.Text;
            var result = from pro in context.P_Product
                         where pro.Id == pro_id
                         select pro;
            context.P_Product.Remove(result.First());
            context.SaveChanges();
            MessageBox.Show("Remove Success");
            pProductBindingSource.DataSource = context.P_Product.Where(p => p.Type == type).ToList();
        }
        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            context.SaveChanges();
            MessageBox.Show("Save success");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string type = comboBox1.SelectedItem.ToString();
            pProductBindingSource.DataSource = context.P_Product.Where(p => p.Type == type).ToList();
        }

        private void dataGridView5_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int id1 = int.Parse(dataGridView5.SelectedRows[0].Cells[1].Value.ToString());
                int id2 = int.Parse(dataGridView5.SelectedRows[0].Cells[2].Value.ToString());
                var pro1 = context.P_Product.Where(p => p.Id == id1).First();
                textBox17.Text = pro1.Id.ToString();
                textBox16.Text = pro1.Name;
                textBox18.Text = pro1.Detail;
                textBox22.Text = pro1.Price.ToString();
                pictureBox3.Image = LoadImage(pro1.Image);
                var pro2 = context.P_Product.Where(p => p.Id == id2).First();
                textBox20.Text = pro2.Id.ToString();
                textBox19.Text = pro2.Name;
                textBox21.Text = pro2.Detail;
                textBox23.Text = pro2.Price.ToString();
                pictureBox4.Image = LoadImage(pro2.Image);

            }
            catch (Exception)
            {
                MessageBox.Show("ไม่มีสินค้านี้");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pPromotionBindingSource.DataSource = context.P_Promotion.ToList();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string pro1 = textBox24.Text;
                string pro2 = textBox25.Text;

                P_Promotion promo = new P_Promotion();
                promo.Product_1 = pro1;
                promo.Product_2 = pro2;

                context.P_Promotion.Add(promo);
                context.SaveChanges();
                pPromotionBindingSource.DataSource = context.P_Promotion.ToList();

            }
            catch (Exception)
            {
                MessageBox.Show("Please try again");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int promo_id = int.Parse(dataGridView5.SelectedRows[0].Cells[0].Value.ToString());
            string type = textBox13.Text;
            var result = from prom in context.P_Promotion
                         where prom.Id == promo_id
                         select prom;
            context.P_Promotion.Remove(result.First());
            context.SaveChanges();
            MessageBox.Show("Remove Success");
            pPromotionBindingSource.DataSource = context.P_Promotion.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddIdproductFromWeb addnewIdProduct = new AddIdproductFromWeb(this);
            addnewIdProduct.ShowDialog();
        }
    }
}
