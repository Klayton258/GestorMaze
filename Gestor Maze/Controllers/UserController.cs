using Gestor_Maze.Models;
using System;
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

                var json = User.JsonSerialize(obj);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseURL}/login", stringContent);

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

        /** CREATE USER
         * 
         */
        public static async Task<User> NewUser(string fullname, string username, string password, string age, string gener, string phone, string email, string document, string address, string bankAccount, string salary, string pictureBox1, string permission)
        {
            User responseValue = new User();
            using (var httpClient = new HttpClient())
            {
            
            
                UserData obj = new UserData()
                {
                    name = fullname,
                    username = username,
                    pass = password,
                    age = age,
                    gener = gener,
                    phone = phone,
                    email = email,
                    document = document,
                    address = address,
                    bank_account = bankAccount,
                    salary = salary,
                    picture = pictureBox1,
                    permission = permission

                };
                
               

                var json = User.JsonSerialize(obj);

                var stringContent = new StringContent(json , Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{baseURL}/new", stringContent);

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

        /** GET ALL USERS
       * 
       */
        public static async Task<User> AllUsers()
        {
            User responseValue = new User();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseURL);
                var responseString = await response.Content.ReadAsStringAsync();

                responseValue = User.JsonDesserialize(responseString); // Desserialize Json to Object

            }

            return responseValue;
        }

        /**USER ACTIVATE
         * 
         */
        public static async Task<int> ActiveUser(object id)
        {
            User responseValue = new User();

            using (var httpClient = new HttpClient())
            {


                var response = await httpClient.GetAsync($"{baseURL}/activeuser/" + id);

                var resp = await response.Content.ReadAsStringAsync();
                responseValue = User.JsonDesserialize(resp);
                Console.WriteLine(responseValue);

                return 202;
            }
        }

        /**USER DELETE
         * 
         */
        public static async Task<int> DeleteUser(object id)
        {
            User responseValue = new User();

            using (var httpClient = new HttpClient())
            {


                var response = await httpClient.DeleteAsync($"{baseURL}/delete/" + id);

                var resp = await response.Content.ReadAsStringAsync();
                responseValue = User.JsonDesserialize(resp);
                Console.WriteLine(responseValue);

                return 202;
            }
        }
    }
}
