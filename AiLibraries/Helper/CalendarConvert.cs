using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLibraries.Helper
{
    public class CalendarConvert
    {
        public static CultureInfo GetThaiCulture(){
            CultureInfo th = System.Globalization.CultureInfo.GetCultureInfo("th-TH");
            return th;
        }

        public static CultureInfo GetUSCulture()
        {
            CultureInfo us = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            return us;
        }
    }
}
