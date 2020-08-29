using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LastStudy.WebHelpers
{
    public static class DatabaseHelper
    {
        private static string Server = "";
        private static string UserId = "";
        private static string Password = "";
        private static string DatabaseName = "";

        public static Dictionary<string, string> GenerateConnectionString(string databaseName)
        {
            Server = ConfigurationManager.AppSettings["server"];
            UserId = ConfigurationManager.AppSettings["username"];
            Password = ConfigurationManager.AppSettings["password"];
            DatabaseName = databaseName;

            Dictionary<string, string> connectionStringKey = new Dictionary<string, string>();
            connectionStringKey.Add("server", Server);
            connectionStringKey.Add("database", DatabaseName);
            connectionStringKey.Add("uid", UserId);
            connectionStringKey.Add("password", Password);

            return connectionStringKey;
        }

        // need to change this later
        public static List<string> States()
        {
            return new List<string>() { "Andaman and Nicobar Islands", "Andhra Pradesh", "Andhra Pradesh (New)", "Arunachal Pradesh", "Assam", "Bihar", "Chandigarh"
                , "Chattisgarh", "Dadra and Nagar Haveli", "Daman and Diu", "Delhi", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jammu and Kashmir", "Jharkhand"
                , "Karnataka", "Kerala", "Lakshadweep Islands", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Pondicherry"
                , "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand", "West Bengal" };
        }
    }
}