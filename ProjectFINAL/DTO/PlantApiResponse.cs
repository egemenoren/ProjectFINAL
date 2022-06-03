using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFINAL.DTO
{
    public class PlantApiResponse
    {
        public int PlantId { get; set; }
        public object HumidityCardAddress { get; set; }
        public object PumpCardAddress { get; set; }
    }
}