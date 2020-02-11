using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using AiLibraries.Helper;
using Microsoft.VisualBasic.CompilerServices;

namespace AiLibraries.LibExtendsion
{
    public static class LibExtendsion
    {
        /// <summary>
        /// เช็คว่า String นี้เป็นตัวเลขรึเปล่า
        /// </summary>
        /// <param name="str">ค่าที่จะเช็ค</param>
        /// <returns>เป็นตัวเลขรึเปล่า</returns>
        public static bool IsNumber(this object str)
        {
            bool result;
            try
            {
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
                Convert.ToDouble(RuntimeHelpers.GetObjectValue(str));
// ReSharper restore ReturnValueOfPureMethodIsNotUsed
                result = true;
            }
            catch (Exception expr14)
            {
                ProjectData.SetProjectError(expr14);
                result = false;
                ProjectData.ClearProjectError();
            }
            return result;
        }

        /// <summary>
        /// แปลงข้อมูลในรูปของ List ไปเป็น Dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof (T);
            var ds = new DataSet();
            var t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (PropertyInfo propInfo in elementType.GetProperties())
            {
                Type colType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                t.Columns.Add(propInfo.Name, colType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (PropertyInfo propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }
                t.Rows.Add(row);
            }
            return ds;
        }

        /// <summary>
        /// แปลงตัวเลขจาก string เป็น int
        /// </summary>
        /// <param name="number">ตัวเลขเป็น string</param>
        /// <returns>ตัวเลขเป็น int</returns>
        public static int ToInt(this string number)
        {
            return int.Parse(number);
        }

        public static int ToInt(this object number)
        {
            return Convert.ToInt32(number);
        }

        /// <summary>
        /// แปลงตัวเลขจาก string เป็น double
        /// </summary>
        /// <param name="number">ตัวเลขเป็น string</param>
        /// <returns>ตัวเลขเป็น double</returns>
        public static double ToDouble(this string number)
        {
            return double.Parse(number);
        }

        /// <summary>
        /// แปลงตัวเลขจาก string เป็น float
        /// </summary>
        /// <param name="number">ตัวเลขเป็น string</param>
        /// <returns>ตัวเลขเป็น double</returns>
        public static double ToFloat(this string number)
        {
            return float.Parse(number);
        }

        /// <summary>
        /// สร้างกล่อง AlertBox
        /// </summary>
        /// <param name="alert">ข้อความที่ต้องการแสดง</param>
        public static void Alert(this string alert)
        {
            MessageBox.Show(alert);
        }

