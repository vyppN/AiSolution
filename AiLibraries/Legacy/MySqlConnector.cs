using MySql.Data.MySqlClient;

namespace AiLibraries.Legacy
{
    public abstract class MySqlConnector
    {
        //-----Properties ----------------

        protected static MySqlConnection Connection;
        protected string ConnectionString;
        protected string Database;
        protected string IsolationLevel = "REPEATABLE READ";
        protected string Password;
        protected string Server;
        protected bool Snapshot = true; //<---can config default
        protected string User;
        protected bool InnoDB { get; set; }

        protected bool ConsistentSnapshot
        {
            get { return Snapshot; }
            set { Snapshot = value; }
        }


        //-----Abstract Methos -----------
        protected abstract void CreateConnection();
        protected abstract void CreateConnection(string server, string database, string user, string password);
        protected abstract void SetConnectionString();
        protected abstract void SetConnectionString(string server, string database, string user, string password);

        //-----Method---------------------
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


        public void TransactionBegin()
        {
            if (InnoDB)
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
                    if (Snapshot)
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
            if (InnoDB)
            {
                MySqlCommand myCommand = Connection.CreateCommand();
                myCommand.CommandText = "COMMIT";
                myCommand.ExecuteNonQuery();
            }
        }

        public void TransactionRollback()
        {
            if (InnoDB)
            {
                MySqlCommand myCommand = Connection.CreateCommand();
                myCommand.CommandText = "ROLLBACK";
                myCommand.ExecuteNonQuery();
            }
        }
    }
}