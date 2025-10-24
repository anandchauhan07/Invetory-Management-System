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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
            SetPlaceholder(CategoryIdTb, "Category Id");
            SetPlaceholder(CategoryNameTb, "Category Name");
            
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

        private void CategoryList_Click(object sender, EventArgs e)
        {

        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values('" + CategoryIdTb.Text + "','" + CategoryNameTb.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Category "+CategoryNameTb.Text+" Added Successfully");
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
            string query = "select * from CategoryTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            SetPlaceholder(CategoryIdTb, "Category Id");
            SetPlaceholder(CategoryNameTb, "Category Name");
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTbl set CatName='" + CategoryNameTb.Text + "' where CatId='" + CategoryIdTb.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User "+CategoryNameTb.Text+" Updated Successfully");
                populate();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {

                CategoryIdTb.Focus();
                if (CategoryIdTb.Text == "")
                {
                    MessageBox.Show("Enter the Category Id  to be Deleted");
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from  CategoryTbl where CatId='" + CategoryIdTb.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Category "+CategoryNameTb.Text+" Deleted Successfully");
                    Clear_Click(sender,e);
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

            CategoryIdTb.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            CategoryNameTb.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
