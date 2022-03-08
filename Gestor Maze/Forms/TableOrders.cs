using Bunifu.Framework.UI;
using Gestor_Maze.Controllers;
using Gestor_Maze.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_Maze.Forms
{
    public partial class TableOrders : BunifuForm
    {
        public TableOrders(object obj)
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            lblTableID.Text = "#" + obj;
            ListOrders(obj);
            ListProducts();
        }

        private void ListProducts()
        {
            try
            {
                var product = Task.Run(() => ProductController.AllProducts());
                product.Wait();

                    for (int i = 0; i < product.Result.data.Count; i++)
                    {
                        cbcProducts.Items.Add(product.Result.data[i].product_name);
                    }
                
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            } //end try catch
        }

        private void ListOrders(object obj)
        {
            int id = int.Parse(obj.ToString());

            var orders = Task.Run(() => OrderController.GetOrderByTableId(id));
            orders.Wait();
            double total=0;
            for (int i = 0; i < orders.Result.data.Count; i++)
            {
                TblOrdTable.Rows.Add
                    (
                        orders.Result.data[i].product_id,
                        orders.Result.data[i].product_name,
                        orders.Result.data[i].quantity,
                        orders.Result.data[i].price,
                        orders.Result.data[i].subtotal
                    );

                total += orders.Result.data[i].subtotal;
            }
                lblTotal.Text = total.ToString() + ",00 MT";
            lblTableName.Text = orders.Result.data[0].table_name;
        }
        
        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are u sure u want to close?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dispose();
            }
        }


        private void lblTopMenu_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbcProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cbcProducts.SelectedItem.ToString();
            var id = Task.Run(() => ProductController.GetProducIdtbyName(name));
            id.Wait();

            //lblTableName.Text = id.Result.ToString();
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
