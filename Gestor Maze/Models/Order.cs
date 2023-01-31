using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Models
{
    class OrderData
    {
        public int id { get; set; }
        public string name { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int table_id { get; set; }
        public string table_name { get; set; }
        public int state_id { get; set; }
        public string state { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double subtotal { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }
  
    class Order
    {
        public string msg { get; set; }
        public int code { get; set; }
        public List<OrderData> data { get; set; }


        public static Order JsonDesserialize(string json)
        {
            return JsonConvert.DeserializeObject<Order>(json);
        }
        public static string JsonSerialize(OrderData obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DefaultValueHandling = DefaultValueHandling.Ignore;

            var json = JsonConvert.SerializeObject(obj, settings);

            return json;
        }
    }

    class Report
    {
        public string msg { get; set; }
        public int code { get; set; }
        public List<Rel> data { get; set; }

        public static Report JsonDesserialize(string json)
        {
            return JsonConvert.DeserializeObject<Report>(json);
        }

        public static string JsonSerialize(Rel obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.DefaultValueHandling = DefaultValueHandling.Ignore;

            var json = JsonConvert.SerializeObject(obj,settings);

            return json;
        }
    }

    class Rel
    {
        public string product_name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double subtotal { get; set; }
        public string begin { get; set; }
        public string end { get; set; }
    }
}
