using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
    public class Plant:BaseEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
