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
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
            SetPlaceholder(CustomerIdTb, "Customer Id");
            SetPlaceholder(CustomerfullnameTb, "Customer FullName");
            SetPlaceholder(CustomertelephoneTb, "Telephone");
            populate();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {

        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values('" + CustomerIdTb.Text + "','" + CustomerfullnameTb.Text + "','" + CustomertelephoneTb.Text + "')", con);
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
        void populate()
        {

            con.Open();
            string query = "select * from CustomerTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTbl set Cfullname='" + CustomerfullnameTb.Text + "',CPhone='" + CustomertelephoneTb.Text + "' where CId='" + CustomerIdTb.Text + "'", con);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == -1)
                return;


            CustomerIdTb.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            CustomerfullnameTb.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            CustomertelephoneTb.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            con.Open();
            SqlDataAdapter da=new SqlDataAdapter("select Count(*) from OrderTbl where CustId="+CustomerIdTb.Text+"",con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            countLbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter da2 = new SqlDataAdapter("select Sum(TotalAmt) from OrderTbl where CustId=" + CustomerIdTb.Text + "", con);
            DataTable Dtamount = new DataTable();
            da2.Fill(Dtamount);
            AmtLbl.Text = Dtamount.Rows[0][0].ToString();
            SqlDataAdapter da3 = new SqlDataAdapter("select Max(OrderDate) from OrderTbl where CustId=" + CustomerIdTb.Text + "", con);
            DataTable DtDate = new DataTable();
            da3.Fill(DtDate);
            DateLbl.Text = DtDate.Rows[0][0].ToString();





            con.Close();

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                
                CustomertelephoneTb.Focus();
                if (CustomertelephoneTb.Text == "")
                {
                    MessageBox.Show("Enter the Customer telephone number to be Deleted");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from  CustomerTbl where CId='" + CustomerIdTb.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer Deleted Successfully");
                    populate();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetPlaceholder(CustomerIdTb, "Customer Id");
            SetPlaceholder(CustomerfullnameTb, "Customer FullName");
            SetPlaceholder(CustomertelephoneTb, "Telephone");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
