using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randomshit
{
    public partial class Form6 : Form
    {
        public Form6(Form3 form3)
        {
            InitializeComponent();
        }

        string connstring = "server=127.0.0.1; port=3306; database=pos_db; username=root; pwd=QZr8408o;";

        system_cofig config = new system_cofig();

        public int cashierID;
        public string cashierName;
        public List<Order> orderlist = new List<Order>();
        double total = 0;
        double payment = 0;
        double change = 0;
        

        private void Form6_Load(object sender, EventArgs e)
        {
            foreach (Order o in orderlist)
            {
                if (o.order.productName.Length < 7)
                {
                    listBox1.Items.Add(o.order.productName + "\t" + o.quantity.ToString() + "\t         P" + o.getTotal().ToString());
                }
                else
                {
                    listBox1.Items.Add(o.order.productName + "\t" + o.quantity.ToString() + "\t         P" + o.getTotal().ToString());
                }

                total += o.getTotal();

                guna2TextBox1.Text = "₱" + total.ToString();
            }

            label8.Text = "₱ " + total.ToString();

            Color color = config.getColorScheme();
            guna2Panel1.FillColor = color;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button1.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button1.Text;
            }
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button12.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button12.Text;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button3.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button3.Text;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button4.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button4.Text;
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button5.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button5.Text;
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button6.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button6.Text;
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button7.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button7.Text;
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button8.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button8.Text;
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button9.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button9.Text;
            }
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button11.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button11.Text;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button2.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button2.Text;
            }
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "₱" || guna2TextBox2.Text == "₱0")
            {
                guna2TextBox2.Text = "₱";
                guna2TextBox2.Text += guna2Button13.Text;
            }
            else
            {
                guna2TextBox2.Text += guna2Button13.Text;
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text.Length > 1)
            {
                string text = "";
                for (int i = 0; i < guna2TextBox2.Text.Length - 1; i++)
                {
                    text += guna2TextBox2.Text[i];
                }
                guna2TextBox2.Text = text;
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {

            string text = "";

            for (int i = 1; i < guna2TextBox2.Text.Length; i++)
            {
                text += guna2TextBox2.Text[i];
            }

            payment = double.Parse(text);

            if (payment < total)
            {
                MessageBox.Show("Your payment is insufficient.");
            }
            else
            {
                change = payment - total;

                if (MessageBox.Show("Confirm Payment?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();

                        foreach(Order ord in orderlist)
                        {
                            string query = "UPDATE product_tbl SET Stocks = @Stocks WHERE (PID = @PID);";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@Stocks", ord.order.productStock - ord.quantity);
                                cmd.Parameters.AddWithValue("@PID", ord.order.productId);

                                cmd.ExecuteNonQuery();
                            }

                            ord.order.productStock -= ord.quantity;
                        }
                        conn.Close();
                    }

                    this.Close();

                    int paperheight = 1200;
                    
                    foreach(Order ord in orderlist)
                    {
                        paperheight += 60;
                    }

                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.Document.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("receipt", 800, paperheight);

                    printPreviewDialog1.ShowDialog();

                    string productName = "";
                    string unitPrice = "";
                    string Quantity = "";
                    string orderTotal = "";
                    

                    foreach(Order ord in orderlist)
                    {
                        productName += ord.order.productName + "NEWLINE";
                        unitPrice += "₱ " + ord.order.productPrice.ToString() + "NEWLINE";
                        Quantity += ord.quantity.ToString() + "NEWLINE";
                        orderTotal += "₱ " + ord.getTotal() + "NEWLINE";
                    }


                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        string qry = "INSERT INTO transaction_tbl (CashierName, productName, unitPrice, Quantity, orderTotal, Date, totalAmount, CashierID, Total) VALUES (@CashierName, @productName, @unitPrice, @Quantity, @orderTotal, @Date, @totalAmount, @CashierID, @Total);";

                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                        {
                            cmd.Parameters.AddWithValue("@CashierName", cashierName);
                            cmd.Parameters.AddWithValue("@productName", productName);
                            cmd.Parameters.AddWithValue("@unitPrice", unitPrice);
                            cmd.Parameters.AddWithValue("@Quantity", Quantity);
                            cmd.Parameters.AddWithValue("@orderTotal", orderTotal);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("g"));
                            cmd.Parameters.AddWithValue("@totalAmount", "₱ " + total.ToString());
                            cmd.Parameters.AddWithValue("@CashierID", cashierID);
                            cmd.Parameters.AddWithValue("@Total", total);

                            cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }


                    orderlist.Clear();
                }
                else
                {
                    MessageBox.Show("you cancelled your payment");
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string storeName = config.getStoreName();

            int numOfUnderscore = (int)Math.Ceiling((26 - storeName.Length) / 2.0);
            string underscores = "";
            for (int i = numOfUnderscore; i >= 0; i--)
            {
                underscores += '_';
            }

            int height = 530;

            e.Graphics.DrawString(underscores + storeName + underscores, new Font("Arial", 40, FontStyle.Bold), Brushes.Black, new Point(0, 30));
            e.Graphics.DrawString("081 Brgy. Matictic Looban", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(235, 100));
            e.Graphics.DrawString("09480682386", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(310, 140));
            e.Graphics.DrawString("CASHIER:\t\t" + cashierName, new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(80, 250));
            e.Graphics.DrawString("DATE:\t\t\t" + DateTime.Now.ToString("d"), new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(80, 300));
            e.Graphics.DrawString("TIME:\t\t\t" + DateTime.Now.ToString("T"), new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(80, 350));
            e.Graphics.DrawString("ProductName           Quantity                Price", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(40, 480));

            foreach (Order ord in orderlist)
            {
                if (ord.order.productName.Length < 7)
                {
                    e.Graphics.DrawString(ord.order.productName + "\t\t\t" + ord.quantity.ToString() + "\t\t₱" + ord.getTotal().ToString(), new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(50, height));
                }
                else
                {
                    e.Graphics.DrawString(ord.order.productName + "\t\t" + ord.quantity.ToString() + "\t\t₱" + ord.getTotal().ToString(), new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(50, height));
                }


                height += 50;
            }

            e.Graphics.DrawString("------------------------------------------------------------------------", new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(20, height));

            e.Graphics.DrawString("PAYMENT:\t₱" + payment.ToString(), new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(400, height + 80));
            e.Graphics.DrawString("TOTAL:\t\t₱" + total.ToString(), new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(400, height + 120));
            e.Graphics.DrawString("------------------------------------", new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(390, height + 150));
            e.Graphics.DrawString("CHANGE:\t₱" + change, new Font("Arial", 22, FontStyle.Bold), Brushes.Black, new Point(400, height + 190));

            e.Graphics.DrawString("==============================================================", new Font("Arial", 22, FontStyle.Regular), Brushes.Black, new Point(0, height + 400));

            e.Graphics.DrawString("THANK YOU FOR PURCHASING!", new Font("Arial", 26, FontStyle.Bold), Brushes.Black, new Point(110, height + 480));
            e.Graphics.DrawString("PLEASE COME AGAIN!", new Font("Arial", 26, FontStyle.Bold), Brushes.Black, new Point(200, height + 540));
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
