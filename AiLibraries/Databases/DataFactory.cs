using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AiLibraries.Databases
{
    /// <summary>
    /// สร้าง object ที่ support null handler ผ่าน Factory Design Pattern
    /// </summary>
    public class DataFactory
    {
        public static IList Find(CriteriaObject criteria)
        {
            return Find(criteria.ObjectType, criteria.NullType);
        }

        public static IList Find(Type dataType, Type nullType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find");
            var dataOutput = methodFind.Invoke(dataObject, null);
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                return new List<object>() { Activator.CreateInstance(nullType) };
            }
            return (IList)dataOutput;
        }
        public static IList Find(Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find",new Type[]{});
            var dataOutput = methodFind.Invoke(dataObject, null);
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return new List<object>() { ctor.Invoke(new object[] { true }) };
            }
            return (IList)dataOutput;
        }

        public static IList Find(string order, CriteriaObject criteria)
        {
            return Find(criteria.ObjectType, criteria.NullType, order);
        }
        public static IList Find(Type dataType, Type nullType, string order)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find", new Type[] { typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { order });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                return new List<object>() { Activator.CreateInstance(nullType) };
            }
            return (IList)dataOutput;
        }
        public static IList Find(string order, Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find", new Type[] { typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { order });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return new List<object>() { ctor.Invoke(new object[] { true }) };
            }
            return (IList)dataOutput;
        }


        public static IList Find(string column, string value, CriteriaObject criteria)
        {
            return Find(criteria.ObjectType, criteria.NullType, column, value);
        }
        public static IList Find(Type dataType, Type nullType, string column, string value)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find", new Type[] { typeof(string),typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { column,value });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                return new List<object>() { Activator.CreateInstance(nullType) };
            }
            return (IList)dataOutput;
        }
        public static IList Find(string column, string value, Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find", new Type[] { typeof(string),typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { column,value });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return new List<object>() { ctor.Invoke(new object[] { true }) };
            }
            return (IList)dataOutput;
        }


        public static IList Find(object data, CriteriaObject criteria)
        {
            return Find(criteria.ObjectType, criteria.NullType, data);
        }

        public static IList Find(Type dataType,Type nullType,object data)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find",new Type[]{typeof(object)});
            var dataOutput = methodFind.Invoke(dataObject, new[] {data});
            MethodInfo methodCount = null;
            if(dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                return new List<object>() {Activator.CreateInstance(nullType)};
            }
            return (IList) dataOutput;
        }

        public static IList Find(object data, Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Find", new Type[] { typeof(object) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { data });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] {typeof (bool)});
                return new List<object>() { ctor.Invoke(new object[]{true}) };
            }
            return (IList)dataOutput;
        }

        public static IList FindLike(string column, string value, CriteriaObject criteria)
        {
            return FindLike(criteria.ObjectType, criteria.NullType, column, value);
        }
        public static IList FindLike(Type dataType, Type nullType, string column, string value)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("FindLike", new Type[] { typeof(string),typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { column,value });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                return new List<object>() { Activator.CreateInstance(nullType) };
            }
            return (IList)dataOutput;
        }
        public static IList FindLike(object data, Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("FindLike", new Type[] { typeof(object) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { data });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return new List<object>() { ctor.Invoke(new object[] { true }) };
            }
            return (IList)dataOutput;
        }

        public static dynamic FindOne(object data, CriteriaObject criteria)
        {
            return FindOne(criteria.ObjectType, criteria.NullType, data);
        }
        public static dynamic FindOne(Type dataType, Type nullType, object data)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("FindOne", new Type[] { typeof(object) });
            var dataOutput = methodFind.Invoke(dataObject, new[] {data});
            if (dataOutput == null)
            {
                return Activator.CreateInstance(nullType);
            }
            return dataOutput;
        }
        public static dynamic FindOne(object data, Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("FindOne", new Type[] { typeof(object) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { data });
            if (dataOutput == null)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return ctor.Invoke(new object[] { true });
            }
            return dataOutput;
        }

        public static dynamic FindOne(CriteriaObject criteria)
        {
            return FindOne(criteria.ObjectType, criteria.NullType);
        }
        public static dynamic FindOne(Type dataType, Type nullType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("FindOne");
            var dataOutput = methodFind.Invoke(dataObject, null);
            if (dataOutput == null)
            {
                return Activator.CreateInstance(nullType);
            }
            return dataOutput;
        }
        public static dynamic FindOne(Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("FindOne");
            var dataOutput = methodFind.Invoke(dataObject,null);
            if (dataOutput == null)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return ctor.Invoke(new object[] { true });
            }
            return dataOutput;
        }

        public static dynamic First(CriteriaObject criteria)
        {
            return First(criteria.ObjectType, criteria.NullType);
        }
        public static dynamic First(Type dataType, Type nullType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("First");
            var dataOutput = methodFind.Invoke(dataObject, null);
            if (dataOutput == null)
            {
                return Activator.CreateInstance(nullType);
            }
            return dataOutput;
        }
        public static dynamic First(Type dataType)
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("First");
            var dataOutput = methodFind.Invoke(dataObject, null);
            if (dataOutput == null)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return ctor.Invoke(new object[] { true });
            }
            return dataOutput;
        }


        public static dynamic Get(CriteriaObject criteria, string column = "*", string order = "")
        {
            return Get(criteria.ObjectType, criteria.NullType, column, order);
        }
        public static dynamic Get(Type dataType, Type nullType,string column = "*", string order = "")
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Get", new Type[] { typeof(string), typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { column, order });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                return new List<object>() { Activator.CreateInstance(nullType) };
            }
            return (IList)dataOutput;
        }
        public static IList Get(Type dataType, string column = "*", string order = "")
        {
            var dataObject = Activator.CreateInstance(dataType);
            MethodInfo methodFind = dataObject.GetType().GetMethod("Get", new Type[] { typeof(string), typeof(string) });
            var dataOutput = methodFind.Invoke(dataObject, new[] { column,order });
            MethodInfo methodCount = null;
            if (dataOutput is IList)
                methodCount = dataOutput.GetType().GetMethod("get_Count");
            if (dataOutput == null || (int)methodCount.Invoke(dataOutput, null) == 0)
            {
                ConstructorInfo ctor = dataType.GetConstructor(new[] { typeof(bool) });
                return new List<object>() { ctor.Invoke(new object[] { true }) };
            }
            return (IList)dataOutput;
        }

    }
}
