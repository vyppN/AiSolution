using System.Xml;

namespace AiLibraries.Legacy
{
    public class ConnectDBNoUTF8 : ConnectDB
    {
        protected override void SetConnectionString()
        {
            var xDoc = new XmlDocument();
            xDoc.Load(@"config/config.xml");
            XmlNodeList nList = xDoc.GetElementsByTagName("config");
            foreach (XmlNode n in nList)
            {
                foreach (XmlNode childNode in n.ChildNodes)
                {
                    if (childNode.Name == "server")
                    {
                        Server = childNode.InnerText;
                    }
                    if (childNode.Name == "database")
                    {
                        Database = childNode.InnerText;
                    }
                    if (childNode.Name == "user")
                    {
                        User = childNode.InnerText;
                    }
                    if (childNode.Name == "pass")
                    {
                        Password = childNode.InnerText;
                    }
                }
            }
            ConnectionString = "SERVER=" + Server + ";"
                               + "UID=" + User + ";" + "PASSWORD=" + Password + ";"
                               + "DATABASE=" + Database + ";Allow Zero Datetime=true;";
        }

        protected new void setConnectionStringEncrypted()
        {
            var xDoc = new XmlDocument();
            xDoc.Load(@"config/config.xml");
            XmlNodeList nList = xDoc.GetElementsByTagName("config");
            foreach (XmlNode n in nList)
            {
                foreach (XmlNode childNode in n.ChildNodes)
                {
                    if (childNode.Name == "server")
                    {
                        Server = Crypto.DecryptStringAES(childNode.InnerText, "aikung");
                    }
                    if (childNode.Name == "database")
                    {
                        Database = Crypto.DecryptStringAES(childNode.InnerText, "aikung");
                    }
                    if (childNode.Name == "user")
                    {
                        User = Crypto.DecryptStringAES(childNode.InnerText, "aikung");
                    }
                    if (childNode.Name == "pass")
                    {
                        Password = Crypto.DecryptStringAES(childNode.InnerText, "aikung");
                    }
                }
            }
            ConnectionString = "SERVER=" + Server + ";"
                               + "UID=" + User + ";" + "PASSWORD=" + Password + ";"
                               + "DATABASE=" + Database + ";Allow Zero Datetime=true;";
        }

        protected override void SetConnectionString(string server, string database, string user, string password)
        {
            ConnectionString = "SERVER=" + server + ";"
                               + "UID=" + user + ";" + "PASSWORD=" + password + ";"
                               + "DATABASE=" + database + ";Allow Zero Datetime=false;";
        }
    }
}