using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUINavigationApp1.Models
{
    class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public int image_id { get; set; }
    }
}
