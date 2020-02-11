using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using AiLibraries.Helper;
using AiLibraries.LibExtendsion;
using MySql.Data.Types;

namespace AiLibraries.Databases
{
    public enum Operators
    {
        EQUAL,
        LIKE
    }

    public abstract class AiORM
    {
        protected readonly Query _query;
        private bool singularName;
        private bool isNull = false;

        protected AiORM()
        {
            _query = new Query();
            SetTableName(GetType().Name);
        }


        protected virtual void SetTableName(string TableName)
        {
            _query.SetTableName(HelpMe.GetTableName(TableName));
        }

        public AiORM Limit(int limit)
        {
            _query.Limit(limit);
            return this;
        }
        public AiORM Limit(string limit)
        {
            _query.Limit(limit);
            return this;
        }

        public AiORM Limit(int from, int to)
        {
            _query.Limit(from + "," + to);
            return this;
        }

        public void Where(Enum enums, string oper, string value)
        {
            _query.Where(GetKey(enums), oper, value);
        }

        public void Where(string column, string oper, string value)
        {
            _query.Where(column, oper, value);
        }

        public void ClearWhereCluase()
        {
            _query.ClearWhereCluase();
        }

        public int Count()
        {
           return _query.Count(GetType());
        }

        public int Max(string column)
        {
            return _query.Max(GetType(),column);
        }

        public IList Find(string order = "")
        {
            PropertyInfo[] props = GetType().GetProperties();
            props.ToList().ForEach(prop =>
            {
                if (GetType().GetProperty(prop.Name).GetValue(this, null) != null)
                {
                    string value = "";
                    if (GetType().GetProperty(prop.Name).GetValue(this, null) is DateTime || GetType().GetProperty(prop.Name).GetValue(this, null) is MySqlDateTime)
                        value = Convert.ToDateTime( GetType().GetProperty(prop.Name).GetValue(this, null)).SetToMySQLDateString();
                    else
                        value =
                            GetType().GetProperty(prop.Name).GetValue(this, null).
                                ToString();
                    _query.Where(prop.Name, "=", value);
                }
            });

            return _query.Get(GetType(),"*",order);
        }
        
        //public IList Find(string order = "")
        //{
        //    return Get("*", order);
        //}



        public IList Find(Enum enums, string value)
        {
            return _query.Where(GetKey(enums), "=", value).Get(GetType());
        }

        public IList Find(string column, string value)
        {
            return _query.Where(column, "=", value).Get(GetType());
        }

        public IList Find(object data)
        {
            PropertyInfo[] props = data.GetType().GetProperties();
            props.ToList().ForEach(prop =>
            {
                if (data.GetType().GetProperty(prop.Name).GetValue(data, null) != null)
                {
                    string value = "";
                    if (
                        data.GetType().GetProperty(prop.Name).GetValue(data, null) is
                        DateTime)
                        value =
                            ((DateTime)
                            data.GetType().GetProperty(prop.Name).GetValue(data, null)).
                                SetToMySQLDateString();
                    else
                        value =
                            data.GetType().GetProperty(prop.Name).GetValue(data, null).
                                ToString();
                    _query.Where(prop.Name, "=", value);
                }
            });

            return _query.Get(GetType());
        }


        public IList FindLike(Enum enums, string value)
        {
            return _query.Where(GetKey(enums), "like", value).Get(GetType());
        }

        public IList FindLike(string column, string value)
        {
            return _query.Where(column, "like", value).Get(GetType());
        }

        public dynamic FindOne(Enum enums, string value)
        {
            var item = Find(enums, value);
            if (item != null)
            {
                if(item.Count > 0)
                try
                {
                    return item[0];
                }
                catch (Exception e)
                {
                    e.Message.Println();
                    return null;
                }
            }
            return null;
        }

        public dynamic FindOne(object data)
        {
            var item = Find(data);
            if (item != null)
            {
                if (item.Count > 0)
                    try
                    {
                        return item[0];
                    }
                    catch (Exception e)
                    {
                        e.Message.Println();
                        return null;
                    }
            }
            return null;
        }

        public dynamic FindOne()
        {
            var item = Find(this);
            if (item != null)
            {
                if (item.Count > 0)
                    try
                    {
                        return item[0];
                    }
                    catch (Exception e)
                    {
                        e.Message.Println();
                        return null;
                    }
            }
            return null;
        }

        public dynamic First()
        {
            return Enumerable.FirstOrDefault(Get());
        }

        public dynamic Get(string column = "*", string order = "")
        {
            return _query.Get(GetType(), column, order);
        }

        public void Save()
        {
            _query.Save(this);
        }

        public int InsertAndGetLastID()
        {
            return _query.InsertAndGetLastID(this);
        }

        public void Delete(Enum eEnum, Operators oper,string value)
        {
            _query.Where(GetKey(eEnum), GetOperator(oper), value).Delete();
        }

        public void Delete()
        {
            string prime = _query.GetPrimaryKey(this);
            _query.Where(prime, "=", GetType().GetProperties()[0].GetValue(this, null).ToString()).Delete();
        }


        public string GetKey(Enum enums)
        {
            return GetType().GetProperties()[enums.ToInt()].Name;
        }

        private string GetOperator(Operators operators)
        {
            switch (operators)
            {
                case Operators.LIKE:
                    return "LIKE";
                default:
                    return "=";
            }
        }
    }
}