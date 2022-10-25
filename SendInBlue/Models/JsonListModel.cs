using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    internal class JsonListModel

        { 
            public List<list> lists { get; set; }
            public int count { get; set; }
         }

        public class list
        {
            public int id { get; set; }
            public string name { get; set; }
            public int uniqueSubscribers { get; set; }
            public int totalBlacklisted { get; set; }
            public int totalSubscribers { get; set; }
        }
}
