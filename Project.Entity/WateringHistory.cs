using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
    public class WateringHistory : BaseEntity
    {
        public int PlantId { get; set; }
        public int LastHumidityRate { get; set; }
    }
}
