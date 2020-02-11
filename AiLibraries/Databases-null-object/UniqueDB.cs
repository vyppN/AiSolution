using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Xml;
using AiLibraries.Cryptographies;

namespace AiLibraries.Databases
{
    public sealed class UniqueDB
    {
        #region Singleton

// ReSharper disable InconsistentNaming
        private static readonly Lazy<UniqueDB> Lazy = new Lazy<UniqueDB>(()=>new UniqueDB());
// ReSharper restore InconsistentNaming

        
        static UniqueDB()
        {
            
        }

        public static UniqueDB Instance
        {
            get { return Lazy.Value; }
        }

        #endregion

        private MySqlConnection _connection;
        private string _connectionString;
        private string _database;
        private string _password;
        private string _port;
        private string _server, _user;
        private MySqlTransaction _transaction;

        public string DatabaseName { get { return _database; } }

        private UniqueDB()
        {
                
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        public MySqlTransaction Transaction()
        {
            return _transaction;
        }

        public void SetTransaction(IsolationLevel isolation)
        {
            _transaction = _connection.BeginTransaction(isolation);
        }

        private void SetConnectionString(bool utf8)
        {
            string utf = "";
            if (utf8) utf = "charset = utf8";
            if (_port == null)
            {
                _connectionString = String.Format("SERVER={0};user={1};PASSWORD={2};DATABASE={3};Allow Zero Datetime=true;{4}", _server, _user, _password, _database,utf);
            }
            else
            {
                _connectionString = String.Format("SERVER={0};port={1};user={2};PASSWORD={3};DATABASE={4};Allow Zero Datetime=true;{5}", _server, _port,_user, _password, _database, utf);
            }
        }

        public void SetOldPassword(bool use)
        {
            if (!use)
                _connectionString += "SET old_passwords=FALSE;";
            else
                _connectionString = _connectionString.Replace("SET old_passwords=FALSE;", "");
        }

        public UniqueDB SetConnection(string server, string database, string user, string password, string port = null,
                                      bool utf8 = true)
        {
            _server = server;
            _database = database;
            _user = user;
            _password = password;
            _port = port;

            SetConnectionString(utf8);
            var conn = new MySqlConnection(_connectionString);
            _connection = conn;
            return this;
        }

        public UniqueDB SetConnectionFromConfig(string port = null, bool utf8 = true)
        {
            _server = ConfigurationManager.AppSettings["host"];
            _database = ConfigurationManager.AppSettings["database"];
            _user = ConfigurationManager.AppSettings["username"];
            _password = ConfigurationManager.AppSettings["password"]; ;
            _port = port;

            SetConnectionString(utf8);
            var conn = new MySqlConnection(_connectionString);
            _connection = conn;
            return this;
        }

        public UniqueDB SetConnectionFromCustomConfig(string path,string key,string port = null, bool utf8 = true)
        {
            /* FORMAT ----------------------------
             * 
             * <?xml version="1.0" encoding="utf-8"?>
             *   <configuration>
             *     <appSettings>
             *       <app key="host" value="HOST" />
             *       <app key="database" value="DBNAME" />
             *       <app key="username" value="USER" />
             *       <app key="password" value="PASS" /> -- With AES Encryption
             *     </appSettings>
             *   </configuration>
             * -----------------------------------
             */

            XmlDocument xmlDoc = new XmlDocument();
             xmlDoc.Load(path);
             XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
             foreach (XmlNode childNode in appSettingsNode)
             {
                 if (childNode.Attributes["key"].Value == "host")
                     _server = childNode.Attributes["value"].Value;
                 if (childNode.Attributes["key"].Value == "database")
                     _database = childNode.Attributes["value"].Value;
                 if (childNode.Attributes["key"].Value == "username")
                     _user = childNode.Attributes["value"].Value;
                 if (childNode.Attributes["key"].Value == "password")
                     _password = Crypto.DecryptStringAES(childNode.Attributes["value"].Value,key);
             }
            _port = port;

            SetConnectionString(utf8);
            var conn = new MySqlConnection(_connectionString);
            _connection = conn;
            return this;
        }

        public DateTime GetDateFromDatabase()
        {
            string sql = "Select curdate() as date";
            var command = new MySqlCommand(sql, GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            DateTime prime = new DateTime();
            while (reader.Read())
            {
                prime = reader.GetDateTime("date");
            }
            reader.Close();
            return prime;
        }

        public string GetTimeFromDatabase()
        {
            string sql = "Select curtime() as time";
            var command = new MySqlCommand(sql, GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            var prime ="";
            while (reader.Read())
            {
                prime = reader.GetString("time");
            }
            reader.Close();
            return prime;
        }
    }
}