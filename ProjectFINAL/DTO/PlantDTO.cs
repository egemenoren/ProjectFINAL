using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFINAL.DTO
{
    public class PlantDTO
    {
        public Plant Plant { get; set; }
        public decimal CurrentHumidityRate { get; set; }
        public DateTime? LastWateredTime { get; set; }
        public WateringType WateringType { get; set; }
    }
}