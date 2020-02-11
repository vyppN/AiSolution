using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AiLibraries.Helper
{
    public class TextEncoding
    {
        /// <summary>
        /// แปลงข้อคามไปเป็นรหัส JSON Unicode
        /// </summary>
        /// <param name="value">ข้อความที่จะแปลง</param>
        /// <returns></returns>
        public static string EncodeNonAsciiCharacters(string value)
        {
            var sb = new StringBuilder();
            if (value == null) return "";
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // นอก ASCII Table
                    string encodedValue = "\\u" + ((int) c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// แปลงรหัส JSON Unicode เป็นข้อความ
        /// </summary>
        /// <param name="value">รหัส Unicode ที่จะแปลง</param>
        /// <returns></returns>
        public static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                ((char) int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString(
                    CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// แปลง latin1-swedish เป็น utf-8
        /// </summary>
        /// <param name="str">ข้อความ latin1</param>
        /// <returns>ข้อความ utf-8</returns>
        public static string LatinToThai(string str)
        {
            if (str == null) return "";
            return Encoding.Default.GetString(Encoding.GetEncoding(1252).GetBytes(str));
        }

        /// <summary>
        /// แปลงข้อความ utf-8 เป็น latin1-swedish
        /// </summary>
        /// <param name="str">ข้อความ utf-8</param>
        /// <returns>ข้อความ latin1</returns>
        public static string ThaiToLatin(string str)
        {
            if (str == null) return "";
            return Encoding.GetEncoding("latin1").GetString(Encoding.Default.GetBytes(str));
        }
    }
}