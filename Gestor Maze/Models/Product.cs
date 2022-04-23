using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gestor_Maze.Models
{
    class Data
    {   
        public int id { get; set; }
        public string product_name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }

    }
    class Product
    {   public string msg { get; set; }
        public int code { get; set; }
        public List<Data> data { get; set; }


        public static Product JsonDesserialize(string json)
        {
            return JsonConvert.DeserializeObject<Product>(json);
        }
        public static string JsonSerialize(Data obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DefaultValueHandling = DefaultValueHandling.Ignore;

            var json = JsonConvert.SerializeObject(obj,settings);

            return json;
        }
    }
}
