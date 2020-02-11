using System;

namespace AiLibraries.Legacy
{
    /// <summary>
    /// เอาไว้ print ข้อความออกทางหน้าจอ
    /// </summary>
    public static class PrintHelp
    {
        /// <summary>
        /// มันก็นคือ Console.Write(); น่ะแหละ
        /// </summary>
        /// <param name="obj"></param>
        public static void print(this object obj)
        {
            Console.Write(obj.ToString());
        }

        /// <summary>
        /// มันก็นคือ Console.WriteLine(); น่ะแหละ
        /// </summary>
        public static void println()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// มันก็นคือ Console.WriteLine(); น่ะแหละ
        /// </summary>
        /// <param name="obj"></param>
        public static void println(this object obj)
        {
            Console.WriteLine(obj.ToString());
        }

        public static void println(this object obj, object objt)
        {
            Console.WriteLine(obj.ToString(), objt);
        }

        public static void end()
        {
            Console.ReadLine();
        }
    }
}