using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManage.Repository.Models
{
    public class Contact
    {
        public int Id { get; set; }   // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
