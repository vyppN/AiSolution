using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiLibraries;
using AiLibraries.Databases;
using AiLibraries.LibExtendsion;

namespace TestClassLib
{

    class Program
    {
        static void Main(string[] args)
        {
            UniqueDB.Instance.SetConnection("127.0.0.1", "testbase", "root", "1234").GetConnection().Open();
            

            var products = DataFactory.Find(typeof(Product), typeof(NullProduct), new Product() { name = "psp" });
            //var needle = new Product() { name = "PS" };
            //var products = DataFactory.FindOne(typeof(Product), typeof(NullProduct), needle);
            //var products = DataFactory.Find(needle, typeof(Product));

            //var product = (AbstractProduct)products[0];
            foreach (AbstractProduct product in products)
            {
                Console.WriteLine(product.name);
            }
        }
    }
}
