using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LastStudy.Core.Helpers
{
    public static class DatabaseHelper
    {
        public static string GenerateConnectionString(string server, string username, string password, string databaseName)
        {
            //return "server="+ ConfigurationManager.AppSettings["sqlServer"] +";database="+ ConfigurationManager.AppSettings["databaseName"] 
            //    + ";uid="+ ConfigurationManager.AppSettings["userid"] + ";password="+ ConfigurationManager.AppSettings["password"] + "";

            return "server=" + server + ";database=" + databaseName + ";uid=" + username + ";password=" + password + "";
        }
    }
}
