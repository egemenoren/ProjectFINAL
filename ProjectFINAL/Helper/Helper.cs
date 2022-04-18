using System.Web;

namespace ProjectFINAL.Helper
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
