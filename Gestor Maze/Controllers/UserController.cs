using Gestor_Maze.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Controllers
{
    class UserController
    {
        private static string baseURL = Gestor_Maze.Properties.Resources.baseUrlusers; // Endpoint


        /**USER LOGIN
         * 
         */
        public static async Task<User> Login(string username, string password)
        {
            User responseValue = new User();

            using (var httpClient = new HttpClient())
            {
                UserData obj = new UserData()
                {
                    username = username,
                    pass = password

                };
                //var response = await httpClient.GetAsync($"{baseURL}/report");
                //var responseString = await response.Content.ReadAsStringAsync();

                var json = User.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseURL}login", stringContent);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var bad = await response.Content.ReadAsStringAsync();
                    responseValue = User.JsonDesserialize(bad);
                }
                else
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    responseValue = User.JsonDesserialize(resp);
                }
                return responseValue;
            }
        }
    }
}
