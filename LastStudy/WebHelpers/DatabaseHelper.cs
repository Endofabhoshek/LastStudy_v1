using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LastStudy.WebHelpers
{
    public static class DatabaseHelper
    {
        public static string GenerateConnectionString(string databaseName)
        {
            return "server=" + ConfigurationManager.AppSettings["server"] + ";database=" + databaseName 
                + ";uid=" + ConfigurationManager.AppSettings["username"] + ";password=" + ConfigurationManager.AppSettings["password"] + "";

            //return "server=" + server + ";database=" + databaseName + ";uid=" + username + ";password=" + password + "";
        }
    }
}