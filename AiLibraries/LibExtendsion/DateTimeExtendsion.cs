using System;
using System.Collections.Generic;
using System.Globalization;

namespace AiLibraries.LibExtendsion
{
    /// <summary>
    /// method สำหรับเรียกใช้ด่วนๆ
    /// สร้างวันที่เป็น เดือนมกราคม วิธีใช้  วันที่.เดือน(ปี)
    /// </summary>
    public static class DateTimeExtendsion
    {
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

        //-------------MISC----------------------------------------

        /// <summary>
        /// แปลงเป็น คริสตศักราช
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string SetToMySQLDateString(this DateTime date)
        {

            var cul = new CultureInfo("en-US");
            return date.ToString("yyyy-MM-dd",cul);
        }

        ///<summary>
        /// แปลงเป็นวันที่ภาษาไทย
        /// </summary>
        /// <param name="date">วันที่</param>
        ///<param name="hasZero"> </param>
        public static string ToThaiDateString(this DateTime date, bool hasZero = false)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            string[] dateArray = dateString.Split('-');

            int year = Convert.ToInt32(dateArray[0]);
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
            if (!hasZero)
                dateArray[2] = Convert.ToInt32(dateArray[2]).ToString(CultureInfo.InvariantCulture);
            return dateArray[2] + " " + month + " " + year;
        }
    }
}