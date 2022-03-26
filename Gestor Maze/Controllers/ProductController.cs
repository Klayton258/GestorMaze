using Gestor_Maze.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Controllers
{
    class ProductController
    {
        private static string baseURL = Gestor_Maze.Properties.Resources.baseUrlproducts; // Endpoint

        /**
         * Get All Products
         */
        public static async Task<Product> AllProducts()
        {
            Product responseValue = new Product();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = Product.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        /**POST Product
        */
        public static async Task<Product> NewProduct(string name, double price, int quantity)
        {
            Console.WriteLine(" PROBLEM GET HERE " + name +" "+price+" "+ quantity);
            Product responseValue = new Product();
            using (var httpClient = new HttpClient())
            {
                Data obj = new Data() 
                {
                    product_name = name, 
                    price = price, 
                    quantity = quantity
                };
                var json = Product.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(baseURL, stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = Product.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = Product.JsonDesserialize(resp);
                }
                return responseValue;
            }
        }

        /**
        *UPDATE Product by id
        */
        public static async Task<Product> UpdateProduct(string name=null, double price=0, int quantity=0)
        {
            Product responseValue = new Product();
            using (var httpClient = new HttpClient())
            {
                #region UPDATE NAME
                if (name != null)
                {
                Data obj = new Data()
                {
                    product_name = name
                };

                    var json = Product.JsonSerialize(obj);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(baseURL, stringContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var bad = await response.Content.ReadAsStringAsync();
                        responseValue = Product.JsonDesserialize(bad);
                    }
                    else
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        responseValue = Product.JsonDesserialize(resp);
                    }

                }
                #endregion

                #region UPDATE PRICE
                else
                if (price != 0)
                {
                    Data obj = new Data()
                    {
                        
                        price = price
                    };

                    var json = Product.JsonSerialize(obj);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(baseURL, stringContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var bad = await response.Content.ReadAsStringAsync();
                        responseValue = Product.JsonDesserialize(bad);
                    }
                    else
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        responseValue = Product.JsonDesserialize(resp);
                    }

                }
                #endregion

                #region UPDATE QUANTITY
                else
                if (quantity != 0)
                {
                    Data obj = new Data()
                    {
                        quantity = quantity
                    };

                    var json = Product.JsonSerialize(obj);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(baseURL, stringContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var bad = await response.Content.ReadAsStringAsync();
                        responseValue = Product.JsonDesserialize(bad);
                    }
                    else
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        responseValue = Product.JsonDesserialize(resp);
                    }
                }
                #endregion

                return responseValue;
            }
        }


        /**
         * DELETE Product by id
         */
        public static async Task<int> DeleteProduct(object id)
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

        public static async Task<Product> SearchProduct(string name)
        {
            Product responseValue = new Product();

            using (var httpClient = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseURL + $"search/{name}"))
                {
                    var response = await httpClient.SendAsync(requestMessage);

                    var resp = await response.Content.ReadAsStringAsync();

                    responseValue = Product.JsonDesserialize(resp); // Desserialize Json to Object

                }

                return responseValue;
            }
        }

        public static async Task<int> GetProducIdtbyName(string name)
        {
            Product responseValue = new Product();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL + $"getId/{name}");

                var resp = await response.Content.ReadAsStringAsync();

                responseValue = Product.JsonDesserialize(resp);

                int id = responseValue.data[0].id;


                return id;
            }
        }


        /** GET TABLE BY ID
         */
        public static async Task<Product> GetProductById(int id)
        {
            Product responseValue = new Product();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL + id);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = Product.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        public static async Task<Product> GetAllProductbyName(string name)
        {
            Product responseValue = new Product();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL + $"getallbyname/{name}");

                var resp = await response.Content.ReadAsStringAsync();

                responseValue = Product.JsonDesserialize(resp);


                return responseValue;
            }

        }
    }
}
