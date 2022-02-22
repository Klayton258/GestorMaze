using System;
using System.IO;
using System.Net;

namespace Gestor_Maze.Models
{
    public enum ApiRequest
    {
        GET,
        POST,
        PUT,
        DELETE

    }
    public class RestClient
    {
        public string endpoint { get; set; }
        public ApiRequest httpMethod { get; set; }
        public string postJSON { get; set; } 

        public RestClient()
        {
            endpoint = string.Empty;
            httpMethod = ApiRequest.GET;
        }

        public string makeRequest()
        {
            string responseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = httpMethod.ToString();

            if(request.Method == "POST" && postJSON != string.Empty)
            {
                request.ContentType = "application/json";
                using (StreamWriter swJsonPayload = new StreamWriter(request.GetRequestStream()))
                {
                    swJsonPayload.Write(postJSON);

                    swJsonPayload.Close();
                }
            }

            if (request.Method == "PUT")
            {
                Console.WriteLine("HERE WE GO WITH PUT");
                
            }

            HttpWebResponse response = null;
            try{
                response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                    }
                }

            }catch(Exception ex)
            {
                 responseValue = "{\" errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            Console.WriteLine("$$$ sResponseValue= { "+ responseValue + " }");
            return responseValue;
        }
    }
}
