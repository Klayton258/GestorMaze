using Gestor_Maze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Controllers
{
    class OrderController
    {
        private static string baseURL = Gestor_Maze.Properties.Resources.baseUrlorders; // Endpoint
       
        /** Get All ORDERS
         * 
         */
        public static async Task<Order> AllTables()
        {
            
            Order responseValue = new Order();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = Order.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        /** POST ORDER
        */
        public static async Task<Order> NewOrder(int product_id, int table_id, double price, int quantity, double subtotal)

        {
            int state_id = 1;

            Order responseValue = new Order();
            using (var httpClient = new HttpClient())
            {
                OrderData obj = new OrderData()
                {
                    product_id = product_id,
                    table_id = table_id,
                    state_id = state_id,
                    price = price,
                    quantity = quantity,
                    subtotal = subtotal
                };

                var json = Order.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseURL}new", stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(resp);
                }
                return responseValue;
            }
        }

        /** NORMAL PUT ORDER
       */
        public static async Task<Order> NormalUpdateOrder(int id, int product_id, int table_id, int state_id, double price, int quantity, double subtotal)
        {
            Order responseValue = new Order();
            using (var httpClient = new HttpClient())
            {
                #region UPDATE ORDER

                OrderData obj = new OrderData()
                {
                    product_id = product_id,
                    table_id = table_id,
                    state_id = state_id,
                    price = price,
                    quantity = quantity,
                    subtotal = subtotal
                };

                var json = Order.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(baseURL + $"normalupdateorder/{id}", stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(resp);
                }


                #endregion


                return responseValue;
            }
        }

        /** PUT ORDER
        */
        public static async Task<Order> UpdateOrder(int id, int product_id, int table_id, int state_id, double price, int quantity, double subtotal)
        {
            Order responseValue = new Order();
            using (var httpClient = new HttpClient())
            {
                #region UPDATE ORDER

                OrderData obj = new OrderData()
                {
                    product_id= product_id,
                    table_id = table_id,
                    state_id= state_id,
                    price= price,
                    quantity = quantity,
                    subtotal = subtotal
                };

                var json = Order.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(baseURL + $"updateorder/{id}", stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(resp);
                }


                #endregion


                return responseValue;
            }
        }

        /** DELETE ORDER
        */
        public static async Task<int> DeleteOrder(int id)
        {
            Product responseValue = new Product();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(baseURL + id);

                var resp = await response.Content.ReadAsStringAsync();
                responseValue = Product.JsonDesserialize(resp);
                Console.WriteLine(responseValue);

                return 202;
            }
        }

        /** GET ORDER BY ID
         */
        public static async Task<Order> GetOrderById(int id)
        {
            Order responseValue = new Order();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL+id);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = Order.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        /** GET ORDER BY TABLE ID
        */
        public static async Task<Order> GetOrderByTableId(int id)
        {
            Order responseValue = new Order();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL +$"orderbytable/{id}");
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = Order.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        /** CLOSE ORDER
        */
        public static async Task<Order> CloseOrdeer(int id)
        {
            Order responseValue = new Order();
            using (var httpClient = new HttpClient())
            {
                #region CLOSE ORDER

                var response = await httpClient.GetAsync(baseURL + $"closeorder/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = Order.JsonDesserialize(resp);
                }


                #endregion


                return responseValue;
            }
        }

        /** Get All REPORT 
         */
        public static async Task<Report> GetReport(string begin, string end)
        {
            Report responseValue = new Report();

            using (var httpClient = new HttpClient())
            {
                Rel obj = new Rel()
                {
                    begin = begin,
                    end = end

                };

                var json = Report.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseURL}report", stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = Report.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = Report.JsonDesserialize(resp);
                }
                return responseValue;
            }
        }
    }
}
