using MySql.Data.MySqlClient;

namespace AiLibraries.Databases
{
    /// <summary>
    /// สร้าง Connection กับ MySQL
    /// !!! ห้ามแก้ไข !!!
    /// </summary>
    public abstract class MySqlConnecter
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
        protected abstract void CreateConnection(bool encrypt = false);

        protected abstract void CreateConnection(string server, string database, string user, string password,
                                                 string port = null, bool inno = false);

        protected abstract void SetConnectionString(bool encrypt = false);

        protected abstract void SetConnectionString(string server, string database, string user, string password,
                                                    string port = null);

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
                    myCommand.CommandText = Snapshot
                                                ? "START TRANSACTION WITH CONSISTENT SNAPSHOT;"
                                                : "START TRANSACTION";
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