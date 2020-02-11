using System.Xml;
using MySql.Data.MySqlClient;

namespace AiLibraries.Legacy
{
    public class ConnectDB : MySqlConnector
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
                        Server = childNode.InnerText;
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
                               + "DATABASE=" + Database + ";Allow Zero Datetime=true;charset=utf8;";
        }

        protected void setConnectionStringEncrypted()
        {
            var xDoc = new XmlDocument();
            xDoc.Load(@"config/docwebconfig.xml");
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
                               + "DATABASE=" + Database + ";Allow Zero Datetime=true;charset=utf8;";
        }

        protected override void SetConnectionString(string server, string database, string user, string password)
        {
            ConnectionString = "SERVER=" + server + ";"
                               + "user=" + user + ";" + "PASSWORD=" + password + ";"
                               + "DATABASE=" + database + ";Allow Zero Datetime=true;charset=utf8;";
        }

        protected void setConnectionString(string server, string database, string user, string password, string port)
        {
            ConnectionString = "SERVER=" + server + ";"
                               + "port=" + port + ";"
                               + "user=" + user + ";" + "PASSWORD=" + password + ";"
                               + "DATABASE=" + database + ";Allow Zero Datetime=true;charset=utf8;";
        }

        protected override void CreateConnection()
        {
            InnoDB = false;
            IsolationLevel = "REPEATABLE READ";
            Snapshot = true;
            SetConnectionString();
            var conn = new MySqlConnection(ConnectionString);
            Connection = conn;
        }

        protected void CreateConnectionEncrypted()
        {
            InnoDB = false;
            IsolationLevel = "REPEATABLE READ";
            Snapshot = true;
            setConnectionStringEncrypted();
            var conn = new MySqlConnection(ConnectionString);
            Connection = conn;
        }

        protected override void CreateConnection(string server, string database, string user, string password)
        {
            InnoDB = false;
            IsolationLevel = "REPEATABLE READ";
            Snapshot = true;
            SetConnectionString(server, database, user, password);
            var conn = new MySqlConnection(ConnectionString);
            Connection = conn;
        }

        protected void CreateConnection(string server, string database, string user, string password, string port)
        {
            InnoDB = false;
            IsolationLevel = "REPEATABLE READ";
            Snapshot = true;
            setConnectionString(server, database, user, password, port);
            var conn = new MySqlConnection(ConnectionString);
            Connection = conn;
        }

        /// <summary>
        /// ต่อ Database ตาม config.xml ไฟล์
        /// </summary>
        /// <returns>การเชื่อมต่อ</returns>
        public static MySqlConnection openConnect()
        {
            var con = new ConnectDB();
            con.CreateConnection();
            MySqlConnection conn = Connection;
            return conn;
        }

        public static MySqlConnection openConnectEncrypted()
        {
            var con = new ConnectDB();
            con.CreateConnectionEncrypted();
            MySqlConnection conn = Connection;
            return conn;
        }

        /// <summary>
        /// ต่อ Database แบบกำหนดค่าเอง
        /// </summary>
        /// <param name="server">ชื่อเครื่อง/IP เครื่อง Server</param>
        /// <param name="database"> ชื่อฐานข้อมูล</param>
        /// <param name="user">ชื่อผู้ใช้งานฐานข้อมูล</param>
        /// <param name="password">รหัสผ่าน</param>
        /// <returns>การเชื่อมต่อ</returns>
        public static MySqlConnection openConnect(string server, string database, string user, string password)
        {
            var con = new ConnectDB();
            con.CreateConnection(server, database, user, password);
            MySqlConnection conn = Connection;
            return conn;
        }


        //public static MongoDatabase NoSQLConnect(string host,string DB, string username, string password)
        //{
        //    MongoClient cl = new MongoClient(new MongoUrl("mongodb://"+username+":"+password+"@"+host));
        //    MongoServer server = cl.GetServer();
        //    MongoDatabase db = server.GetDatabase(DB);
        //    return db;
        //}

        public static MySqlConnection openConnect(string server, string database, string user, string password,
                                                  string port)
        {
            var con = new ConnectDB();
            con.CreateConnection(server, database, user, password, port);
            MySqlConnection conn = Connection;
            return conn;
        }
    }
}