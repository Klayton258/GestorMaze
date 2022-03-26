using Bunifu.UI.WinForms.BunifuButton;
using Gestor_Maze.Controllers;
using Gestor_Maze.Forms;
using Gestor_Maze.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gestor_Maze
{
    public partial class FormPrincipal : Form
    {

        public FormPrincipal(string user, string role)
        {
            InitializeComponent();
            lblVersion.Text = Gestor_Maze.Properties.Resources.version;

            showActivePage.Hide();
            closeTabs.Hide();
            lblUser.Text = user;
            lblPerm.Text = role;

            if (!role.Equals("10"))
            {
                btnManagementMenu.Hide();
            }

        }
           

        private void listProducts()
        {
            try
            {

                var p = Task.Run(() => ProductController.AllProducts());
                p.Wait();

                tableProducts.Rows.Clear(); // Clean the table Products

                for (int i = 0; i < p.Result.data.Count; i++)
                {
                    tableProducts.Rows.
                        Add(
                            p.Result.data[i].id,
                            p.Result.data[i].product_name,
                            p.Result.data[i].price,
                            p.Result.data[i].quantity
                            ); // Add values in the table
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            } //end try catch
        }

        private void ListOrders()
        {
            try
            {

                var p = Task.Run(() => TableController.AllTables());
                p.Wait();

                clientsTables.Rows.Clear(); // Clean the table Products

                for (int i = 0; i < p.Result.data.Count; i++)
                {
                    if (p.Result.data[i].state.Equals("OCCUPED"))
                    {
                        clientsTables.Rows.
                            Add(
                                p.Result.data[i].id,
                                p.Result.data[i].table_name,
                                p.Result.data[i].lot
                                ); // Add values in the table
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An error occured , please check your connection or contact the admin.",
                "Tables Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            }

        }

        private void activatePage(Control c)
        {
            showActivePage.Height = c.Height;
            showActivePage.Top = c.Top;
            showActivePage.Show();
            closeTabs.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "HOME";
            showActivePage.Hide();
            closeTabs.Hide();
            topMenu.GradientBottomLeft = Color.DeepSkyBlue;
            topMenu.GradientBottomRight = Color.DeepSkyBlue;
            topMenu.GradientTopLeft = Color.DeepSkyBlue;
            topMenu.GradientTopRight = Color.DeepSkyBlue;
            PageController.SelectTab(homePage);
            #endregion


        }

        private void btnTablesMenu_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "TABLES";
            activatePage(btnTablesMenu);
            PageController.SelectTab(teblesPage);
            btnTablesMenu.BackColor = Color.Green;
            #endregion

            ListOrders();
        }

        private void btnOrdersMenu_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "ORDERS";
            activatePage(btnOrdersMenu);
            PageController.SelectTab(ordersPage);
            btnOrdersMenu.BackColor = btnOrdersMenu.Activecolor;
            #endregion

        }

        private void PageController_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void teblesPage_Click(object sender, EventArgs e)
        {

        }

        private void btnProductsMenu_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "PRODUCTS"; // Top Menu Title
            activatePage(btnProductsMenu); // Side Menu Pointer
            topMenu.GradientBottomLeft = Color.Teal; // Top Menu color
            topMenu.GradientBottomRight = Color.Teal; // Top Menu color
            topMenu.GradientTopLeft = Color.Teal; // Top Menu color
            topMenu.GradientTopRight = Color.Teal; // Top Menu color
            PageController.SelectTab(productsPage); // Select Products Page
            #endregion

            listProducts();

        } // end btn Product Menu (Click)

        private void btnManagementMenu_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "MANAGEMENT";
            activatePage(btnManagementMenu);
            topMenu.GradientBottomLeft = Color.Gray;
            topMenu.GradientBottomRight = Color.Gray;
            topMenu.GradientTopLeft = Color.Gray;
            topMenu.GradientTopRight = Color.Gray;
            PageController.SelectTab(managementPage);
            #endregion

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            try
            {
                string name = txtName.Text.Trim();
                double price = double.Parse(txtPrice.Text.Trim());
                int quantity = int.Parse(txtQuantity.Text.Trim());

                var response = Task.Run(() => ProductController.NewProduct(name, price, quantity));
                response.Wait();

                #region Message

                if (response.Result.code == 1010)
                {
                    MessageBox.Show(response.Result.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(response.Result.msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                #endregion

                txtName.Text = ""; txtPrice.Text = ""; txtQuantity.Text = "";
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                    "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message

            }


            listProducts();

        }

        private void Orders_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "ORDERS";
            activatePage(btnOrdersMenu);
            topMenu.GradientBottomLeft = Color.Red;
            topMenu.GradientBottomRight = Color.Red;
            topMenu.GradientTopLeft = Color.Red;
            topMenu.GradientTopRight = Color.Red;
            PageController.SelectTab(ordersPage);
            btnOrdersMenu.BackColor = btnOrdersMenu.Activecolor;
            #endregion
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            object product = clientsTables.CurrentRow.Cells[0].Value;


            TableOrders tborders = new TableOrders(product);
            tborders.ShowDialog();



        }

        private void txtSearch_TextChange(object sender, EventArgs e)
        {
            try
            {
                string name = txtSearch.Text;
                var product = Task.Run(() => ProductController.SearchProduct(name));
                product.Wait();


                tableProducts.Rows.Clear(); // Clean the table Products
                if (product.Result.data != null)
                {

                    for (int i = 0; i < product.Result.data.Count; i++)
                    {
                        tableProducts.Rows.
                                   Add(
                                       product.Result.data[i].id,
                                       product.Result.data[i].product_name,
                                       product.Result.data[i].price,
                                       product.Result.data[i].quantity
                                       ); // Add values in the table
                    }

                }
                if (product.Result.code == 404)
                {
                    listProducts();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("An error occured , please check your connection or contact the admin.",
                "Tables Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message

            }



        }

        private void closeTabs_Click(object sender, EventArgs e)
        {
            #region Page Style
            pageTitle.Text = "HOME";
            showActivePage.Hide();
            closeTabs.Hide();
            topMenu.GradientBottomLeft = Color.DeepSkyBlue;
            topMenu.GradientBottomRight = Color.DeepSkyBlue;
            topMenu.GradientTopLeft = Color.DeepSkyBlue;
            topMenu.GradientTopRight = Color.DeepSkyBlue;
            PageController.SelectTab(homePage);
            #endregion

        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            object id = tableProducts.CurrentRow.Cells[0].Value;
            object name = tableProducts.CurrentRow.Cells[1].Value;

            if (MessageBox.Show($"Are you sure you want to delete '{name}'.", "Deleted", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                try
                {

                    var response = Task.Run(() => ProductController.DeleteProduct(id));
                    response.Wait();

                    if (response.Result == 202)
                    {
                        listProducts();
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred deleting '{name}'.", "Error Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                    "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message

                }

            }

        }

        private void bntCreateTable_Click(object sender, EventArgs e)
        {



            try
            {
                string name = txtTableName.Text.Trim();
                int lot = int.Parse(txtTableLot.Text.Trim());

                var response = Task.Run(() => TableController.NewTable(name, lot));
                response.Wait();

                #region Message

                if (response.Result.code == 1010)
                {
                    MessageBox.Show(response.Result.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(response.Result.msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                #endregion

            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            }



            txtTableName.Text = "";
            txtTableLot.Text = "";
        }

        private void btnViewTables_Click(object sender, EventArgs e)
        {
            new ListOfTables().ShowDialog();
        }

        private void tableProducts_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void btnNewTable_Click(object sender, EventArgs e)
        {
            new NewOrder().ShowDialog();
        }

        private void txtName_Click(object sender, EventArgs e)
        {

        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            //new Forms.Keyboard().ShowDialog();
        }


        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnRegister.Enabled = false;

            txtID.Text = tableProducts.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = tableProducts.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = tableProducts.CurrentRow.Cells[2].Value.ToString();
            txtQuantity.Text = tableProducts.CurrentRow.Cells[3].Value.ToString();


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string name = txtName.Text;
            int quantity = int.Parse(txtQuantity.Text);
            double price = double.Parse(txtPrice.Text);

            if (quantity != 0 && price != 0 && id != 0 && name != "")
            {


                //Update Product
                int newQut = 0;
                if (quantity < 0)
                {
                    //subtrair quantidade
                    newQut = 10 - 1;
                }
                else
                {
                    //Add quantity
                    newQut = 10 + 2;
                }

                txtID.Text = ""; txtName.Text = ""; txtPrice.Text = ""; txtQuantity.Text = "";

                btnRegister.Enabled = true;
                btnEdit.Enabled = false;
            }
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = true;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            ListOrders();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        


        private void btnRel_Click(object sender, EventArgs e)
        {
            string begin = dpStartDate.Value.Date.ToString("yyyy-MM-dd");
            string end = dpEndDate.Value.Date.ToString("yyyy-MM-dd");

            new Forms.Rel(begin, end).ShowDialog();
        }
    }
}
