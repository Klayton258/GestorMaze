using Bunifu.Framework.UI;
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
            listProducts();
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
                    

                    for (int i = 0; i < responseValue.data.Count; i++)
                    {
                        cbcProducts.Items.Add(responseValue.data[i].name);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error print products list, please check your connection or contact the admin.",
                                "Products Page", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Return the error message
            } //end try catch
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Are u sure u want to close?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dispose();
            }
        }

        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private extern static void ReleaseCapture();
        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void lblTopMenu_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbcProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTableName.Text = cbcProducts.SelectedIndex.ToString();

            
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
