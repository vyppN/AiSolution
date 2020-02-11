using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualBasic.CompilerServices;

namespace AiLibraries.Legacy
{
    public static class AiLibExtend
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
    }

    public static class FILETIMEExtensions
    {
        public static DateTime ToDateTime(this FILETIME filetime)
        {
            long highBits = filetime.dwHighDateTime;
            highBits = highBits << 32;
            return DateTime.FromFileTimeUtc(highBits + filetime.dwLowDateTime);
        }
    }
}