        public static void Error(this string message, string header = "Error")
        {
            MessageBox.Show(message, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Inform(this string message, string header = "Information")
        {
            MessageBox.Show(message, header, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// หาค่าที่มากที่สุดของ property ที่อยู่ใน List แบบ Lamda Expression
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static T1 MaxOf<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> selector)
        {
            using (IEnumerator<T1> item = source.GetEnumerator())
            {
                if (!item.MoveNext())
                {
                    throw new InvalidOperationException("source contains no element");
                }
                IComparer<T2> comparer = Comparer<T2>.Default;
                T1 candidate = item.Current;
                T2 score = selector(candidate);
                T1 winner = candidate;
                T2 maxScore = score;
                while (item.MoveNext())
                {
                    candidate = item.Current;
                    score = selector(candidate);
                    if (comparer.Compare(score, maxScore) > 0)
                    {
                        winner = candidate;
                        maxScore = score;
                    }
                }
                return winner;
            }
        }

        /// <summary>
        /// หาค่าที่น้อยที่สุดของ property ที่อยู่ใน List แบบ Lamda Expression
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static T1 MinOf<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> selector)
        {
            using (IEnumerator<T1> item = source.GetEnumerator())
            {
                if (!item.MoveNext())
                {
                    throw new InvalidOperationException("source contains no element");
                }
                IComparer<T2> comparer = Comparer<T2>.Default;
                T1 candidate = item.Current;
                T2 score = selector(candidate);
                T1 loser = candidate;
                T2 maxScore = score;
                while (item.MoveNext())
                {
                    candidate = item.Current;
                    score = selector(candidate);
                    if (comparer.Compare(score, maxScore) < 0)
                    {
                        loser = candidate;
                        maxScore = score;
                    }
                }

                return loser;
            }
        }

        /// <summary>
        /// ทำให้ตัวอักษรตัวแรกเป็นตัวพิมพ์ใหญ่
        /// </summary>
        /// <param name="s">ข้อความ</param>
        /// <returns>ข้อความที่แปลงตัวแรกเป็นพิมพ์ใหญ่</returns>
        public static string UppercaseFirst(this string s)
        {
            return StringHelper.UppercaseFirst(s);
        }

        /// <summary>
        /// ทำให้ตัวอักษรตัวแรกเป็นตัวพิมพ์เล็ก
        /// </summary>
        /// <param name="s">ข้อความ</param>
        /// <returns>ข้อความที่แปลงตัวแรกเป็นพิมพ์เล็ก</returns>
        public static string LowercaseFirst(this string s)
        {
            return StringHelper.LowercaseFirst(s);
        }

        /// <summary>
        /// ทำให้ตัวอักษรแรกของข้อความแต่ละวรรคเป็นตัวพิมพ์ใหญ่
        /// </summary>
        /// <param name="value">ข้อความ</param>
        /// <returns>ข้อความแต่ละวรรคเป็นตัวพิมพ์ใหญ่</returns>
        public static string UppercaseWords(this string value)
        {
            return StringHelper.UppercaseWords(value);
        }

        /// <summary>
        /// แปลงข้อความเป็นชื่อคลาส
        /// [ตัวแรกของแต่ละคำเป็นพิมพ์ใหญ่เรียงติดกันไม่เว้นวรรค]
        /// </summary>
        /// <param name="value">ข้อความที่ต้องการแปลง</param>
        /// <returns>ชื่อคลาส</returns>
        public static string ToClassName(this string value)
        {
            return StringHelper.ToClassName(value);
        }

        /// <summary>
        /// มันก็นคือ Console.Write(); น่ะแหละ
        /// </summary>
        /// <param name="obj"></param>
        public static void Print(this object obj)
        {
            Console.Write(obj.ToString());
        }

        /// <summary>
        /// มันก็นคือ Console.WriteLine(); น่ะแหละ
        /// </summary>
        public static void Println()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// มันก็นคือ Console.WriteLine(); น่ะแหละ
        /// </summary>
        /// <param name="obj"></param>
        public static void Println(this object obj)
        {
            Console.WriteLine(obj.ToString());
        }

        public static void Println(this object obj, object objt)
        {
            Console.WriteLine(obj.ToString(), objt);
        }

        public static void End()
        {
            Console.ReadLine();
        }

        /// <summary>
        /// แปลงข้อคามไปเป็นรหัส JSON Unicode
        /// </summary>
        /// <param name="value">ข้อความที่จะแปลง</param>
        /// <returns></returns>
        public static string EncodeNonAsciiCharacters(this string value)
        {
            return TextEncoding.EncodeNonAsciiCharacters(value);
        }

        /// <summary>
        /// แปลงรหัส JSON Unicode เป็นข้อความ
        /// </summary>
        /// <param name="value">รหัส Unicode ที่จะแปลง</param>
        /// <returns></returns>
        public static string DecodeEncodedNonAsciiCharacters(this string value)
        {
            return TextEncoding.DecodeEncodedNonAsciiCharacters(value);
        }

        /// <summary>
        /// แปลง latin1-swedish เป็น utf-8
        /// </summary>
        /// <param name="str">ข้อความ latin1</param>
        /// <returns>ข้อความ utf-8</returns>
        public static string LatinToThai(this string str)
        {
            return TextEncoding.LatinToThai(str);
        }

        public static string ToThai(this string str)
        {
            return str.LatinToThai();
        }

        /// <summary>
        /// แปลงข้อความ utf-8 เป็น latin1-swedish
        /// </summary>
        /// <param name="str">ข้อความ utf-8</param>
        /// <returns>ข้อความ latin1</returns>
        public static string ThaiToLatin(this string str)
        {
            return TextEncoding.ThaiToLatin(str);
        }

        public static string ToLatin(this string str)
        {
            return str.ThaiToLatin();
        }

        public static bool IsIn(this string str, string[] strA)
        {
            return strA.Contains(str);
        }

        public static bool IsIn(this string str, List<string> strL)
        {
            return strL.Contains(str);
        }

        public static bool IsIn(this string str, string str2)
        {
            return str2.Contains(str);
        }
    }
}