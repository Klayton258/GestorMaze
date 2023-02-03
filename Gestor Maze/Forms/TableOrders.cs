using Bunifu.Framework.UI;
using Bunifu.UI.WinForms.BunifuButton;
using Gestor_Maze.Controllers;
using Gestor_Maze.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_Maze.Forms
{
    public partial class TableOrders : Form
    {
        public TableOrders(object obj, object Username)
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            lblTableID.Text = obj.ToString();
            lblUsername.Text = Username.ToString();
            ListOrders(obj);
            ListProducts();
            ListTables();
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
                        cbcProducts.AutoCompleteCustomSource.Add(product.Result.data[i].product_name);
                    }
                
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            } //end try catch
        }

        private void ListTables()
        {
            try
            {
                var tables = Task.Run(() => TableController.AllTables());
                tables.Wait();

                for (int i = 0; i < tables.Result.data.Count; i++)
                {

                    if (tables.Result.data[i].state.Equals("OCCUPED"))
                    {
                        cbcTables.Items.Add(tables.Result.data[i].table_name);
                        cbcTables.AutoCompleteCustomSource.Add(tables.Result.data[i].table_name);
                    }
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
            
            TblOrdTable.Rows.Clear();
            if(orders.Result.data.Count > 0) { 
            for (int i = 0; i < orders.Result.data.Count; i++)
            {
                
                TblOrdTable.Rows.Add
                    (
                        orders.Result.data[i].id,
                        orders.Result.data[i].product_name,
                        orders.Result.data[i].quantity,
                        orders.Result.data[i].price,
                        orders.Result.data[i].subtotal
                    );

                total += orders.Result.data[i].subtotal;
            }
                lblTotal.Text = total.ToString() + " Mzn";
                cbcTables.Text = orders.Result.data[0].table_name;
            }
            else
            {
                var table = Task.Run(() => TableController.GetTableById(id));
                table.Wait();

                cbcTables.Text = table.Result.data[0].table_name;
            }

            if (lblUuid.Text.Equals("none"))
            {
                lblUuid.Text = UUID();
            }
        }
        public BigInteger GuidToBigInteger(Guid guid)
        {
            BigInteger l_retval = 0;
            byte[] ba = guid.ToByteArray();
            int i = ba.Count();
            foreach (byte b in ba)
            {
                l_retval += b * BigInteger.Pow(256, --i);
            }
            return l_retval;
        }

        public String UUID()
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();

            return myuuidAsString;
        }

        private void cbcProducts_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string product = cbcProducts.Text.Trim();
            var pro = Task.Run(() => ProductController.GetAllProductbyName(product));
            pro.Wait();

            lblStockAv.Text = pro.Result.data[0].quantity.ToString();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are u sure u want to close?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dispose();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            double total = double.Parse(Regex.Match(lblTotal.Text, @"\d+\.*\d*").Value);
            double iva = total * 0.17;

            string[] uuid = lblUuid.Text.Split('-');

            #region INVOICE CONTENT
            e.Graphics.DrawString("REPORT RESTAURANT", new Font("Trebuchet MS", 10), Brushes.Black, new Point(65, 50));
            e.Graphics.DrawString("______________________________________________________________", new Font("Consolas", 5), Brushes.Black, new Point(20, 70));
            e.Graphics.DrawString("DATE: " + DateTime.Now.ToLocalTime() , new Font("Consolas", 5), Brushes.Black, new Point(20, 80));
            e.Graphics.DrawString("TICKET N: " , new Font("Consolas", 5), Brushes.Black, new Point(230, 80));
            e.Graphics.DrawString("#"+uuid[0], new Font("Consolas", 5), Brushes.Black, new Point(225, 90));
            e.Graphics.DrawString("Table: "+cbcTables.Text, new Font("Consolas", 5), Brushes.Black, new Point(20, 90));
            e.Graphics.DrawString("______________________________________________________________", new Font("Consolas", 5), Brushes.Black, new Point(20,95));

            e.Graphics.DrawString("Product", new Font("Consolas", 5, FontStyle.Bold), Brushes.Black, new Point(20, 105));
            e.Graphics.DrawString("Unit", new Font("Consolas", 5, FontStyle.Bold), Brushes.Black, new Point(160, 105));
            e.Graphics.DrawString("Subtotal", new Font("Consolas", 5, FontStyle.Bold), Brushes.Black, new Point(220, 105));

            int heigth = 120;
            foreach (DataGridViewRow row in TblOrdTable.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                e.Graphics.DrawString(Convert.ToString(row.Cells[1].Value), new Font("Consolas", 5), Brushes.Black, new Point(20, heigth));
                e.Graphics.DrawString("("+Convert.ToString(row.Cells[2].Value) +")", new Font("Consolas", 5), Brushes.Black, new Point(162, heigth));
                e.Graphics.DrawString(Convert.ToString(row.Cells[4].Value)+" Mzn", new Font("Consolas", 5), Brushes.Black, new Point(222, heigth));
                heigth += 10;

                }
            }
            e.Graphics.DrawString("IVA 17%:                                               "+ iva.ToString()+ " MT", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth += 10));
            e.Graphics.DrawString("______________________________________________________________", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("TOTAL: " +lblTotal.Text, new Font("Consolas", 10, FontStyle.Bold), Brushes.Black, new Point(120, heigth += 10));
            e.Graphics.DrawString("______________________________________________________________", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth += 10));

            e.Graphics.DrawString("SELLER: "+lblUsername.Text, new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("PAYED WITH:                                              Cash", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=20));
            e.Graphics.DrawString("MONEY GIVEN:                                         xxxx ,MT", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("CHANGE:                                               xxx ,MT", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("______________________________________________________________", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth += 10));
            e.Graphics.DrawString("                   *********THANKS********                     ", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("______________________________________________________________", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth += 5));
            e.Graphics.DrawString("ADDRESS: Av. Kim IL Sung, 1420", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("NUIT: 152055", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            e.Graphics.DrawString("TEL: +258 848 293 580", new Font("Consolas", 5), Brushes.Black, new Point(20, heigth+=10));
            #endregion

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 286, 600);
            printPreviewDialog1.ShowDialog();

            //PaymentMethod pymthod = new PaymentMethod();
            //pymthod.ShowDialog();
        }

        public double SubtotalToUpdate(int quantity, double price, double actualSubtotal)
        {
            double newSubtotal = 0;

            return newSubtotal = (quantity * price) + actualSubtotal;
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool verify = false;
                string table = cbcTables.Text.Trim();
                string product = cbcProducts.Text.Trim();

                #region VALIDATE VALUES
                if (!cbcProducts.Items.Contains(product) || product == "")
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
                int quantity = int.Parse(txtquantity.Text.Trim());

                if (quantity > int.Parse(lblStockAv.Text) || int.Parse(lblStockAv.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Quantity not avaliable or quantity can't be 0",
                    "Stock Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                    btnAdd.Enabled = true;
                    return;
                }

                double subtotal = price * quantity;


                #region VARS TO UPDATE ORDER
                int qutty_update = 0;
                int id_update = 0;
                int state_update = 1;
                double sub_update = 0;
                int table_update = int.Parse(lblTableID.Text);
                #endregion

                #region FOREACH TO FIND THE ORDER TO UPLOAD
                foreach (DataGridViewRow row in TblOrdTable.Rows)
                {
                    string value = (string)row.Cells[1].Value;
                    if (value != null)
                    {
                        if (value.Equals(product))
                        {
                            id_update = (int)row.Cells[0].Value;
                            state_update = 1;
                            qutty_update = int.Parse(txtquantity.Text) + (int)row.Cells[2].Value;
                            sub_update = (double)row.Cells[4].Value;

                            verify = true;
                        }
                    }
                }
                #endregion
                
                if (verify)
                {
                    
                    double newSubtotal = SubtotalToUpdate(quantity, price, sub_update); //get the new subtotal
                    var update = Task.Run(() => OrderController.UpdateOrder(id_update, product_id, table_update, quantity, newSubtotal));
                    update.Wait();

                    if (update.Result.code != 204)
                    {
                        MessageBox.Show(update.Result.msg,
                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Return the error message
                    }
                    cbcProducts.Text = "";
                    txtquantity.Value = 0;
                    lblStockAv.Text = "0";

                    TblOrdTable.Rows.Clear();
                    ListOrders(lblTableID.Text);
                    return;
                }
                else
                {
                    var createOrder = Task.Run(() => OrderController.NewOrder(product_id, table_id, price, quantity, subtotal));
                    createOrder.Wait();

                    if (createOrder.Result.code != 201)
                    {

                        MessageBox.Show(createOrder.Result.msg,
                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Return the error message
                    }
                    cbcProducts.Text = "";
                    txtquantity.Value = 0;
                    lblStockAv.Text = "0";

                    TblOrdTable.Rows.Clear();
                    ListOrders(lblTableID.Text);
                }

            }
            catch (Exception)
            {
                btnAdd.Enabled = true;
                MessageBox.Show("An error occured , please check your connection or contact the admin.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string table = cbcTables.Text.Trim();
            string product = TblOrdTable.CurrentRow.Cells[1].Value.ToString();

            #region VALIDATE VALUES
            if (!cbcProducts.Items.Contains(product) || product == "")
            {
                MessageBox.Show($"Product {product} doesn't exist",
                "Product doesn't exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
                btnAdd.Enabled = true;
                return;
            }
            if (txtquantity.Value == 0 || txtquantity.Value < 0)
            {
                MessageBox.Show($"Please fill in de quantity box how much you want to reduce",
                "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            #endregion

            var pro = Task.Run(() => ProductController.GetAllProductbyName(product));
            pro.Wait();

            var tableall = Task.Run(() => TableController.GetTableAllbyName(table));
            tableall.Wait();

            int id_update = (int)TblOrdTable.CurrentRow.Cells[0].Value;
            int table_id = tableall.Result.data[0].id;
            int product_id = pro.Result.data[0].id;
            double price = pro.Result.data[0].price;
            int quantity = int.Parse(txtquantity.Text.Trim());
            double subtotal = (double)TblOrdTable.CurrentRow.Cells[4].Value;

            int newQuantity =  (int)TblOrdTable.CurrentRow.Cells[2].Value - quantity;

            double newSubtotal = SubtotalToUpdate(newQuantity, price, 0); //get the new subtotal

            var update = Task.Run(() => OrderController.NormalUpdateOrder(id_update, product_id, table_id, quantity, newSubtotal));
            update.Wait();

            if (update.Result.code != 204)
            {
                MessageBox.Show(update.Result.msg,
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Return the error message
            }

            cbcProducts.Text = "";
            txtquantity.Value = 0;
            lblStockAv.Text = "0";

            TblOrdTable.Rows.Clear();
            ListOrders(lblTableID.Text);
            return;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
                int id = (int)TblOrdTable.CurrentRow.Cells[0].Value;
                string name = TblOrdTable.CurrentRow.Cells[1].Value.ToString();

            if (MessageBox.Show($"Are you sure you want to delete '{name}'.", "Delete order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var delete = Task.Run(() => OrderController.DeleteOrder(id));
                delete.Wait();

            if (delete.Result != 202)
            {

                    MessageBox.Show("An error ocurred removing this order please check your connection or contact the admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                TblOrdTable.Rows.Clear();
                ListOrders(lblTableID.Text);
                return;

              
            }
        }

        private void cbcTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tablename = cbcTables.Text;
            var table = Task.Run(() => TableController.GetTableAllbyName(tablename));
            table.Wait();

            int id = table.Result.data[0].id;
            lblTableID.Text = id.ToString();
            cbcTables.Text = table.Result.data[0].table_name;
            TblOrdTable.Rows.Clear();
            ListOrders(id);

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int id = int.Parse(lblTableID.Text);

            if (MessageBox.Show("ARE YOU SHURE YOU WANT TO CLOSE?", "Close Table", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("DO YOU WANT TO SAVE THE INVOICE?", "Invoce print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    printPreviewDialog1.Document = printDocument1;
                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 286, 600);
                    printPreviewDialog1.ShowDialog();

                    //printDocument1.Print();

                    var close = Task.Run(() => OrderController.CloseOrdeer(id));
                    close.Wait();

                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {

                    var close = Task.Run(() => OrderController.CloseOrdeer(id));
                    close.Wait();

                    DialogResult = DialogResult.OK;
                    Dispose();
                }
            }
        }
    }
}
