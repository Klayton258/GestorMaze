using Gestor_Maze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Controllers
{
    class TableController
    {
        private static string baseURL = "http://127.0.0.1:8000/api/tables/"; // Endpoint

        /**Get All TABLES
         * 
         */
        public static async Task<TableModel> AllTables()
        {
            TableModel responseValue = new TableModel();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = TableModel.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        /**POST TABLE
        */
        public static async Task<TableModel> NewTable(string name, int lot)
        {
            int state_id = 1;
            TableModel responseValue = new TableModel();
            using (var httpClient = new HttpClient())
            {
                TableData obj = new TableData() 
                {
                   state_id = state_id,
                   table_name = name,
                   lot = lot
                };

                var json = TableModel.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(baseURL, stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = TableModel.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = TableModel.JsonDesserialize(resp);
                }
                return responseValue;
            }
        }

        /**PUT TABLE
        */
        public static async Task<TableModel> UpdateTable(int id, string name, int lot, int state)
        {
            TableModel responseValue = new TableModel();
            using (var httpClient = new HttpClient())
            {
                #region UPDATE TABLE
                
                    TableData obj = new TableData() 
                    { 
                        state_id = state,
                        table_name = name ,
                        lot = lot,
                    };

                    var json = TableModel.JsonSerialize(obj);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(baseURL + id, stringContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var bad = await response.Content.ReadAsStringAsync();
                        responseValue = TableModel.JsonDesserialize(bad);
                    }
                    else
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        responseValue = TableModel.JsonDesserialize(resp);
                    }

                
                #endregion


                return responseValue;
            }
        }

        /** DELETE TABLE
        */
        public static async Task<int> DeleteProduct(int id)
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

        /** GET TABLE BY ID
         */
        public static async Task<TableModel> GetTableById(int id)
        {
            TableModel responseValue = new TableModel();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL + id);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = TableModel.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        public static async Task<List<TableData>> GetTabletbyName(string name)
        {
            TableModel responseValue = new TableModel();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL + $"getId/{name}");

                var resp = await response.Content.ReadAsStringAsync();

                responseValue = TableModel.JsonDesserialize(resp);

                int id = responseValue.data[0].id;

                Task<TableModel> task = GetTableById(id);
                task.Wait();

                List<TableData> list = new List<TableData>();

                foreach (TableData item in task.Result.data)
                {
                    list.Add(item);
                }

                return list;
            }
        }
    }
}
