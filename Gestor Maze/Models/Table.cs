using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Models
{

    class TableData
    {
        public int id { get; set; }
        public int state_id { get; set; }
        public string state { get; set; }
        public string table_name { get; set; }
        public int lot { get; set; }
        public string created { get; set; }
        public string updated { get; set; }

    }
    class TableModel
    {
        public string msg { get; set; }
        public int code { get; set; }
        public List<TableData> data { get; set; }


        public static TableModel JsonDesserialize(string json)
        {
            return JsonConvert.DeserializeObject<TableModel>(json);
        }
        public static string JsonSerialize(TableData obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return json;
        }
    }
}
