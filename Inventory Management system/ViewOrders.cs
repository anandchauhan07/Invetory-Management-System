using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_system
{
    public partial class ViewOrders : Form
    {
        
        public ViewOrders()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Initial Catalog=Inventorydb;Integrated Security=True;Connect Timeout=30;");
        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate_Orders();
        }
        void populate_Orders()
        {

            con.Open();
            string query = "select * from OrderTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            { 
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
           e.Graphics.DrawString("Order Summary", new Font("Century", 25  , FontStyle.Bold), Brushes.Crimson,new Point(230,100));
            e.Graphics.DrawString("Order Id:-   " + dataGridView1.CurrentRow.Cells[0].Value.ToString(), new Font("Century", 20), Brushes.Black, new Point(80, 160));
            e.Graphics.DrawString("Customer Id:-   " + dataGridView1.CurrentRow.Cells[1].Value.ToString(), new Font("Century", 20), Brushes.Black, new Point(80, 190));
            e.Graphics.DrawString("Customer Name:-   " + dataGridView1.CurrentRow.Cells[2].Value.ToString(), new Font("Century", 20), Brushes.Black, new Point(80, 220));
            e.Graphics.DrawString("Order Date:-   " + dataGridView1.CurrentRow.Cells[3].Value.ToString(), new Font("Century", 20), Brushes.Black, new Point(80, 250));
            e.Graphics.DrawString("Total Amount Rs:   " + dataGridView1.CurrentRow.Cells[4].Value.ToString(), new Font("Century", 20), Brushes.Black, new Point(80, 280));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageOrders manageOrders = new ManageOrders();
            manageOrders.Show();
            this.Hide();

        }
    }
}
