using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gestor_Maze.Models
{
    class Data
    {   
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public string created { get; set; }
        public string updated { get; set; }

        public Data (string name, double price, int quantity)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }
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
          var json= JsonConvert.SerializeObject(obj);

            return json;
        }
    }
}
