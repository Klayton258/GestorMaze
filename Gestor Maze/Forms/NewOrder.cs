using Bunifu.Framework.UI;
using Gestor_Maze.Controllers;
using Gestor_Maze.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_Maze.Forms
{
    public partial class NewOrder : BunifuForm
    {
        public NewOrder()
        {
            InitializeComponent();
            ListTables();
            ListProducts();
        }

        private void ListTables()
        {
            try
            {
                var table = Task.Run(() => TableController.AllTables());
                table.Wait();

                for (int i = 0; i < table.Result.data.Count; i++)
                {
                if (table.Result.data[i].state.Equals("AVALIABLE"))
                {
                    cbxTable.Items.Add(
                        table.Result.data[i].table_name);
                    cbxTable.AutoCompleteCustomSource.Add(
                         table.Result.data[i].table_name);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ListProducts()
        {
            try
            {
                var product = Task.Run(() => ProductController.AllProducts());
                product.Wait();

                for (int i = 0; i < product.Result.data.Count; i++)
                {
                    
                    cbxProduct.Items.Add(
                       product.Result.data[i].product_name);

                    cbxProduct.AutoCompleteCustomSource.Add(
                        product.Result.data[i].product_name);
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void NewOrder_Load(object sender, EventArgs e)
        {
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string table = cbxTable.Text;
                string product = cbxProduct.Text;

                //if (!cbxTable.Items.Contains(table) || table == "")
                //{
                //    MessageBox.Show($"Table {table} doesn't exist.",
                //    "Table doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                //    return;
                //}
                if (!cbxProduct.Items.Contains(product) || product == "")
                {
                    MessageBox.Show($"Product {product} doesn't exist",
                    "Product doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                    return;
                }

                var id = Task.Run(() => ProductController.GetAllProductbyName(product));
                id.Wait();
                //int product_id = int.Parse(id.Result.ToString());

                //var tblid = Task.Run(() => ableController.GetTabletbyName(table));
                //tblid.Wait();
                Console.WriteLine(id.Result);



                foreach (Data item in id.Result)
                {
                    Console.WriteLine("Product NAME = "+item.product_name);
                }

                //int table_id = int.Parse(id.Result.ToString());
                
                double price = 0;
                int quantity = 0;
                double subtotal = 0;

                //var createOrder = Task.Run(() => OrderController.NewOrder(product_id,table_id,price,quantity,subtotal));
                //createOrder.Wait();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
