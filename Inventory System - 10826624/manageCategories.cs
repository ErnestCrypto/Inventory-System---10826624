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
    public partial class manageCategories : Form
    {
        public manageCategories()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CategoryTb1";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];
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
                SqlCommand cmd = new SqlCommand("insert into CategoryTb1 values('" + CatIdTb.Text + "','" + CatNameTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer has been Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CatIdTb.Text == "")
            {
                MessageBox.Show(" Enter the category's Id");

            }
            else
            {
                Con.Open();
                string Myquery = "delete from CategoryTb1 where CatId ='" + CatIdTb.Text + "' ;";
                SqlCommand cmd = new SqlCommand(Myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void manageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTb1 set CatName= '" + CatNameTb.Text + "',CatId = '" + CatIdTb.Text + "' where CatId = '" + CatIdTb.Text + "' ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category has been Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {

            }
        }
    }
}
