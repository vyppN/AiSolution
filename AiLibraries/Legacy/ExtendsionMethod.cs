using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;

namespace AiLibraries.Legacy
{
    /// <summary>
    /// method สำหรับเรียกใช้ด่วนๆ
    /// สร้างวันที่เป็น เดือนมกราคม วิธีใช้  วันที่.เดือน(ปี)
    /// </summary>
    public static class ExtendsionMethod
    {
        //private static Action EmptyDelegate = delegate() { };

        //public static void Refresh(this UIElement uiElement)
        //{
        //    uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        //}

        //------------------- Date Helper --------------------

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime January(this int i, int year)
        {
            return new DateTime(year, 1, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime February(this int i, int year)
        {
            return new DateTime(year, 2, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime March(this int i, int year)
        {
            return new DateTime(year, 3, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime April(this int i, int year)
        {
            return new DateTime(year, 4, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime May(this int i, int year)
        {
            return new DateTime(year, 5, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime June(this int i, int year)
        {
            return new DateTime(year, 6, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime July(this int i, int year)
        {
            return new DateTime(year, 7, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime August(this int i, int year)
        {
            return new DateTime(year, 8, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime September(this int i, int year)
        {
            return new DateTime(year, 9, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime October(this int i, int year)
        {
            return new DateTime(year, 10, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime November(this int i, int year)
        {
            return new DateTime(year, 11, i);
        }

        /// <summary>
        /// สร้างวันที่ วิธีใช้ วันที่.เดือน(ปี)
        /// </summary>
        /// <param name="i">วันที่</param>
        /// <param name="year">ปี</param>
        /// <returns></returns>
        public static DateTime December(this int i, int year)
        {
            return new DateTime(year, 12, i);
        }

        //---------------- End Date Helper -------------------- 

        //---------------- Time Span Helper -------------------
        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Days(this int i)
        {
            return new TimeSpan(i, 0, 0, 0);
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Hours(this int i)
        {
            return new TimeSpan(i, 0, 0);
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Minutes(this int i)
        {
            return new TimeSpan(0, i, 0);
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static TimeSpan Seconds(this int i)
        {
            return new TimeSpan(0, 0, i);
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime Ago(this TimeSpan ts)
        {
            return DateTime.Now - ts;
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime AgoSince(this TimeSpan ts, DateTime dt)
        {
            return dt - ts;
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime FromNow(this TimeSpan ts)
        {
            return DateTime.Now + ts;
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime From(this TimeSpan ts, DateTime dt)
        {
            return dt + ts;
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> To(this DateTime from, DateTime to)
        {
            for (DateTime dt = from; dt <= to; dt = dt.AddDays(1.0))
                yield return dt;
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DownTo(this DateTime from, DateTime to)
        {
            for (DateTime dt = from; dt >= to; dt = dt.AddDays(-1.0))
                yield return dt;
        }

        /// <summary>
        /// สร้างช่วงเวลา เช่น 20นาที, 1ชั่วโมง หรือ 20 นาทีที่แล้ว เป็นต้น
        /// จากต้นปี ถึงปลายปีทุก ๆ 3 วัน
        /// January(2008).StepTo(31.December(2008), 3.Days());
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> StepTo(this DateTime from, DateTime to, TimeSpan step)
        {
            if (step.Ticks > 0L)
            {
                for (DateTime dt = from; dt <= to; dt = dt.Add(step))
                    yield return dt;
            }
            else if (step.Ticks < 0L)
            {
                for (DateTime dt = from; dt >= to; dt = dt.Add(step))
                    yield return dt;
            }
            else
                throw new ArgumentException("step cannot be zero");
        }

        //-------------End Time Span Helper -------------------


        /// <summary>
        /// แปลงข้อมูลในรูปของ IList ไปเป็น Dataset
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
        /// สร้างกล่อง AlertBox
        /// </summary>
        /// <param name="alert">ข้อความที่ต้องการแสดง</param>
        public static void Alert(this string alert)
        {
            MessageBox.Show(alert);
        }

        ///<summary>
        /// แปลงเป็นวันที่ภาษาไทย
        /// </summary>
        /// <param name="date">วันที่</param>
        public static string ToThaiDateString(this DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            string[] dateArray = dateString.Split('-');
            int year = ToInt32(dateArray[0]);
            if (year < 2500) year += 543;

            string month = "";

            switch (dateArray[1])
            {
                case "01":
                    month = "มกราคม";
                    break;
                case "02":
                    month = "กุมภาพันธ์";
                    break;
                case "03":
                    month = "มีนาคม";
                    break;
                case "04":
                    month = "เมษายน";
                    break;
                case "05":
                    month = "พฤษภาคม";
                    break;
                case "06":
                    month = "มิถุนายน";
                    break;
                case "07":
                    month = "กรกฎาคม";
                    break;
                case "08":
                    month = "สิงหาคม";
                    break;
                case "09":
                    month = "กันยายน";
                    break;
                case "10":
                    month = "ตุลาคม";
                    break;
                case "11":
                    month = "พฤศจิกายน";
                    break;
                case "12":
                    month = "ธันวาคม";
                    break;
            }

            return dateArray[2] + " " + month + " " + year;
        }

        ///<summary>
        /// แปลงเป็น int
        /// </summary>
        /// <param name="value">ค่าที่จะแปลง</param>
        public static int ToInt32(this string value)
        {
            return Convert.ToInt32(value);
        }

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

        public static string SetToGregorian(this string dateDDMMYYYY, char delimiter)
        {
            string[] temp = dateDDMMYYYY.Split(delimiter);
            int year = temp[temp.Length - 1].ToInt32();
            if (year > 2500)
                return (year - 543) + "-" + temp[1] + "-" + temp[0];
            return year + "-" + temp[1] + "-" + temp[0];
        }
    }
}