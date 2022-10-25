using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class CreatContactModel:ApiKey

    {
        public string? FristName { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? Phonenumber { get; set; }
        public string? Campaign { get; set; }
        public int FolderId { get; set; }
        public int listId { get; set; }
    }
}
