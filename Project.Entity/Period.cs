using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
    public enum Period
    {
        [Description("Günlük")]
        Daily=1,
        [Description("Haftalık")]
        Weekly = 2
    }
}
