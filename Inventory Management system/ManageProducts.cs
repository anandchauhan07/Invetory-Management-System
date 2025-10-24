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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
            SetPlaceholder(PId, "Product Id");
            SetPlaceholder(Pname, "Product Name");
            SetPlaceholder(Pprice, "Product Price");
            SetPlaceholder(Pqty, "Product Qty");
            SetPlaceholder(Pdesc, "Description");
            fillCategory();

        }

        void clear()
        {
            SetPlaceholder(PId, "Product Id");
            SetPlaceholder(Pname, "Product Name");
            SetPlaceholder(Pprice, "Product Price");
            SetPlaceholder(Pqty, "Product Qty");
            SetPlaceholder(Pdesc, "Description");
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Initial Catalog=Inventorydb;Integrated Security=True;Connect Timeout=30;");

        void fillCategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                Pcat.ValueMember= "CatName";
                Pcat.DataSource = dt;
                comboBox1.ValueMember = "CatName";
                comboBox1.DataSource = dt;

            

                con.Close();
            }
            catch
            {
                // Handle exceptions
                MessageBox.Show("Error fetching categories");
            }
        }
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

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillCategory();
            populate();
        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PId.Focus();
                if (PId.Text == "" || Pname.Text=="" || Pprice.Text=="" || Pqty.Text =="" || Pdesc.Text =="" || Pcat.Text =="")
                {
                    MessageBox.Show("Enter the Product Id  to be Deleted");
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + PId.Text + "','" + Pname.Text + "','" + Pprice.Text + "','" + Pqty.Text + "','"+Pdesc.Text+ "','"+Pcat.Text+"')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product "+ Pname.Text+" Added Successfully");
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
            string query = "select * from ProductTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            try
            {
                PId.Focus();
                if (PId.Text == "")
                {
                    MessageBox.Show("Enter the Product Id  to  Update");
                }
                con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set ProductName='" + Pname.Text + "',ProductPrice='" + Pprice.Text + "', ProductQty='"+Pqty.Text+"',ProductDesc='"+Pdesc.Text+"',ProductCat='"+Pcat.Text+"' where ProductId='" + PId.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product Updated Successfully");
                populate();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PId.Text=dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Pname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Pprice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Pqty.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            Pdesc.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            Pcat.SelectedValue = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                PId.Focus();
                if (PId.Text == "")
                {
                    MessageBox.Show("Enter the Product Id  to be Deleted");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where ProductId='" + PId.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product "+Pname.Text+" Deleted Successfully");
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
            con.Open();
            string Query="select * from ProductTbl where ProductCat='"+comboBox1.Text+"'";
            SqlCommand cmd=new SqlCommand(Query,con);
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            populate();
        }

        private void Pcat_SelectedIndexChanged(object sender, EventArgs e)
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
