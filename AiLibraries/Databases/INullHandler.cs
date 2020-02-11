using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AiLibraries.Databases
{
    /// <summary>
    /// Interface สำหรับ class ที่ต้องการใช้ Null Type Handler
    /// </summary>
    public interface INullHandler
    {
        IList Find(string order);
        IList Find(Enum enums,string value);
        IList Find(string column, string value);
        IList Find(object data);
        IList FindLike(Enum enums, string value);
        IList FindLike(string column, string value);
        dynamic FindOne(Enum enums, string value);
        dynamic FindOne(object data);
        dynamic FindOne();
        dynamic First();
        dynamic Get(string column = "*", string order = "");
    }
}
