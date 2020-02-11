using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AiLibraries.Legacy.Old
{
    /// <summary>
    /// แปลงภาษาที่มาเป็นรหัส unicode
    /// </summary>
    public class EncodeNoneASCII
    {
        /// <summary>
        /// แปลงข้อคามไปเป็นรหัส
        /// </summary>
        /// <param name="value">ข้อความที่จะแปลง</param>
        /// <returns></returns>
        public static string EncodeNonAsciiCharacters(string value)
        {
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
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
        /// แปลงรหัสเป็นข้อความ
        /// </summary>
        /// <param name="value">รหัสที่จะแปลง</param>
        /// <returns></returns>
        public static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                    {
                        return
                            ((char) int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString(
                                CultureInfo.InvariantCulture);
                    });
        }
    }
}