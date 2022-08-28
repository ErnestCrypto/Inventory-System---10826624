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

namespace Inventory_System___10826624
{
    public partial class manageProducts : Form
    {
        public manageProducts()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        void fillCategory()
        {
            string query = "select * from CategoryTb1";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try 
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName",typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                searchCombo.ValueMember = "CatName";
                searchCombo.DataSource = dt;
                Con.Close();

            }
            catch
            {

            }
        }

        void populate()
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


        void filterbycategory()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTb1 where ProdCat = '"+searchCombo.SelectedValue.ToString()+"' ";
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
        private void buton1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTb1 values('" + ProdIdTb.Text + "','" + ProdNameTb.Text + "','" + QtyTb.Text + "','" +PriceTb.Text+"','"+DescriptionTb.Text+"','"+CatCombo.SelectedValue.ToString()+"' )", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product has been Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void manageProducts_Load(object sender, EventArgs e)
        {
            fillCategory();
            populate();
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIdTb.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdNameTb.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            QtyTb.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
            DescriptionTb.Text = ProductsGV.SelectedRows[0].Cells[4].Value.ToString();
            CatCombo.SelectedValue = ProductsGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ProdIdTb.Text == "")
            {
                MessageBox.Show(" Enter the Product's Id");

            }
            else
            {
                Con.Open();
                string Myquery = "delete from ProductTb1 where Prodid ='" + ProdIdTb.Text + "' ;";
                SqlCommand cmd = new SqlCommand(Myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTb1 set ProdName= '" + ProdNameTb.Text + "',ProdCity = '" + QtyTb.Text + "',ProdPrice = '" + PriceTb.Text + "',ProdDesc = '" + DescriptionTb.Text + "',ProdCat = '" + CatCombo.SelectedValue.ToString() + "' where Prodid = '" + ProdIdTb.Text + "' ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product has been Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void searchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CatCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
