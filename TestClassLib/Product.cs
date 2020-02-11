using AiLibraries.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClassLib
{
    public class Product : AbstractProduct
    {
        private static Product _instance;
        private readonly bool _isNullObject;


        protected override bool IsNull()
        {
            return _isNullObject;
        }

        public static Product Instance()
        {
            if (_instance == null)
                _instance = new Product();
            return _instance;
        }

        public Product()
        {
        }

        public Product(bool isNull = false)
        {
            _isNullObject = isNull;
            if (isNull)
            {
                base.id = -1;
                base.name = NullString;
            }
        }
    }
}
