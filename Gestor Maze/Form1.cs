using Gestor_Maze.Controllers;
using Gestor_Maze.Forms;
using Gestor_Maze.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public FormPrincipal()
        {
            InitializeComponent();
            showActivePage.Hide();
            closeTabs.Hide();
        }
        public string baseURL = "http://127.0.0.1:8000/api/products/"; // Endpoint

        private async void listProducts()
        {
            try
            {

                Product responseValue = new Product();

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(baseURL);
                    var responseString = await response.Content.ReadAsStringAsync();

                    responseValue = Product.JsonDesserialize(responseString); // Desserialize Json to Object

                    tableProducts.Rows.Clear(); // Clean the table Products

                    for (int i = 0; i < responseValue.data.Count; i++)
                    {
                        tableProducts.Rows.
                            Add(
                                responseValue.data[i].id,
                                responseValue.data[i].name,
                                responseValue.data[i].price,
                                responseValue.data[i].quantity
                                ); // Add values in the table
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            } //end try catch
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
            pageTitle.Text = "HOME";
            showActivePage.Hide();
            closeTabs.Hide();
            topMenu.GradientBottomLeft = Color.DeepSkyBlue;
            topMenu.GradientBottomRight = Color.DeepSkyBlue;
            topMenu.GradientTopLeft = Color.DeepSkyBlue;
            topMenu.GradientTopRight = Color.DeepSkyBlue;
            PageController.SelectTab(homePage);
          
        }

        private void btnTablesMenu_Click(object sender, EventArgs e)
        {
            pageTitle.Text = "TABLES";
            activatePage(btnTablesMenu);
            PageController.SelectTab(teblesPage);
            topMenu.GradientBottomLeft = Color.Green;
            topMenu.GradientBottomRight = Color.Green;
            topMenu.GradientTopLeft = Color.Green;
            topMenu.GradientTopRight = Color.Green;
            btnTablesMenu.BackColor = Color.Green;

            //Task<Product> p =  ProductController.Test();

            //Console.WriteLine(p.Result);


        }

        private void btnOrdersMenu_Click(object sender, EventArgs e)
        {
            pageTitle.Text = "ORDERS";
            activatePage(btnOrdersMenu);
            topMenu.GradientBottomLeft = Color.Red;
            topMenu.GradientBottomRight = Color.Red;
            topMenu.GradientTopLeft = Color.Red;
            topMenu.GradientTopRight = Color.Red;
            PageController.SelectTab(ordersPage);
            btnOrdersMenu.BackColor = btnOrdersMenu.Activecolor;
        }

        private void PageController_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void teblesPage_Click(object sender, EventArgs e)
        {

        }

        private void btnProductsMenu_Click(object sender, EventArgs e)
        {
            pageTitle.Text = "PRODUCTS"; // Top Menu Title
            activatePage(btnProductsMenu); // Side Menu Pointer
            topMenu.GradientBottomLeft = Color.Teal; // Top Menu color
            topMenu.GradientBottomRight = Color.Teal; // Top Menu color
            topMenu.GradientTopLeft = Color.Teal; // Top Menu color
            topMenu.GradientTopRight = Color.Teal; // Top Menu color
            PageController.SelectTab(productsPage); // Select Products Page

            listProducts();

        } // end btn Product Menu (Click)

        private void btnManagementMenu_Click(object sender, EventArgs e)
        {
            pageTitle.Text = "MANAGEMENT";
            activatePage(btnManagementMenu);
            topMenu.GradientBottomLeft = Color.Gray;
            topMenu.GradientBottomRight = Color.Gray;
            topMenu.GradientTopLeft = Color.Gray;
            topMenu.GradientTopRight = Color.Gray;
            PageController.SelectTab(managementPage);

            
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void txtRegister_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            double price = double.Parse(txtPrice.Text);
            int quantity = int.Parse(txtQuantity.Text);

            //Data obj = new Data(name, price, quantity);
            //p = ProductController.newProduct(obj);



            using (var httpClient = new HttpClient())
            {
                Data obj = new Data(name, price, quantity);
                var json = Product.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(baseURL, stringContent);

                #region API Response Message

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    Product b = Product.JsonDesserialize(bad);
                    MessageBox.Show(b.msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    var resp = await response.Content.ReadAsStringAsync();
                    Product p = Product.JsonDesserialize(resp);

                MessageBox.Show(p.msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                #endregion

            }
            

            txtName.Text = ""; txtPrice.Text = "";  txtQuantity.Text = ""; 

            listProducts();

        }

        private void Orders_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            pageTitle.Text = "ORDERS";
            activatePage(btnOrdersMenu);
            topMenu.GradientBottomLeft = Color.Red;
            topMenu.GradientBottomRight = Color.Red;
            topMenu.GradientTopLeft = Color.Red;
            topMenu.GradientTopRight = Color.Red;
            PageController.SelectTab(ordersPage);
            btnOrdersMenu.BackColor = btnOrdersMenu.Activecolor;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //object product = tableProducts.CurrentRow.Cells[0].Value;
            object product = "46565";
            

            new TableOrders(product).ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
        }

        private async void txtSearch_TextChange(object sender, EventArgs e)
        {
            Product responseValue = new Product();
            using (var httpClient = new HttpClient())
            {  
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseURL + $"search/{txtSearch.Text}"))
            {
                    //requestMessage.Headers.Add("name", txtSearch.Text);
                    var response = await httpClient.SendAsync(requestMessage);

                    var resp = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(" - "+ resp);



                    //responseValue = Product.JsonDesserialize(resp);


                    responseValue = Product.JsonDesserialize(resp); // Desserialize Json to Object

                    tableProducts.Rows.Clear(); // Clean the table Products

                    for (int i = 0; i < responseValue.data.Count; i++)
                    {
                        tableProducts.Rows.
                            Add(
                                responseValue.data[i].id,
                                responseValue.data[i].name,
                                responseValue.data[i].price,
                                responseValue.data[i].quantity
                                ); // Add values in the table
                    }
                }
            }
        }

        private void closeTabs_Click(object sender, EventArgs e)
        {
            pageTitle.Text = "HOME";
            showActivePage.Hide();
            closeTabs.Hide();
            topMenu.GradientBottomLeft = Color.DeepSkyBlue;
            topMenu.GradientBottomRight = Color.DeepSkyBlue;
            topMenu.GradientTopLeft = Color.DeepSkyBlue;
            topMenu.GradientTopRight = Color.DeepSkyBlue;
            PageController.SelectTab(homePage);
        }

        private async void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Remove Product Method
            Product responseValue = new Product();
            using (var httpClient = new HttpClient())
            {
                object id = tableProducts.CurrentRow.Cells[0].Value;
                object name = tableProducts.CurrentRow.Cells[1].Value;

                if (MessageBox.Show($"Are you sure you want to delete '{name}'.", "Deleted", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    var response = await httpClient.DeleteAsync(baseURL + id);
                   
                    switch (response.StatusCode)
                    {
                      
                        case System.Net.HttpStatusCode.NoContent:
                            MessageBox.Show("Product Deleted.", "Deleted", MessageBoxButtons.OK);
                            listProducts();
                            break;
                    }
                    MessageBox.Show($"An error occurred deleting '{name}'.", "Fail Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
