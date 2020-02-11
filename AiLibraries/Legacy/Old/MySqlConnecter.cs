using System;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;

namespace AiLibraries.Legacy.Old
{
    /// <summary>
    /// เอาไว้ต่อกับ Database ไม่ได้ใช้หรอก ไปเรียกใช้คลาส ConnectDB แทนเถอะ
    /// </summary>
    public class MySqlConnecter
    {
        //===properties===
        //set isolation level
        protected string IsolationLevel = "REPEATABLE READ"; //<---can config default

        protected string connectionString;
        protected string database;
        protected string password;
        protected string server;
        protected bool snapshot = true; //<---can config default
        protected string user;
        protected MySqlConnection Connection { get; set; }
        //set inno
        protected bool innoDB { get; set; }

        public String setServerName
        {
            get { return server; }
            set { server = value; }
        }

        public String setUserName
        {
            get { return user; }
            set { user = value; }
        }

        public String setPassWord
        {
            get { return password; }
            set { password = value; }
        }

        public String setDatabaseName
        {
            get { return database; }
            set { database = value; }
        }

        protected bool CONSISTENT_SNAPSHOT
        {
            get { return snapshot; }
            set { snapshot = value; }
        }

        public void ReadUncommitted()
        {
            IsolationLevel = "READ UNCOMMITTED";
        }

        public void ReadCommitted()
        {
            IsolationLevel = "READ COMMITTED";
        }

        public void RepeatableRead()
        {
            IsolationLevel = "REPEATABLE READ";
        }

        public void Serializable()
        {
            IsolationLevel = "SERIALIZABLE";
        }

        public MySqlConnection getConnection()
        {
            return Connection;
        }

        public void setConnectionString1()
        {
            if (server == null) server = "localhost";
            if (user == null) user = "root";
            if (password == null) password = "";
            if (database == null) database = "";
            connectionString = "SERVER=" + server + ";"
                               + "UID=" + user + ";" + "PASSWORD=" + password + ";"
                               + "DATABASE=" + database + ";Allow Zero Datetime=true;";
        }

        public void setConnectionString()
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
                        server = childNode.InnerText;
                    }
                    if (childNode.Name == "database")
                    {
                        database = childNode.InnerText;
                    }
                    if (childNode.Name == "user")
                    {
                        user = childNode.InnerText;
                    }
                    if (childNode.Name == "pass")
                    {
                        password = childNode.InnerText;
                    }
                }
            }
            connectionString = "SERVER=" + server + ";"
                               + "UID=" + user + ";" + "PASSWORD=" + password + ";"
                               + "DATABASE=" + database + ";Allow Zero Datetime=true;charset=utf8;";
        }

        public void setConnectionString(string server, string database, string user, string password)
        {
            connectionString = "SERVER=" + server + ";"
                               + "UID=" + user + ";" + "PASSWORD=" + password + ";"
                               + "DATABASE=" + database + ";Allow Zero Datetime=false;";
        }

        //set consistent snapshot

        //===method===
        public void CreateConnection()
        {
            //***config***
            innoDB = false; //<--set no innoDB
            IsolationLevel = "REPEATABLE READ"; //<--set Isolation Level
            snapshot = true; // set snapshot
            //************
            setConnectionString();
            var conn = new MySqlConnection(connectionString);
            Connection = conn;
        }

        public void CreateConnection1(string server, string database, string user, string password)
        {
            //***config***
            innoDB = false; //<--set no innoDB
            IsolationLevel = "REPEATABLE READ"; //<--set Isolation Level
            snapshot = true; // set snapshot
            //************
            setConnectionString(server, database, user, password);
            var conn = new MySqlConnection(connectionString);
            Connection = conn;
        }

        public void OpenConnection()
        {
            Connection.Open();
            Connection.CreateCommand();
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public void TransactionBegin()
        {
            if (innoDB)
            {
                //_transaction = _connection.BeginTransaction()
                MySqlCommand myCommand = Connection.CreateCommand();
                myCommand.CommandText = "SET SESSION TRANSACTION ISOLATION LEVEL " + IsolationLevel;
                myCommand.ExecuteNonQuery();

                if (IsolationLevel == "READ UNCOMMITTED")
                {
                    myCommand.CommandText = "SET autocommit=1;";
                    myCommand.ExecuteNonQuery();
                }
                else
                {
                    myCommand.CommandText = "SET autocommit=0;";
                    myCommand.ExecuteNonQuery();
                }

                //myCommand.CommandText = "START TRANSACTION"
                if (IsolationLevel == "REPEATABLE READ" || IsolationLevel == "SERIALIZABLE")
                {
                    if (snapshot)
                    {
                        myCommand.CommandText = "START TRANSACTION WITH CONSISTENT SNAPSHOT;";
                    }
                    else
                    {
                        myCommand.CommandText = "START TRANSACTION";
                    }
                }
                else
                {
                    myCommand.CommandText = "START TRANSACTION";
                }

                myCommand.ExecuteNonQuery();
            }
        }

        public void TransactionCommit()
        {
            if (innoDB)
            {
                MySqlCommand myCommand = Connection.CreateCommand();
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
        }

        public void TransactionRollback()
        {
            if (innoDB)
            {
                MySqlCommand myCommand = Connection.CreateCommand();
                myCommand.CommandText = "ROLLBACK";
                myCommand.ExecuteNonQuery();
            }
        }

        //-------------------- Select ตาราง -------------------------------------//


        public static DataSet mysqlexec(MySqlConnection connection_OdbcConnection, string sql, string table)
        {
            var dataSet = new DataSet();
            MySqlConnection connection = connection_OdbcConnection;
            var command = new MySqlCommand();
            var adapter = new MySqlDataAdapter();
            command.CommandText = sql;
            command.Connection = connection;
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, table);
            return dataSet;
        }
    }
}