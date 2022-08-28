﻿using System;
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
    public partial class viewOrders : Form
    {
        public viewOrders()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        void populateorders()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from OrdersTb1"; 
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                OrdersGV.DataSource = ds.Tables[0];       
                Con.Close();
            }
            catch
            { 
            }
        }
        private void viewOrders_Load(object sender, EventArgs e)
        {
            populateorders();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();
            }
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Order Summary", new Font("Centry", 25, FontStyle.Bold), Brushes.Blue, new Point(230));
            e.Graphics.DrawString("Order Id : " + OrdersGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Centry", 20, FontStyle.Regular), Brushes.Black, new Point(80, 100));
            e.Graphics.DrawString("Customer Id : " + OrdersGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Centry", 20, FontStyle.Regular), Brushes.Black, new Point(80, 135));
            e.Graphics.DrawString("Order Id : " + OrdersGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Centry", 20, FontStyle.Regular), Brushes.Black, new Point(80, 190));
            e.Graphics.DrawString("Order Date : " + OrdersGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Centry", 20, FontStyle.Regular), Brushes.Black, new Point(80, 190));
            e.Graphics.DrawString("Total Amount : " + OrdersGV.SelectedRows[0].Cells[4].Value.ToString(), new Font("Centry", 20, FontStyle.Regular), Brushes.Black, new Point(80, 220));
            e.Graphics.DrawString("PowerdBy Ernest Akoto Bamfo - 10826624", new Font("Centry", 25, FontStyle.Bold), Brushes.Blue, new Point(230));

        }
    }
}
