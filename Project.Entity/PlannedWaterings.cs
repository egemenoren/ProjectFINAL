﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
    public class PlannedWaterings : BasePlantDataEntity
    {
        public int? LimitHumidityRate { get; set; }
        public DateTime? WateringHour { get; set; }
        public Period? Period { get; set; }
        public WateringType? WateringType { get; set; } = Entity.WateringType.Not_Set;
        public short? WateringSecond { get; set; }
    }
}
