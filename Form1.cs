using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace randomshit
{
    public partial class Form1 : Form
    {
        string connstring = "server=127.0.0.1; port=3306; database=pos_db; uid=root; pwd=QZr8408o;";

        system_cofig config = new system_cofig();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Color color = config.getColorScheme();
            Color fontColor = config.getFontColor();

            this.BackColor = color;
            this.ForeColor = fontColor;

            guna2Button1.FillColor = color;
            guna2Button1.ForeColor = fontColor;

            guna2Button2.FillColor = color;
            guna2Button2.ForeColor = fontColor;

            guna2Button3.FillColor = color;
            guna2Button3.ForeColor = fontColor;

            guna2CircleButton1.ForeColor = fontColor;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            this.Hide();
            form2.ShowDialog();
            this.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM user_tbl WHERE userName = @userName";

            using (MySqlConnection conn =  new MySqlConnection(connstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", guna2TextBox1.Text);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int uid = reader.GetInt32("UID");
                            string fn = reader.GetString("FirstName");
                            string mn = reader.GetString("MiddleName");
                            string ln = reader.GetString("LastName");
                            string gender = reader.GetString("Gender");
                            int age = reader.GetInt32("Age");
                            DateTime birthday = reader.GetDateTime("Birthday");
                            string un = reader.GetString("userName");
                            string pw = reader.GetString("passWord");
                            bool admin = reader.GetBoolean("Administrator");

                            if (admin)
                            {
                                if (pw == guna2TextBox2.Text)
                                {
                                    Form4 form4 = new Form4();

                                    this.Hide();
                                    form4.ShowDialog();
                                    this.Show();
                                    guna2TextBox1.Text = "";
                                    guna2TextBox2.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect Password.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("This acoount has no access on administrator controls.");
                            }
                            
                        }
                    }
                }
                conn.Close();
            }
        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM user_tbl WHERE userName = @userName";

            using (MySqlConnection conn = new MySqlConnection(connstring))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", guna2TextBox1.Text);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Username isn't found.");
                            return;
                        }

                        while (reader.Read())
                        {
                            int uid = reader.GetInt32("UID");
                            string fn = reader.GetString("FirstName");
                            string mn = reader.GetString("MiddleName");
                            string ln = reader.GetString("LastName");
                            string gender = reader.GetString("Gender");
                            int age = reader.GetInt32("Age");
                            DateTime birthday = reader.GetDateTime("Birthday");
                            string un = reader.GetString("userName");
                            string pw = reader.GetString("passWord");
                            bool admin = reader.GetBoolean("Administrator");
                            bool deactivated = reader.GetBoolean("Deactivated");

                            if (!deactivated)
                            {
                                if (pw == guna2TextBox2.Text)
                                {
                                    Form3 form3 = new Form3();
                                    this.Hide();
                                    form3.currUser = guna2TextBox1.Text;
                                    form3.currUserID = reader.GetInt32("UID");
                                    form3.ShowDialog();
                                    this.Show();
                                    guna2TextBox1.Text = "";
                                    guna2TextBox2.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect Password.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("This account is deactivated");
                            }

                        }
                    }
                }
                conn.Close();
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
