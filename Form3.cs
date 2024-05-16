using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace randomshit
{
    public partial class Form3 : Form
    {
        string connstring = "server=127.0.0.1; port=3306; database=pos_db; username=root; pwd=QZr8408o;";

        system_cofig config = new system_cofig();

        public Form3()
        {
            InitializeComponent();
        }

        Form5 form5 = new Form5();
        public string currUser;
        public int currUserID;
        List<Product> products = new List<Product>();
        public List<Order> orderlist = new List<Order>();


        public void load_products()
        {
            products.Clear();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel5.Controls.Clear();

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();
                
                string query = "SELECT * FROM product_tbl;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["PID"]);
                            byte[] imgdt = (byte[])reader["ProductImg"];
                            string prname = reader["ProductName"].ToString();
                            double prc = Convert.ToDouble(reader["Presyo"]);
                            int stk = Convert.ToInt32(reader["Stocks"]);
                            string cat = reader["Category"].ToString();

                            Product tmpproduct = new Product(id, imgdt, prname, prc, stk, cat);
                            products.Add(tmpproduct);
                        }
                    }
                }

                foreach (Product prod in products)
                {
                    if (prod.productStock >= 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Panel pnl = new Panel();
                            pnl.BackColor = Color.White;
                            pnl.Height = 115;
                            pnl.Width = 160;
                            pnl.BorderStyle = BorderStyle.FixedSingle;
                            pnl.Tag = prod;
                            pnl.Click += new EventHandler(pnl_Click);

                            PictureBox ptrbx = new PictureBox();
                            ptrbx.SizeMode = PictureBoxSizeMode.StretchImage;
                            ptrbx.Image = toImage(prod.productImageData);
                            ptrbx.Location = new Point(80, 35);
                            ptrbx.Height = 80;
                            ptrbx.Width = 80;
                            ptrbx.Click += new EventHandler(ptrbx_Click);
                            pnl.Controls.Add(ptrbx);

                            Label lbl = new Label();
                            lbl.Text = prod.productName.ToString();
                            lbl.Font = new Font("Arial", 12, FontStyle.Bold);
                            lbl.Width = 300;
                            lbl.Location = new Point(0, 5);
                            lbl.Click += new EventHandler(lbl_Click);
                            pnl.Controls.Add(lbl);

                            Label lbl1 = new Label();
                            lbl1.Text = "₱" + prod.productPrice.ToString();
                            lbl1.Font = new Font("Arial", 16, FontStyle.Bold);
                            lbl1.Height = 30;
                            lbl1.Width = 300;
                            lbl1.Location = new Point(10, 55);
                            lbl1.Click += new EventHandler(lbl_Click);
                            pnl.Controls.Add(lbl1);

                            Label lbl2 = new Label();
                            lbl2.Text = "Stk: " + prod.productStock.ToString();
                            lbl2.Font = new Font("Arial", 8, FontStyle.Bold);
                            lbl2.Location = new Point(10, 100);
                            lbl2.ForeColor = Color.Gray;
                            lbl2.Click += new EventHandler(lbl_Click);
                            pnl.Controls.Add(lbl2);


                            if (i == 0)
                            {
                                flowLayoutPanel1.Controls.Add(pnl);
                            }
                            else if (i == 1)
                            {
                                if (prod.Category == "Burger")
                                {
                                    flowLayoutPanel2.Controls.Add(pnl);
                                }
                                else if (prod.Category == "Hotdog")
                                {
                                    flowLayoutPanel3.Controls.Add(pnl);
                                }
                                else if (prod.Category == "Beverage")
                                {
                                    flowLayoutPanel5.Controls.Add(pnl);
                                }
                            }
                        }
                    }
                    
                }

                conn.Close();
            }
        }

        public void load_orders()
        {

            Color color = config.getColorScheme();

            int tag = 0;

            foreach (Order order in orderlist)
            {
                Panel pnl = new Panel();
                pnl.BackColor = Color.White;
                pnl.Tag = tag;
                pnl.Height = 75;
                pnl.Width = 193;
                pnl.BorderStyle = BorderStyle.FixedSingle;
                flowLayoutPanel4.Controls.Add(pnl);

                Guna2Button btn = new Guna2Button();
                btn.Height = 30;
                btn.Width = 30;
                btn.Location = new Point(155, 32);
                btn.FillColor = Color.FromArgb(39, 39, 39);
                btn.Text = "x";
                btn.BorderRadius = 5;
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                btn.Click += new EventHandler(delete_order);
                pnl.Controls.Add(btn);

                Label lbl1 = new Label();
                lbl1.Text = order.order.productName;
                lbl1.Font = new Font("Arial", 10, FontStyle.Bold);
                lbl1.Width = 120;
                lbl1.Location = new Point(10, 5);
                pnl.Controls.Add(lbl1);

                Label lbl2 = new Label();
                lbl2.Text = order.order.getPrice();
                lbl2.Font = new Font("Arial", 9, FontStyle.Bold);
                lbl2.Location = new Point(155, 5);
                lbl2.ForeColor = Color.Gray;
                pnl.Controls.Add(lbl2);

                Label lbl3 = new Label();
                lbl3.Text = "₱" + order.getTotal().ToString();
                lbl3.Font = new Font("Arial", 10, FontStyle.Bold);
                lbl3.Location = new Point(15, 50);
                lbl3.ForeColor = Color.Gray;
                pnl.Controls.Add(lbl3);

                Label lbl4 = new Label();
                lbl4.Text = "Quantity: " + order.quantity.ToString();
                lbl4.Font = new Font("Arial", 10, FontStyle.Bold);
                lbl4.Location = new Point(15, 30);
                lbl4.ForeColor = Color.Gray;
                pnl.Controls.Add(lbl4);

                tag++;
            }

            Panel pnll = new Panel();
            pnll.Height = 10;
            pnll.Width = 193;
            flowLayoutPanel4.Controls.Add(pnll);

            label3.Text = "₱" + get_total_amount().ToString();
        }

        public void delete_order(object sender, EventArgs e)
        {
            Guna2Button btn = (Guna2Button)sender; 
            int orderlistIndex = (int)btn.Parent.Tag;

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("UPDATE product_tbl SET Stocks = Stocks + @ewan WHERE PID = @PID", conn))
                {
                    cmd.Parameters.AddWithValue("@PID", orderlist[orderlistIndex].order.productId);
                    cmd.Parameters.AddWithValue("@ewan", orderlist[orderlistIndex].quantity);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            orderlist.RemoveAt(flowLayoutPanel4.Controls.IndexOf(btn.Parent));

            flowLayoutPanel4.Controls.RemoveAt(flowLayoutPanel4.Controls.IndexOf(btn.Parent));

            load_products();

            label3.Text = "₱" + get_total_amount().ToString();
        }

        public double get_total_amount()
        {
            double total = 0;

            for (int i = 0; i < orderlist.Count; i++)
            {
                total += orderlist[i].getTotal();
            }

            return total;
        }

        public void pnl_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = (Panel)sender;

            Product selectedProduct = clickedPanel.Tag as Product;

            if (selectedProduct.productStock > 0)
            {
                form5.product = selectedProduct;
                form5.orderlist = orderlist;

                form5.ShowDialog();

                orderlist = form5.orderlist;

                flowLayoutPanel4.Controls.Clear();

                load_products();
                load_orders();
            }
            else
            {
                MessageBox.Show("Insufficient Stock");
            }

        }

        public void lbl_Click(object sender, EventArgs e)
        {
            Label clickedLabel = (Label)sender;

            Product selectedProduct = clickedLabel.Parent.Tag as Product;

            if (selectedProduct.productStock > 0)
            {
                form5.product = selectedProduct;
                form5.orderlist = orderlist;

                form5.ShowDialog();

                orderlist = form5.orderlist;

                flowLayoutPanel4.Controls.Clear();

                load_products();
                load_orders();
            }
            else
            {
                MessageBox.Show("Insufficient Stock");
            }

        }

        public void ptrbx_Click(object sender, EventArgs e)
        {
            PictureBox clickedLabel = (PictureBox)sender;

            Product selectedProduct = clickedLabel.Parent.Tag as Product;

            if (selectedProduct.productStock > 0)
            {
                form5.product = selectedProduct;
                form5.orderlist = orderlist;

                form5.ShowDialog();

                orderlist = form5.orderlist;

                flowLayoutPanel4.Controls.Clear();

                load_products();
                load_orders();
            }
            else
            {
                MessageBox.Show("Insufficient Stock");
            }
        }

  
        private void Form3_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT * FROM user_tbl WHERE userName = @userName;";

                using (MySqlCommand cmd = new MySqlCommand(query1, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", currUser);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            guna2CirclePictureBox1.Image = toImage((byte[])reader["ImageData"]);
                            label4.Text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                        }
                    }
                }

                conn.Close();
            }

            load_products();

            flowLayoutPanel2.SendToBack();
            flowLayoutPanel3.SendToBack();
            flowLayoutPanel5.SendToBack();


            Color color = config.getColorScheme();
            Color fontColor = config.getFontColor();

            this.BackColor = color;

            guna2Button1.FillColor = color;
            guna2Button1.ForeColor = fontColor;

            label1.ForeColor = fontColor;
        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public Image toImage(byte[] imgdata)
        {
            using (MemoryStream ms =  new MemoryStream(imgdata))
            {
                return Image.FromStream(ms);
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.SendToBack();
            flowLayoutPanel3.SendToBack();
            flowLayoutPanel5.SendToBack();

        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.SendToBack();
            flowLayoutPanel3.SendToBack();
            flowLayoutPanel5.SendToBack();
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.SendToBack();
            flowLayoutPanel2.SendToBack();
            flowLayoutPanel5.SendToBack();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Date and Time: " + DateTime.Now.ToString("F");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (orderlist.Count < 1) {
                MessageBox.Show("You have no order yet");
                return;
            }

            Form6 form6 = new Form6(this);
            form6.cashierID = currUserID;
            form6.cashierName = label4.Text;
            form6.orderlist = orderlist;

            form6.ShowDialog();

            orderlist = form6.orderlist;

            flowLayoutPanel4.Controls.Clear();

            load_orders();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel5.Controls.Clear();

            load_products();
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.SendToBack();
            flowLayoutPanel2.SendToBack();
            flowLayoutPanel3.SendToBack();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in flowLayoutPanel4.Controls)
                {
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("UPDATE product_tbl SET Stocks = Stocks + @ewan WHERE PID = @PID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PID", orderlist[(int)control.Tag].order.productId);
                            cmd.Parameters.AddWithValue("@ewan", orderlist[(int)control.Tag].quantity);
                            cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
            
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in flowLayoutPanel4.Controls)
                {
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("UPDATE product_tbl SET Stocks = Stocks + @ewan WHERE PID = @PID", conn))
                        {
                            cmd.Parameters.AddWithValue("@PID", orderlist[(int)control.Tag].order.productId);
                            cmd.Parameters.AddWithValue("@ewan", orderlist[(int)control.Tag].quantity);
                            cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {

            }

            this.Close();
        }
    }
}
