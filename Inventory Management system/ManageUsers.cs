using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;


namespace Inventory_Management_system
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
            SetPlaceholder(usernameTb, "UserName");
            SetPlaceholder(fullnameTb, "FullName");
            SetPlaceholder(passwordTb, "Password");
            SetPlaceholder(telephoneTb, "Telephone");
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Initial Catalog=Inventorydb;Integrated Security=True;Connect Timeout=30;");



        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Crimson;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Crimson;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Crimson;
                }
            };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        void populate()
        {

            con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter da=new SqlDataAdapter(query,con);
            SqlCommandBuilder builder=new SqlCommandBuilder(da);
            var ds= new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
            
        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {   try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into UserTbl values('" + usernameTb.Text + "','" + fullnameTb.Text + "','" + passwordTb.Text + "','" + telephoneTb.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User Added Successfully");
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update UserTbl set Ufullname='" + fullnameTb.Text + "',Upassword='" + passwordTb.Text + "',UPhone='" + telephoneTb.Text + "' where Uname='" + usernameTb.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User Updated Successfully");
                populate();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                telephoneTb.Focus();
                if(telephoneTb.Text=="")
                {
                    MessageBox.Show("Enter the User telephone number to be Deleted");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UserTbl where UPhone='" + telephoneTb.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Deleted Successfully");
                    populate();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == -1)
                return;

       
            usernameTb.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            fullnameTb.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            passwordTb.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            telephoneTb.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SetPlaceholder(usernameTb, "UserName");
            SetPlaceholder(fullnameTb, "FullName");
            SetPlaceholder(passwordTb, "Password");
            SetPlaceholder(telephoneTb, "Telephone");
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
