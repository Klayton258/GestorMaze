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

            if (!role.Equals("10") && !role.Equals("100"))
            {
                btnManagementMenu.Hide();
                btnRegister.Hide();
                btnEdit.Hide();
            }
            else if (role.Equals("9"))
            {
                btnManagementMenu.Show();
                btnRegister.Show();
                btnEdit.Show();
                gbCreateUser.Hide();
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
            object name = lblUser.Text;

            TableOrders tborders = new TableOrders(product,name);
            tborders.ShowDialog();

            if (tborders.DialogResult == DialogResult.OK)
            {
                ListOrders();
            }



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
            NewOrder newOrder = new NewOrder();
            newOrder.ShowDialog();

            if (newOrder.DialogResult == DialogResult.OK)
            {
                ListOrders();
            }
        }

        private void txtName_Click(object sender, EventArgs e)
        {

        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            //new Forms.Keyboard().ShowDialog();
        }


        private void editPToolStripMenuItem_Click(object sender, EventArgs e)
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
            string id = txtID.Text;
            string name = txtName.Text;
            int quantity = int.Parse(txtQuantity.Text);
            double price = double.Parse(txtPrice.Text);

            if (quantity != 0 && price != 0 && id != "" && name != "")
            {
                var updateProduct = Task.Run(() => ProductController.UpdateProduct(id, name, price, quantity));

                txtID.Text = ""; txtName.Text = ""; txtPrice.Text = ""; txtQuantity.Text = "";

                btnRegister.Enabled = true;
                btnEdit.Enabled = false;
                listProducts();
            }
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = true;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            var product = Task.Run(() => ProductController.AllProducts());
            product.Wait();


            tableProducts.Rows.Clear(); // Clean the table Products
                int count = 0;
            if (product.Result.data != null)
            {
                for (int i = 0; i <= product.Result.data.Count; i++)
                {
                    count++;
                }
            }
                lblVisitors.Text = count.ToString();
        }

        private void btnRel_Click(object sender, EventArgs e)
        {
            string begin = dpStartDate.Value.Date.ToString("yyyy-MM-dd");
            string end = dpEndDate.Value.Date.ToString("yyyy-MM-dd");

            new Forms.Rel(begin, end).ShowDialog();
        }

        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login loginForm = new Login();

            loginForm.Show();

            this.Hide();
        }

        private void bunifuTextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string age = dpkBirthday.Value.Date.ToString("yyyy-MM-dd");

            string gener = "";
            if (rbtnFemenine.Checked)
            {
                gener = rbtnFemenine.Text;
                rbtnMale.Checked = false;
            }
            else if (rbtnMale.Checked)
            {
                gener = rbtnMale.Text;
                rbtnFemenine.Checked = false;
            }


            string phone = txtPhone.Text;
            string email = txtEmailUser.Text.Trim();
            string document = txtDocument.Text.Trim();
            string address = txtAddress.Text.Trim();
            string bankAccount = txtBankAccount.Text.Trim();
            string salary = txtSalary.Text.Trim();
            string permission = cbcPermissions.Text;
            
            if (cbcPermissions.Text == "Colaborator")
            {
                permission = "1";
            }
            else if (cbcPermissions.Text == "Atendent")
            {
                permission = "2";
            }
            else if (cbcPermissions.Text == "Management")
            {
                permission = "9";
            }
            else if (cbcPermissions.Text == "Administrator")
            {
                permission = "10";
            }

            if (phone.Equals("") || email.Equals("") || document.Equals("") || address.Equals("") || bankAccount.Equals("") || salary.Equals("") 
                || permission.Equals("") || fullname.Equals("") || username.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Fill all the fields");
            }

            string path = "https://mazedeve.com";


            var newUser = Task.Run(() => UserController.NewUser(fullname, username, password, age, gener, phone, email, document, address, bankAccount, salary, path, permission));
            newUser.Wait();
        }

        private void userPicture_Click(object sender, EventArgs e)
        {

            FileDialog fileDialog2 = openFileDialog1;
            fileDialog2.ShowDialog();
            var path = fileDialog2.FileName;
            //userPicture.ImageLocation = path;
            //Console.WriteLine("=========> " + path);
        }

        private void btnActiveOrders_Click(object sender, EventArgs e)
        {
            btnTablesMenu_Click(sender, e);
        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            UsersManage userManage = new UsersManage();
            userManage.ShowDialog();
        }
    }
}
