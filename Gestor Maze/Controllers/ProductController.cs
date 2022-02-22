using Gestor_Maze.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gestor_Maze.Controllers
{
    class ProductController
    {
        private static string baseURL = "http://127.0.0.1:8000/api/products/"; // Endpoint

        /**
         * Get All Products
         */ 
        public static Product allProducts()
        {
            RestClient restClient = new RestClient(); // Initialize RestClient
            restClient.httpMethod = ApiRequest.GET;
            restClient.endpoint = baseURL; // Send Endpoint

            string response = string.Empty; // initialize variable
            response = restClient.makeRequest(); // Make the Request
            Product obj = Product.JsonDesserialize(response); // Desserialize Json to Object
            
            return obj; // Return the object
        }

        /**
         *POST Product
         */
        public static Product newProduct(Data obj)
        {
            RestClient restClient = new RestClient(); // Initialize RestClient

            restClient.httpMethod = ApiRequest.POST; // Http Method
            restClient.endpoint = baseURL; // Send Endpoint with id

            string response = string.Empty; // initialize variable
            restClient.postJSON = Product.JsonSerialize(obj); // Serialize Product into Json
            response = restClient.makeRequest(); // Make the Request

            Product message = Product.JsonDesserialize(response); // Desserialize Json to Object
            return message;
        }

        /**
        *UPDATE Product by id
        */
        public static Product updateProduct(Data obj, string id)
        {
            RestClient restClient = new RestClient(); // Initialize RestClient

            restClient.httpMethod = ApiRequest.PUT; // Http Method
            restClient.endpoint = baseURL + id; // Send Endpoint with id

            string response = string.Empty; // initialize variable
            restClient.postJSON = Product.JsonSerialize(obj); // Serialize Product into Json
            response = restClient.makeRequest(); // Make the Request

            Product message = Product.JsonDesserialize(response); // Desserialize Json to Object
            Console.WriteLine(restClient.endpoint + " + " + message.msg);
            return message;
        }

        public async Task<Product> Test()
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
    }
}
