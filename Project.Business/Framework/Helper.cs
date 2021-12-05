using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Project.Business.Framework
{
    public class Helper
    {
        public string GetIPHelper()
        {
            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            return ip;
        }
    }
}
