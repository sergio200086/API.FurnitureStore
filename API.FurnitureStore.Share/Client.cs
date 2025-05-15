using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Share
{
    public class Client
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
    }
}
