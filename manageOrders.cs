using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_System___10826624
{
    public partial class manageOrders : Form
    {
        public manageOrders()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomerTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        int num = 0;
        int uprice, totprice,qty;
        string product;


        void populateproducts()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        void fillCategory()
        {
            string query = "select * from CategoryTb1";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                // CatCombo.ValueMember = "CatName";
                // CatCombo.DataSource = dt;
                searchCombo.ValueMember = "CatName";
                searchCombo.DataSource = dt;
                Con.Close();

            }
            catch
            {

            }
        }

        void updateproduct()
        {
            
            int id = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[0].Value.ToString());
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            if (newQty < 0)
                MessageBox.Show(" Operation failed");
            else
            {
                Con.Open();
                string query = "Update ProductTb1 set ProdCIty = " + newQty + " where Prodid= " + id + " ;";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populateproducts();
            }

        }

        private void manageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateproducts();
            fillCategory();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Product", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UPrice", typeof(int));
            table.Columns.Add("TotPrice", typeof(int));
            OrderGv.DataSource = table;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustName.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void searchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1 where ProdCat = '" + searchCombo.SelectedValue.ToString() + "' ";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        int flag = 0;
        int stock;
        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            stock = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[2].Value.ToString());
            // qty = Convert.ToInt32(QtyTb.Text);
            uprice = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
            // totprice = qty * uprice;
            flag = 1;

        }

        private void buton1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (QtyTb.Text == "")
                MessageBox.Show("Enter the quantity of Products");
            else if (flag == 0)
                MessageBox.Show("Select the product");

            else if (Convert.ToInt32(QtyTb.Text) > stock)
                MessageBox.Show("Not Enough Stock Available");

            else
            {
                num = num + 1;
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * uprice;
                table.Rows.Add(num, product, qty, uprice, totprice);
                OrderGv.DataSource = table;
                flag = 0;
                updateproduct();
            }
            sum = sum + totprice;
            TotAmount.Text =  sum.ToString();
        }

        private void OrderGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(OrderIdTb.Text == "" || CustId.Text == "" || CustName.Text == "" || TotAmount.Text =="")
            {
                MessageBox.Show("Fill the Data Correctly");
            }
            else
            {
               
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into OrdersTb1   values('" + OrderIdTb.Text + "','" + CustId.Text + "','" + CustName.Text + "','" + orderdate.Text + "','" + TotAmount.Text + "' )", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order has been Successfully Added");
                Con.Close();
                //populate();
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewOrders view = new viewOrders();
            view.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
