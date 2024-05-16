using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace randomshit
{
    public partial class Form4 : Form
    {
        public Image toImage(Byte[] ImageData)
        {
            using (MemoryStream ms = new MemoryStream(ImageData))
            {
                return Image.FromStream(ms);
            }
        }

        public Form4()
        {
            InitializeComponent();
        }

        string connstring = "server=127.0.0.1; port=3306; database=pos_db; user=root; pwd=QZr8408o;";

        system_cofig config = new system_cofig();

        private void LoadColors()
        {
            Color color = config.getColorScheme();
            Color fontColor = config.getFontColor();

            panel1.BackColor = color;
            label18.ForeColor = fontColor;

            guna2Button1.CheckedState.BorderColor = color;
            guna2Button1.CheckedState.CustomBorderColor = color;

            guna2Button2.CheckedState.BorderColor = color;
            guna2Button2.CheckedState.CustomBorderColor = color;

            guna2Button3.CheckedState.BorderColor = color;
            guna2Button3.CheckedState.CustomBorderColor = color;

            guna2Button8.CheckedState.BorderColor = color;
            guna2Button8.CheckedState.CustomBorderColor = color;

            updatebtn.FillColor = color;
            updatebtn.ForeColor = fontColor;

            guna2ShadowPanel8.FillColor = color;
            guna2ShadowPanel7.FillColor = color;
            guna2ShadowPanel4.FillColor = color;
            guna2ShadowPanel5.FillColor = color;
            guna2ShadowPanel3.FillColor = color;
            guna2ShadowPanel1.FillColor = color;

            label6.ForeColor = fontColor;
            label10.ForeColor = fontColor;
            label8.ForeColor = fontColor;
            label3.ForeColor = fontColor;

        }

        public void ReloadForm()
        { 
            this.Controls.Clear();
            InitializeComponent();
            LoadColors();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            guna2Button1.Checked = true;
            guna2Button10.FillColor = config.getColorScheme();
            guna2TextBox1.Text = config.getStoreName();
            panel2.BringToFront();

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT ProductName, Presyo, Stocks FROM product_tbl;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView1.DataSource = dataTable;
                }

                string query2 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = false;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query2, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView2.DataSource = dataTable;
                }

                conn.Close();
            }

            LoadColors();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            AddItemForm aif = new AddItemForm();
            aif.createButton.Visible = true;
            aif.createButton.Enabled = true;
            aif.comboBox1.Enabled = true;
            aif.comboBox1.Visible = true;
            aif.label3.Enabled = true;
            aif.label3.Visible = true;

            aif.ShowDialog();
            ReloadForm();
            panel2.BringToFront();

            guna2Button1.Checked = true;

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query = "SELECT ProductName, Presyo, Stocks FROM product_tbl;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView1.DataSource = dataTable;
                }

                conn.Close();
            }

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string name = guna2DataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                string query = "SELECT PID, ProductImg, ProductName, Presyo, Stocks, Category FROM Product_tbl WHERE ProductName = @ProductName;";

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", name);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            label4.Text = reader["PID"].ToString();
                            txtbxname.Text = reader["ProductName"].ToString();
                            pictureBox1.Image = toImage((byte[])reader["ProductImg"]);
                            txtbxprice.Text = reader["Presyo"].ToString();
                            txtbxstocks.Text = reader["Stocks"].ToString();
                            comboBox1.SelectedItem = reader["Category"].ToString();
                        }
                    }
                }

                conn.Close();
            }

        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string query = "DELETE FROM product_tbl WHERE PID = @PID;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PID", int.Parse(label4.Text));

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();

                    MessageBox.Show("Successfully Deleted!");
                }

                guna2DataGridView1.DataSource = null;

                using (MySqlConnection conn2 = new MySqlConnection(connstring))
                {
                    conn2.Open();

                    string query1 = "SELECT ProductName, Presyo, Stocks FROM product_tbl;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn2))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        guna2DataGridView1.DataSource = dataTable;
                    }

                    conn2.Close();
                }

                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();

                    string query = "SELECT ProductName, Presyo, Stocks FROM product_tbl;";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        guna2DataGridView1.DataSource = dataTable;
                    }

                    conn.Close();
                }

                guna2Button1.Checked = true;

                this.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
            
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            Image pixbox2 = pictureBox2.Image;
            byte[] pbImageData = null;

            if (pixbox2 != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
                    pbImageData = ms.ToArray();
                }
            }
            
            int prodid = int.Parse(label4.Text);

            string prodname = "";
            Image prodimg;
            double price = 0;
            int stocks = 0;
            string cate = "";

            MySqlConnection conn = new MySqlConnection(connstring);
            
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM product_tbl WHERE PID = @PID;", conn);
                
            cmd.Parameters.AddWithValue("@PID", prodid);

            MySqlDataReader reader = cmd.ExecuteReader();
                    
            while (reader.Read())
            {
                prodname = reader["ProductName"].ToString();
                prodimg = toImage((byte[])reader["ProductImg"]);
                price = double.Parse(reader["Presyo"].ToString());
                stocks = int.Parse(reader["Stocks"].ToString());
                cate = reader["Category"].ToString();
            }
                    
            conn.Close();
            
            if (prodname != txtbxname.Text)
            {
                conn.Open();

                using (MySqlCommand updatecmd = new MySqlCommand("UPDATE product_tbl SET ProductName = @ProductName WHERE PID = @PID;", conn))
                {
                    updatecmd.Parameters.AddWithValue("@ProductName", txtbxname.Text);
                    updatecmd.Parameters.AddWithValue("@PID", prodid);
                    updatecmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            if (pictureBox2.Image != null)
            {
                conn.Open();

                using (MySqlCommand updatecmd = new MySqlCommand("UPDATE product_tbl SET ProductImg = @ProductImg WHERE PID = @PID;", conn))
                {
                    updatecmd.Parameters.Add("@ProductImg", MySqlDbType.LongBlob).Value = pbImageData;
                    updatecmd.Parameters.AddWithValue("@PID", prodid);
                    updatecmd.ExecuteNonQuery();
                }

                conn.Close();
            }


            if (price != int.Parse(txtbxprice.Text))
            {
                conn.Open();

                using (MySqlCommand updatecmd = new MySqlCommand("UPDATE product_tbl SET Presyo = @Presyo WHERE PID = @PID;", conn))
                {
                    updatecmd.Parameters.AddWithValue("@Presyo", double.Parse(txtbxprice.Text));
                    updatecmd.Parameters.AddWithValue("@PID", prodid);
                    updatecmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            if (stocks != int.Parse(txtbxstocks.Text))
            {
                conn.Open();

                using (MySqlCommand updatecmd = new MySqlCommand("UPDATE product_tbl SET Stocks = @Stocks WHERE PID = @PID;", conn))
                {
                    updatecmd.Parameters.AddWithValue("@Stocks", int.Parse(txtbxstocks.Text));
                    updatecmd.Parameters.AddWithValue("@PID", prodid);
                    updatecmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            if (cate != comboBox1.SelectedItem.ToString())
            {
                conn.Open();

                using (MySqlCommand updatecmd = new MySqlCommand("UPDATE product_tbl SET Category = @Category WHERE PID = @PID;", conn))
                {
                    updatecmd.Parameters.AddWithValue("@Category", comboBox1.SelectedItem.ToString());
                    updatecmd.Parameters.AddWithValue("@PID", prodid);
                    updatecmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            MessageBox.Show("Successfully Updated!");

            guna2DataGridView1.DataSource = null;

            using (MySqlConnection conn2 = new MySqlConnection(connstring))
            {
                conn2.Open();

                string query1 = "SELECT ProductName, Presyo, Stocks FROM product_tbl;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn2))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView1.DataSource = dataTable;
                }

                conn2.Close();
            }
            


            guna2Button1.Checked = true;

            conn.Open();

            string query = "SELECT ProductName, Presyo, Stocks FROM product_tbl;";

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                guna2DataGridView1.DataSource = dataTable;
            }

            conn.Close();

            this.Show();

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(ofd.FileName);
                pictureBox2.Visible = true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = false;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView2.DataSource = dataTable;
                }

                conn.Close();
            }

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = true;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView3.DataSource = dataTable;
                }

                conn.Close();
            }

            guna2Button2.Checked = true;
            guna2Button1.Checked = false;
            guna2Button3.Checked = false;
            guna2Button8.Checked = false;

            itemOption.Visible = false;
            itemOption.Enabled = false;

            cashierOption.Enabled = true;
            cashierOption.Visible = true;

            panel3.BringToFront();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button1.Checked = true;
            guna2Button2.Checked = false;
            guna2Button3.Checked = false;
            guna2Button8.Checked = false;

            itemOption.Visible = true;
            itemOption.Enabled = true;

            cashierOption.Enabled = false;
            cashierOption.Visible = false;

            panel2.BringToFront();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        string un;

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                un = guna2DataGridView2.Rows[e.RowIndex].Cells["userName"].Value.ToString();

                MySqlConnection conn = new MySqlConnection(connstring);

                conn.Open();

                string query = "SELECT * FROM user_tbl WHERE userName = @userName;";

                using (MySqlCommand cmd  = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", un);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["FirstName"].ToString() + " " + reader["MiddleName"].ToString() + " " + reader["LastName"].ToString();
                            DateTime dt = (DateTime)reader["Birthday"];
                            dt = dt.Date;

                            guna2CirclePictureBox1.Image = toImage((byte[])reader["ImageData"]);
                            nameholder.Text = name;
                            genderholder.Text = reader["Gender"].ToString();
                            birthdayholder.Text = dt.ToString("dd-MM-yyyy");
                        }
                    }
                }

                conn.Close();
            }
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                string query = "UPDATE user_tbl SET Deactivated = @Deactivated WHERE userName = @userName;";

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", un);
                    cmd.Parameters.AddWithValue("@Deactivated", true);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            MessageBox.Show("Successfully Deactivated.");

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = false;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView2.DataSource = dataTable;
                }

                conn.Close();
            }

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = true;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView3.DataSource = dataTable;
                }

                conn.Close();
            }

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                un = guna2DataGridView3.Rows[e.RowIndex].Cells["userName"].Value.ToString();

                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    string query = "Select * FROM user_tbl WHERE userName = @userName;";

                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userName", un);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime dt = (DateTime)reader["Birthday"];

                                guna2CirclePictureBox1.Image = toImage((byte[])reader["ImageData"]);
                                nameholder.Text = reader["FirstName"].ToString() + " " + reader["MiddleName"].ToString() + " " + reader["LastName"].ToString();
                                genderholder.Text = reader["Gender"].ToString();
                                birthdayholder.Text = dt.ToString("dd-MM-yyyy");
                            }
                        }
                    }

                    conn.Close();
                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                string query = "UPDATE user_tbl SET Deactivated = @Deactivated WHERE userName = @userName;";

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", un);
                    cmd.Parameters.AddWithValue("@Deactivated", false);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            MessageBox.Show("Successfully Activated.");

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = false;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView2.DataSource = dataTable;
                }

                conn.Close();
            }

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                string query1 = "SELECT userName, FirstName, LastName FROM user_tbl WHERE Administrator = false AND Deactivated = true;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    guna2DataGridView3.DataSource = dataTable;
                }

                conn.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            List<Transaction> transaction_list = new List<Transaction>();
            transaction_list.Clear();

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();
                string qry = "SELECT * FROM transaction_tbl ORDER BY TID DESC";

                using (MySqlCommand cmd = new MySqlCommand(qry, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string prodname = reader["productName"].ToString();
                            string unitprice = reader["unitPrice"].ToString();
                            string quantity = reader["Quantity"].ToString();
                            string ordtotal = reader["orderTotal"].ToString();
                            string date = reader["Date"].ToString();
                            string total = reader["totalAmount"].ToString();
                            string cashiername = reader["CashierName"].ToString();

                            Transaction temp = new Transaction(prodname.Replace("NEWLINE", "\n"), unitprice.Replace("NEWLINE", "\n"), quantity.Replace("NEWLINE", "\n"), ordtotal.Replace("NEWLINE", "\n"), date, total, cashiername);

                            transaction_list.Add(temp);
                        }
                    }
                }

                conn.Close();

            }

            guna2DataGridView4.Columns.Clear();
            guna2DataGridView4.Rows.Clear();
            guna2DataGridView4.Columns.Add("Cashier Name", "Cashier Name");
            guna2DataGridView4.Columns.Add("Name of Product", "Name of Product");
            guna2DataGridView4.Columns.Add("Unit Price", "Unit Price");
            guna2DataGridView4.Columns.Add("Quantity", "Quantity");
            guna2DataGridView4.Columns.Add("Date", "Date");
            guna2DataGridView4.Columns.Add("Total Amount", "Total Amount");

            guna2DataGridView4.Rows.Clear();

            
            foreach(Transaction tran in transaction_list)
            {
                guna2DataGridView4.Rows.Add(tran.CashierName, tran.ProductName, tran.UnitPrice, tran.Quantity, tran.Date, tran.TotalAmount);
            }


            guna2Button1.Checked = false;
            guna2Button2.Checked = false;
            guna2Button3.Checked = true;
            guna2Button8.Checked = false;

            itemOption.Visible = false;
            itemOption.Enabled = false;

            cashierOption.Enabled = false;
            cashierOption.Visible = false;

            panel4.BringToFront();
        }

        private void guna2DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2DataGridView4_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2Button1.Checked = false;
            guna2Button2.Checked = false;
            guna2Button3.Checked = false;

            itemOption.Visible = false;
            itemOption.Enabled = false;

            cashierOption.Enabled = false;
            cashierOption.Visible = false;

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            guna2Button1.Checked = false;
            guna2Button2.Checked = false;
            guna2Button3.Checked = false;
            guna2Button8.Checked = true;

            itemOption.Visible = false;
            itemOption.Enabled = false;

            cashierOption.Enabled = false;
            cashierOption.Visible = false;

            panel5.BringToFront();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            guna2Button10.FillColor = colorDialog1.Color;
            guna2Button10.Tag = getLuminance(colorDialog1.Color);
        }

        private double getLuminance(Color color)
        {
            double Red = int.Parse(color.R.ToString()) / 255.0;
            double Green = int.Parse(color.G.ToString()) / 255.0;
            double Blue = int.Parse(color.B.ToString()) / 255.0;

            return (0.299 * Red) + (0.587 * Green) + (0.114 * Blue); 
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("UPDATE system_config SET ColorScheme = @ColorScheme, StoreName = @StoreName, FontColor = @FontColor", conn))
                {
                    String fontColor = (double)guna2Button10.Tag > 0.53 ? "272727" : "White";

                    cmd.Parameters.AddWithValue("@ColorScheme", guna2Button10.FillColor.ToArgb().ToString());
                    cmd.Parameters.AddWithValue("@FontColor", fontColor);
                    cmd.Parameters.AddWithValue("@StoreName", guna2TextBox1.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Configuration Saved!");
                conn.Close();
            }

            Application.Restart();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label18.Text = "Date and Time: " + DateTime.Now.ToString("G");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
