using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Initial Catalog=Inventorydb;Integrated Security=True;Connect Timeout=30;");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
                            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void contextMenuStrip4_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip5_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();   
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda=new SqlDataAdapter("select count(*) from UserTbl where Uname='"+textBox1.Text+"' and Upassword='"+textBox2.Text+"'",con);
            DataTable dt= new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString()=="1")
            {
                Home home = new Home();
                home.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }
            con.Close();
        }
    }
}
