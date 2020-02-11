using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using AiLibraries.Attributes;
using AiLibraries.Helper;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace AiLibraries.Databases
{
    /// <summary>
    /// จัดการกับข้อมูลใน Database โดยอัตโนมัติ
    /// อย่าแก้ไขไฟล์นี้ถ้าไม่อยากเป็นไมเกรน
    /// </summary>
    public class Query
    {
        private readonly List<string> _binding = new List<string>();
        private readonly List<string> _keys = new List<string>();
        private string _tableName;
        private readonly List<string> _where = new List<string>();
        private string _limit = null;

        public Query(string tableName)
        {
            SetTableName(tableName);
        }

        public Query()
        {

        }

        public void SetTableName(string tableName){
            _tableName = tableName;
        }

        public Query Where(string key, string oper, string value)
        {
            _where.Add(String.Format("AND {0} {1} @{0}", key, oper));
            _binding.Add(value);
            _keys.Add(key);
            return this;
        }

        public void ClearWhereCluase()
        {
            _where.Clear();
            _binding.Clear();
            _keys.Clear();
        }

        public void Limit(string limit)
        {
            _limit = limit;
        }

        public void Limit(int limit)
        {
            _limit = limit.ToString(CultureInfo.InvariantCulture);
        }

        public bool CheckIgnore(PropertyInfo prop)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            bool ignore = false;
            attrs.ToList().ForEach(x =>
            {
                if (x.GetType().Name == "ORMIgnore")
                {
                    var xx = (ORMIgnore)x;
                    ignore = xx.Ignore;
                }
            });
            return ignore;
        }

        public bool CheckStaticName(PropertyInfo prop)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            bool staticName = false;
            attrs.ToList().ForEach(x =>
            {
                if (x.GetType().Name == "StaticName")
                {
                    var xx = (StaticName)x;
                    staticName = xx.Static;
                }
            });
            return staticName;
        }

        public dynamic Get(Type classType, string column = "*", string order = "")
        {
            var trans = UniqueDB.Instance.GetConnection().BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {

                string sql = "SELECT " + column + " FROM " + _tableName + GetWhere() + " " + order + " " + GetLimit();
                var command = new MySqlCommand(sql, UniqueDB.Instance.GetConnection(), trans);

                for (int i = 0; i < _binding.Count; i++)
                {
                    command.Parameters.AddWithValue(_keys[i], _binding[i]);
                }
                MySqlDataReader reader = command.ExecuteReader();

                Type listGeneric = typeof (List<>);
                Type[] param = {classType};
                object dbObjects = Activator.CreateInstance(listGeneric.MakeGenericType(param));
                MethodInfo add = dbObjects.GetType().GetMethod("Add");


                while (reader.Read())
                {
                    object obj = Activator.CreateInstance(classType);
                    PropertyInfo[] props = obj.GetType().GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        
                        if (CheckIgnore(prop))
                            continue;

                        var propName = "";
                        if (CheckStaticName(prop))
                            propName = prop.Name;
                        else
                            propName = prop.Name.ToLower();


                        if (HasColumn(reader, propName))
                        {
                            if (reader[propName] != DBNull.Value)
                                prop.SetValue(obj, reader[propName], null);
                        }
                    }

                    add.Invoke(dbObjects, new[] {obj});
                }

                reader.Close();
                trans.Commit();
                ClearWhereCluase();
                return dbObjects;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "ข้อผิดพลาด");
                return null;
            }
        }

        
        public bool HasColumn(IDataRecord r, string columnName)
        {
            try
            {
                return r.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public void Delete()
        {
            string sql = "DELETE FROM " + _tableName + GetWhere();
            var command = new MySqlCommand(sql, UniqueDB.Instance.GetConnection());
            for (int i = 0; i < _binding.Count; i++)
            {
                command.Parameters.AddWithValue(_keys[i], _binding[i]);
            }
            command.ExecuteNonQuery();
            ClearWhereCluase();
        }

        public dynamic Frist(Type classType, string column = "*")
        {
            dynamic item = Enumerable.FirstOrDefault(Get(classType, column));
            if (item == null) item = Activator.CreateInstance(classType);
            return item;
        }

        public void Save(object data)
        {
            string sql;
            if (IsDuplicate(data))
            {
                sql = GetUpdate(data);
            }
            else
            {
                sql = GetInsert(data);
            }

            

            var con = UniqueDB.Instance.GetConnection();
            var trans = UniqueDB.Instance.GetConnection().BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                var command = new MySqlCommand(sql, con, trans);
                PropertyInfo[] props = data.GetType().GetProperties();
                props.ToList().ForEach(
                    prop =>
                    {
                        if (!CheckIgnore(prop))
                        {

                            var value = data.GetType().GetProperty(prop.Name).GetValue(data, null);
                            var culture = new CultureInfo("en-US");
                            if (value != null && (value is MySqlDateTime || value is DateTime))
                                value = Convert.ToDateTime(value).ToString("yyyy-MM-dd",culture);

                            var propName = "";
                            if (CheckStaticName(prop))
                                propName = prop.Name;
                            else
                                propName = prop.Name.ToLower();

                            command.Parameters.AddWithValue(propName, value);
                        }
                    });
                command.ExecuteNonQuery();
                trans.Commit();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "ข้อผิดพลาด");
                trans.Rollback();
            }
            ClearWhereCluase();
        }

        public int InsertAndGetLastID(object data)
        {
            string sql = GetInsert(data); 
            var con = UniqueDB.Instance.GetConnection();
            var trans = UniqueDB.Instance.GetConnection().BeginTransaction(IsolationLevel.RepeatableRead);
            var id = 0;
            try
            {
                sql += "SELECT LAST_INSERT_ID();";
                var command = new MySqlCommand(sql, con, trans);
                PropertyInfo[] props = data.GetType().GetProperties();
                props.ToList().ForEach(
                    prop =>
                    {
                        if (!CheckIgnore(prop))
                        {
                            var value = data.GetType().GetProperty(prop.Name).GetValue(data, null);
                            if (value != null && (value is MySqlDateTime || value is DateTime))
                                value = Convert.ToDateTime(value).ToString("yyyy-MM-dd");

                            var propName = "";
                            if (CheckStaticName(prop))
                                propName = prop.Name;
                            else
                                propName = prop.Name.ToLower();
                            command.Parameters.AddWithValue(propName, value);
                        }
                    });
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                reader.Close();
                trans.Commit();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "ข้อผิดพลาด");
                trans.Rollback();
            }
            ClearWhereCluase();
            return id;
        }


        private string GetInsert(object data)
        {
            PropertyInfo[] props = data.GetType().GetProperties();
            var propNames = new List<string>();
            
            foreach (var prop in props)
            { 

                if (CheckIgnore(prop))
                    continue;
                propNames.Add(prop.Name);
            }

            return "INSERT INTO " + _tableName + " (" + String.Join(",", propNames) + ") " + " VALUES (@" +
                   String.Join(",@", propNames) + ");";
        }

        private string GetUpdate(object data)
        {
            PropertyInfo[] props = data.GetType().GetProperties();
            var propNames = new string[props.Length];
            int i = 0;
            foreach (var prop in props)
            {
                if (CheckIgnore(prop))
                    continue;
                propNames[i] = prop.Name;
                i++;
            }

            string update = "";
            propNames.ToList().ForEach(key => { if (key != null) update += key + "=@" + key + ","; });
            string prime = GetPrimaryKey(data);
            StringHelper.CutStringRight(ref update, ",");
            return "UPDATE " + _tableName + " SET " + update + " WHERE " + prime + "= @" + prime+";";
        }

        private string GetWhere()
        {
            if (_where.Count == 0)
            {
                return "";
            }
            
            string where = String.Join(" ", _where);
            string whereSql = StringHelper.GetRightString(where, "AND");
            return " WHERE " + StringHelper.GetRightString(whereSql, "OR");
        }

        private string GetLimit()
        {
            if (_limit == null)
            {
                return "";
            }
            return " LIMIT " + _limit;
        }

        private bool IsDuplicate(object data)
        {
            string prime = GetPrimaryKey(data);
            foreach (PropertyInfo info in data.GetType().GetProperties())
            {

                if (prime.ToLower() == info.Name.ToLower())
                    prime = info.Name;
            }
            if (data.GetType().GetProperty(prime).GetValue(data, null) == null) 
                return false;
            Where(prime, "=", data.GetType().GetProperty(prime).GetValue(data, null).ToString());
            var list = Get(data.GetType()) as IList;
            return list.Count > 0;
        }

        public string GetPrimaryKey(object data)
        {
            string sql = "DESCRIBE " + _tableName;
            var command = new MySqlCommand(sql, UniqueDB.Instance.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            string prime = "";
            while (reader.Read())
            {
                if (reader["Key"].Equals("PRI"))
                    prime = reader["Field"].ToString();
            }
            reader.Close();
            return prime;
        }

        public int Count(Type classType, string column = "*", string order = "")
        {
            string sql = "SELECT count(" + column + ") as count FROM " + _tableName + GetWhere() + " ";
            var command = new MySqlCommand(sql, UniqueDB.Instance.GetConnection());
            for (int i = 0; i < _binding.Count; i++)
            {
                command.Parameters.AddWithValue(_keys[i], _binding[i]);
            }
            MySqlDataReader reader = command.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32("count");
            }
            reader.Close();
            return count;
        }

        public int Max(Type classType, string column = "*", string order = "")
        {
            string sql = "SELECT Max(" + column + ") as max FROM " + _tableName + GetWhere() + " ";
            var command = new MySqlCommand(sql, UniqueDB.Instance.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32("max");
            }
            reader.Close();
            return count;
        }

        [Obsolete]
        private List<ReferenceTable> GetReferenceTables()
        {
            var referenceTableList = new List<ReferenceTable>();
            string sql = "SELECT " +
                         "TABLE_NAME,COLUMN_NAME, REFERENCED_TABLE_NAME,REFERENCED_COLUMN_NAME " +
                         "from INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                         "where REFERENCED_TABLE_NAME = '" + _tableName + "';";
            var command = new MySqlCommand(sql, UniqueDB.Instance.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var referenceTable = new ReferenceTable();
                referenceTable.ColumnName = reader.GetString("COLUMN_NAME");
                referenceTable.TableName = reader.GetString("TABLE_NAME");
                referenceTable.ReferencedColumnName = reader.GetString("REFERENCED_COLUMN_NAME");
                referenceTable.ReferencedTable = reader.GetString("REFERENCED_TABLE_NAME");
                referenceTableList.Add(referenceTable);
            }
            reader.Close();
            return referenceTableList;
        }
    }
}