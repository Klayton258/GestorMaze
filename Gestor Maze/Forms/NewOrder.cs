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
        public static bool done = false;

        private void ListTables()
        {
            try
            {
                var table = Task.Run(() => TableController.AllTables());
                table.Wait();

                for (int i = 0; i < table.Result.data.Count; i++)
                {
                if (table.Result.data[i].state.Equals("ACTIVE") && table.Result.data[i].state_id != 3)
                {
                        Console.WriteLine(table.Result.data[i].table_name +" "+ table.Result.data[i].state_id);
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
            btnAdd.Enabled = false;
            try
            {
                string table = cbxTable.Text.Trim();
                string product = cbxProduct.Text.Trim();

                #region VALIDATE VALUES
                if (!cbxTable.Items.Contains(table) || table == "")
                {
                    MessageBox.Show($"Table {table} doesn't exist.",
                    "Table doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                    btnAdd.Enabled = true;
                    return;
                }
                if (!cbxProduct.Items.Contains(product) || product == "")
                {
                    MessageBox.Show($"Product {product} doesn't exist",
                    "Product doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                    btnAdd.Enabled = true;
                    return;
                }
                #endregion

                var pro = Task.Run(() => ProductController.GetAllProductbyName(product));
                pro.Wait();

                var tableall = Task.Run(() => TableController.GetTableAllbyName(table));
                tableall.Wait();


                int table_id = tableall.Result.data[0].id;
                int product_id = pro.Result.data[0].id;
                double price = pro.Result.data[0].price;
                int quantity = int.Parse(txtQuantity.Text.Trim());

                if (quantity > int.Parse(txtStock.Text) || int.Parse(txtQuantity.Text.Trim()) <=0 )
                {
                    MessageBox.Show("Quantity not avaliable or quantity can't be 0",
                    "Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                    btnAdd.Enabled = true;
                    return;
                }
                double subtotal = price * quantity;

                var createOrder = Task.Run(() => OrderController.NewOrder(product_id, table_id, price, quantity, subtotal));
                createOrder.Wait();
                DialogResult = DialogResult.OK;
                if (createOrder.Result.code != 201)
                {
                    MessageBox.Show("An error occured , please check your connection or contact the admin.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                    return;
                }
                
            }
            catch (Exception)
            {
                btnAdd.Enabled = true;
                MessageBox.Show("An error occured , please check your connection or contact the admin.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message

            }
            btnAdd.Enabled = true;
            Dispose();
        }

        private void cbxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string product = cbxProduct.Text.Trim();
            if (product !="")
            {
            var pro = Task.Run(() => ProductController.GetAllProductbyName(product));
            pro.Wait();

            txtStock.Text = pro.Result.data[0].quantity.ToString();
            }
            else
            {
                txtStock.Text = "0";

            }
        }
    }
}
