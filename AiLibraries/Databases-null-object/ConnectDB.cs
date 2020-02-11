using System;
using System.Xml;
using AiLibraries.Cryptographies;
using MongoDB.Driver;
using MySql.Data.MySqlClient;

namespace AiLibraries.Databases
{
    public class ConnectDB : MySqlConnecter
    {
        [Obsolete]
        protected override void SetConnectionString(bool encrypt = false)
        {
            var xDoc = new XmlDocument();
            xDoc.Load(@"config/config.xml");
            XmlNodeList nList = xDoc.GetElementsByTagName("config");
            foreach (XmlNode n in nList)
            {
                foreach (XmlNode childNode in n.ChildNodes)
                {
                    if (encrypt)
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
                    else
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
            }
            ConnectionString = "SERVER=" + Server + ";"
                               + "UID=" + User + ";" + "PASSWORD=" + Password + ";"
                               + "DATABASE=" + Database + ";Allow Zero Datetime=true;charset=utf8;";
        }

        protected override void SetConnectionString(string server, string database, string user, string password,
                                                    string port = null)
        {
            if (port == null)
            {
                ConnectionString = "SERVER=" + server + ";"
                                   + "user=" + user + ";" + "PASSWORD=" + password + ";"
                                   + "DATABASE=" + database + ";Allow Zero Datetime=true;charset=utf8;";
            }
            else
            {
                ConnectionString = "SERVER=" + server + ";"
                                   + "port=" + port + ";"
                                   + "user=" + user + ";" + "PASSWORD=" + password + ";"
                                   + "DATABASE=" + database + ";Allow Zero Datetime=true;charset=utf8;";
            }
        }

        [Obsolete]
        protected override void CreateConnection(bool encrypt = false)
        {
            InnoDB = false;
            IsolationLevel = "REPEATABLE READ";
            Snapshot = true;
            SetConnectionString(encrypt);
            var conn = new MySqlConnection(ConnectionString);
            Connection = conn;
        }

        protected override void CreateConnection(string server, string database, string user, string password,
                                                 string port = null, bool inno = false)
        {
            InnoDB = inno;
            IsolationLevel = "REPEATABLE READ";
            Snapshot = true;
            if (port != null)
                SetConnectionString(server, database, user, password, port);
            else
                SetConnectionString(server, database, user, password);
            var conn = new MySqlConnection(ConnectionString);
            Connection = conn;
        }

        /// <summary>
        /// ต่อ Database ตาม config.xml ไฟล์
        /// </summary>
        /// <returns>การเชื่อมต่อ</returns>
        [Obsolete]
        public static MySqlConnection OpenConnect(bool encrypted = false)
        {
            var con = new ConnectDB();
            con.CreateConnection(encrypted);
            MySqlConnection conn = Connection;
            return conn;
        }

        /// <summary>
        /// ต่อ Database แบบกำหนดค่าเอง
        /// </summary>
        /// <param name="server">ชื่อเครื่อง/IP เครื่อง Server</param>
        /// <param name="database"> ชื่อฐานข้อมูล</param>
        /// <param name="username">ชื่อผู้ใช้งานฐานข้อมูล</param>
        /// <param name="password">รหัสผ่าน</param>
        /// <param name="port">port เชื่อมต่อ</param>
        /// <param name="inno"> </param>
        /// <returns>การเชื่อมต่อ</returns>
        public static MySqlConnection OpenConnect(string server, string database, string username, string password,
                                                  string port = null, bool inno = false)
        {
            var con = new ConnectDB();
            con.CreateConnection(server, database, username, password, port, inno);
            MySqlConnection conn = Connection;
            return conn;
        }

        /// <summary>
        /// ต่อ MongoDB
        /// </summary>
        /// <param name="host">ชื่อเครื่อง/IP เครื่อง Server</param>
        /// <param name="database">ชื่อฐานข้อมูล</param>
        /// <param name="username">ชื่อผู้ใช้งานฐานข้อมูล</param>
        /// <param name="password">รหัสผ่าน</param>
        /// <returns></returns>
        public static MongoDatabase NoSqlConnect(string host, string database, string username, string password)
        {
            var cl = new MongoClient(new MongoUrl("mongodb://" + username + ":" + password + "@" + host));
            MongoServer server = cl.GetServer();
            MongoDatabase db = server.GetDatabase(database);
            return db;
        }
    }
}