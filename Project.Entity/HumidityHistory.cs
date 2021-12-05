using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
    public class HumidityHistory:BaseEntity
    {
        public int PlantId { get; set; }
        public double HumidityRate { get; set; }
        public double Temperature { get; set; }
    }
}
