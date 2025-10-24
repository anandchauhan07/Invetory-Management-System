using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory_Management_system
{
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
           
        }
        DataTable table = new DataTable();
        int num = 0;
        int uprice, totalprice, qty;
        string product;
        decimal sum = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Initial Catalog=Inventorydb;Integrated Security=True;Connect Timeout=30;");

        void populateCustomerTbl()
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == -1)
                return;
            CIdTb.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            CNameTb.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populateCustomerTbl();
            populate_Product();
            fillCategory();
            
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("TotalPrice", typeof(decimal));

        }
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
                Pcat.ValueMember = "CatName";
                Pcat.DataSource = dt;
                con.Close();
            }
            catch
            {
                // Handle exceptions
                MessageBox.Show("Error fetching categories");
            }
        }

        void populate_Product()
        {

            con.Open();
            string query = "select * from ProductTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();

        }
        

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Pcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            populate_Product();
        }

        int flag = 0;
        int stock;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product=dataGridView2.CurrentRow.Cells[1].Value.ToString();
            uprice = Convert.ToInt32(dataGridView2.CurrentRow.Cells[2].Value.ToString());
            stock= Convert.ToInt32(dataGridView2.CurrentRow.Cells[3].Value.ToString());
            // qty= Convert.ToInt32(QtyTb.Text);
            // totalprice= uprice * qty;
            flag = 1;
            

        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            
            if(QtyTb.Text=="")
                MessageBox.Show("pls enter the quantity");
            else if(flag==0)
                MessageBox.Show("pls select the product");
            else if(Convert.ToInt32(QtyTb.Text)>stock)
                MessageBox.Show("Not enough in stock");
            else
            {   updateProduct();
                num = num + 1;
                totalprice = Convert.ToInt32(QtyTb.Text) * uprice;

                table.Rows.Add(num, product, Convert.ToInt32(QtyTb.Text), uprice, totalprice);
                dataGridView3.DataSource = table;
                sum += Convert.ToDecimal(totalprice);
                flag = 0;
            }
            
                 
            AmtLbl.Text =  sum.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(OIdTb.Text=="" || CIdTb.Text=="" || CNameTb.Text=="" || AmtLbl.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
               
                    con.Open();
                   
                    string query="insert into OrderTbl values ("+OIdTb.Text+","+CIdTb.Text+",'"+CNameTb.Text+"','"+dateTimePicker1.Value+"','"+AmtLbl.Text+"')";
                    SqlCommand cmd=new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order Inserted Successfully");
            }
               
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OIdTb.Clear();
            CIdTb.Clear();
            CNameTb.Clear();
            QtyTb.Clear();
            AmtLbl.Text = "0";
            table.Rows.Clear();
            dataGridView3.DataSource = table;
            sum = 0;
            num = 0;
            flag = 0;
            dateTimePicker1.Value = DateTime.Now;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewOrders viewOrders = new ViewOrders();
            viewOrders.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Query = "select * from ProductTbl where ProductCat='" + Pcat.Text + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        void updateProduct()
        {
            try
            {
                con.Open();
                int id=dataGridView2.CurrentRow.Cells[0].Value.GetHashCode();
                int newQty = stock - Convert.ToInt32(QtyTb.Text);
                string query = "update ProductTbl set ProductQty=" + newQty + " where ProductId=" + id + "";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                populate_Product();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error"+ex);
            }
          
        }
    }
}
