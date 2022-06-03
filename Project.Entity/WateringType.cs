using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
    public enum WateringType
    {
        [Description("Nem'e Göre")]
        Humidity = 1,
        [Description("Zaman Ayarlı")]
        Timer = 2,
        [Description("Belirlenmedi")]
        Not_Set = 3
    }
}
