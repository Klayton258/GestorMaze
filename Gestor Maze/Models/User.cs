using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_Maze.Models
{
    
        class UserData
        {
            public int id { get; set; }
            public int state_id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string pass { get; set; }
            public string age { get; set; }
            public string gener { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string document { get; set; }
            public string address { get; set; }
            public string bank_account { get; set; }
            public string salary { get; set; }
            public string picture { get; set; }
            public string permission { get; set; }

        }

        class User
        {
            public string msg { get; set; }
            public int code { get; set; }
            public List<UserData> data { get; set; }


            public static User JsonDesserialize(string json)
            {
                return JsonConvert.DeserializeObject<User>(json);
            }
            public static string JsonSerialize(UserData obj)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                var json = JsonConvert.SerializeObject(obj);

                return json;
            }
        }
    
}
