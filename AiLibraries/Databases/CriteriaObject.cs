using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLibraries.Databases
{
    /// <summary>
    /// ตัวจัดการกับ Null Type Support
    /// </summary>
    public class CriteriaObject
    {
        public Type ObjectType { get; set; }
        public Type NullType { get; set; }

        public CriteriaObject(Type objectType, Type nullType)
        {
            ObjectType = objectType;
            NullType = nullType;
        }
    }
}
