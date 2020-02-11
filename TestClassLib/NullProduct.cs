using AiLibraries.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClassLib
{
    public class NullProduct : AbstractProduct
    {
        private static AiORM _instance;
       
        public NullProduct()
        {
            base.id = -1;
            base.name = NullString;
        }

        public static AiORM Instance()
        {
            if (_instance == null)
                _instance = new NullProduct();
            return _instance;
        }

        protected override bool IsNull()
        {
            return true;
        }
    }
}
