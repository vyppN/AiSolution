using System;
using Microsoft.VisualBasic;

namespace AiLibraries.Helper
{
    public class StringHelper
    {
        /// <summary>
        /// ตัดช่องว่างหัวท้ายออก แล้วตัดเอา String จากข้อความทางซ้ายของตัวแบ่งที่กำหนดมาใช้
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutString(ref string str, string cut)
        {
            string str2;
            int index = str.IndexOf(cut, StringComparison.Ordinal);
            if (index != -1)
            {
                str2 = Strings.Trim(str.Substring(index + cut.Length));
                string str3 = Strings.Trim(str.Substring(0, index));
                str = str2;
                return str3;
            }
            str2 = Strings.Trim(str);
            str = "";
            return str2;
        }

        /// <summary>
        /// ตัดเอา String จากข้อความทางซ้ายของตัวแบ่งที่กำหนดมาใช้ โดยไม่ตัดช่องว่างหัวท้ายออก
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutStringNoTrim(ref string str, string cut)
        {
            string str2;
            int index = str.IndexOf(cut, StringComparison.Ordinal);
            if (index != -1)
            {
                str2 = str.Substring(index + cut.Length);
                string str3 = str.Substring(0, index);
                str = str2;
                return str3;
            }
            str2 = str;
            str = "";
            return str2;
        }

        /// <summary>
        /// ตัดเอาว่างหัวท้ายออกจากข้อความ
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        public static string Alltrim(string str)
        {
            return Strings.RTrim(Strings.Trim(str)).Trim();
        }


        /// <summary>
        /// ตัดเอาตัวอักษรออกมาทีละตัว
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        public static string CutChar(ref string str)
        {
            string str2;
            if (Alltrim(str) != "")
            {
                str2 = str.Substring(0, 1);
                str = str.Substring(1);
                return str2;
            }
            str2 = "";
            str = "";
            return str2;
        }


        /// <summary>
        /// ตัดเอาตัวอักษรออกมาทีละตัวจากท้ายข้อความ
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        public static string CutCharRight(ref string str)
        {
            string str2;
            int length = str.Length;
            if (str == "")
            {
                str2 = "";
                str = "";
                return str2;
            }
            str2 = str.Substring(length - 1, 1);
            str = str.Substring(0, length - 1);
            return str2;
        }

        /// <summary>
        /// ตัดช่องว่างหัวท้ายออก แล้วตัดเอา String จากข้อความทางขวาของตัวแบ่งที่กำหนดมาใช้
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutStringRight(ref string str, string cut)
        {
            string str2;
            int length = str.LastIndexOf(cut, StringComparison.Ordinal);
            if (length != -1)
            {
                str2 = Strings.Trim(str.Substring(0, length));
                string str3 = Strings.Trim(str.Substring(length + cut.Length));
                str = str2;
                return str3;
            }
            str2 = str;
            str = "";
            return str2;
        }

        public static string GetRightString(string str,string cut)
        {
            int length = str.IndexOf(cut);
            if(length > -1)
            {
                return str.Substring(length+cut.Length);
            }
            return str;
        }

        /// <summary>
        /// ตัดเอา String จากข้อความทางขวาของตัวแบ่งที่กำหนดมาใช้ โดยไม่ตัดช่องว่างหัวท้ายออก
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutStringRightNoTrim(ref string str, string cut)
        {
            string str2;
            int length = str.LastIndexOf(cut, StringComparison.Ordinal);
            if (length != -1)
            {
                str2 = str.Substring(0, length);
                string str3 = str.Substring(length + cut.Length);
                str = str2;
                return str3;
            }
            str2 = str;
            str = "";
            return str2;
        }

        /// <summary>
        /// ทำให้ตัวอักษรตัวแรกเป็นตัวพิมพ์ใหญ่
        /// </summary>
        /// <param name="s">ข้อความ</param>
        /// <returns>ข้อความที่แปลงตัวแรกเป็นพิมพ์ใหญ่</returns>
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// ทำให้ตัวอักษรตัวแรกเป็นตัวพิมพ์เล็ก
        /// </summary>
        /// <param name="s">ข้อความ</param>
        /// <returns>ข้อความที่แปลงตัวแรกเป็นพิมพ์เล็ก</returns>
        public static string LowercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToLower(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// ทำให้ตัวอักษรแรกของข้อความแต่ละวรรคเป็นตัวพิมพ์ใหญ่
        /// </summary>
        /// <param name="value">ข้อความ</param>
        /// <returns>ข้อความแต่ละวรรคเป็นตัวพิมพ์ใหญ่</returns>
        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        /// <summary>
        /// แปลงข้อความเป็นชื่อคลาส
        /// [ตัวแรกของแต่ละคำเป็นพิมพ์ใหญ่เรียงติดกันไม่เว้นวรรค]
        /// </summary>
        /// <param name="value">ข้อความที่ต้องการแปลง</param>
        /// <returns>ชื่อคลาส</returns>
        public static string ToClassName(string value)
        {
            string str = value.Replace('_', ' ');
            string str2 = UppercaseWords(str);
            int i = str2.IndexOf(' ');

            if (i >= 0)
            {
                str = "";
                while (str2 != "")
                {
                    string str3 = CutString(ref str2, " ");
                    str += str3;
                }
            }
            else
            {
                str = str2;
            }
            return str;
        }

        /// <summary>
        /// แปลงข้อความเป็นชื่อฟิลด์
        /// [ตัวแรกสุดของคำเป็นพิมพ์เล็กและตัวแรกของแต่ละคำต่อไปเป็นพิมพ์ใหญ่เรียงติดกันไม่เว้นวรรค]
        /// </summary>
        /// <param name="value">ข้อความที่ต้องการแปลง</param>
        /// <returns>ชื่อคลาส</returns>
        public static string ToFieldName(string value)
        {
            return LowercaseFirst(ToClassName(value));
        }
    }
}