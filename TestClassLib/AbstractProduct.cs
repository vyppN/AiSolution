using AiLibraries.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClassLib
{
    public abstract class AbstractProduct:AiORM
    {
        public int? id { get; set; }
        public string name { get; set; }
    }
}
