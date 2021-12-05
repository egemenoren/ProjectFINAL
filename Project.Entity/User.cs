using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entity;

namespace Project.Entity
{
    public class User : BaseEntity
    {
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastLoginIP { get; set; }
        public string Password { get; set; } = "123";
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; } = "Undefined";
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsFirstLogin { get; set; } = true;
        
    }
}
