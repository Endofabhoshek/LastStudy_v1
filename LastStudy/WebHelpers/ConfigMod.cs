using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data.SqlClient;
using System.Web;
using LastStudy.Core.Helpers;

namespace LastStudy.WebHelpers
{
    public static class ConfigMod // might use when signing up the institute
    {
        private static void AddConnectionString(string name) // can update from here too
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Web.Config");
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
            XmlNode node;

            node = doc.CreateNode(XmlNodeType.Element, "add", null); //if already exists. check
            
            XmlAttribute attribute = doc.CreateAttribute("name");
            attribute.Value = name;
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("connectionString");
            attribute.Value = DatabaseHelper.GenerateConnectionString(); //reference to external helper -- need to think to resolve this
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("providerName");
            attribute.Value = "MySql.Data.MySqlClient";
            node.Attributes.Append(attribute);
            doc.Save(path);
        }
    }
}