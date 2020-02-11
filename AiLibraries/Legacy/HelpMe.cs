using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;

namespace AiLibraries.Legacy
{
    /// <summary>
    /// คลาสช่วยอำนวยความสะดวกในการทำงาน
    /// </summary>
    public class HelpMe
    {
        private static readonly StringCollection Log = new StringCollection();
        private static List<FileInfo> _paths;

        /// <summary>
        /// ตัดช่องว่างหัวท้ายออก แล้วตัดเอา String จากข้อความทางซ้ายของตัวแบ่งที่กำหนดมาใช้
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutString(ref string str, string cut)
        {
            string str2;
            int index = str.IndexOf(cut, StringComparison.Ordinal);
            if (index != -1)
            {
                str2 = Strings.Trim(str.Substring(index + cut.Length));
                string str3 = Strings.Trim(str.Substring(0, index));
                str = str2;
                return str3;
            }
            str2 = Strings.Trim(str);
            str = "";
            return str2;
        }

        /// <summary>
        /// ตัดเอา String จากข้อความทางซ้ายของตัวแบ่งที่กำหนดมาใช้ โดยไม่ตัดช่องว่างหัวท้ายออก
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutStringNoTrim(ref string str, string cut)
        {
            string str2;
            int index = str.IndexOf(cut, StringComparison.Ordinal);
            if (index != -1)
            {
                str2 = str.Substring(index + cut.Length);
                string str3 = str.Substring(0, index);
                str = str2;
                return str3;
            }
            str2 = str;
            str = "";
            return str2;
        }

        /// <summary>
        /// ตัดเอาว่างหัวท้ายออกจากข้อความ
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        public static string Alltrim(string str)
        {
            return Strings.RTrim(Strings.Trim(str));
        }


        /// <summary>
        /// ตัดเอาตัวอักษรออกมาทีละตัว
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        public static string CutChar(ref string str)
        {
            string str2;
            if (Alltrim(str) != "")
            {
                str2 = str.Substring(0, 1);
                str = str.Substring(1);
                return str2;
            }
            str2 = "";
            str = "";
            return str2;
        }


        /// <summary>
        /// ตัดเอาตัวอักษรออกมาทีละตัวจากท้ายข้อความ
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        public static string CutCharRight(ref string str)
        {
            string str2;
            int length = str.Length;
            if (str == "")
            {
                str2 = "";
                str = "";
                return str2;
            }
            str2 = str.Substring(length - 1, 1);
            str = str.Substring(0, length - 1);
            return str2;
        }


        /// <summary>
        /// ตัดช่องว่างหัวท้ายออก แล้วตัดเอา String จากข้อความทางขวาของตัวแบ่งที่กำหนดมาใช้
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutStringRight(ref string str, string cut)
        {
            string str2;
            int length = str.LastIndexOf(cut, StringComparison.Ordinal);
            if (length != -1)
            {
                str2 = Strings.Trim(str.Substring(0, length));
                string str3 = Strings.Trim(str.Substring(length + cut.Length));
                str = str2;
                return str3;
            }
            str2 = str;
            str = "";
            return str2;
        }

        /// <summary>
        /// ตัดเอา String จากข้อความทางขวาของตัวแบ่งที่กำหนดมาใช้ โดยไม่ตัดช่องว่างหัวท้ายออก
        /// </summary>
        /// <remarks>
        /// This is a remark.
        /// </remarks>
        /// <param name="str">ข้อความตั้งต้น</param>
        /// <param name="cut">ตัวแบ่ง</param>
        public static string CutStringRightNoTrim(ref string str, string cut)
        {
            string str2;
            int length = str.LastIndexOf(cut, StringComparison.Ordinal);
            if (length != -1)
            {
                str2 = str.Substring(0, length);
                string str3 = str.Substring(length + cut.Length);
                str = str2;
                return str3;
            }
            str2 = str;
            str = "";
            return str2;
        }

        //---------------------------------- GetFile --------------------------------
        /// <summary>
        /// อ่านรายชื่อไฟล์
        /// </summary>
        public static void GetFile()
        {
            string[] drives = Environment.GetLogicalDrives();
            foreach (string dr in drives)
            {
                var di = new DriveInfo(dr);

                if (!di.IsReady)
                {
                    ("The drive {0} could not be read").println(di.Name);
                    continue;
                }
                DirectoryInfo rootDir = di.RootDirectory;
                WalkDirectoryTree(rootDir);
            }
            ("Files eith restricted access:").println();
            foreach (string s in Log)
            {
                s.println();
            }

            ("Press any key").println();
            Console.ReadKey();
        }

        /// <summary>
        /// ไล่เก็บรายชื่อแฟ้มในไดเรคทอรี่ที่กำหนด
        /// </summary>
        /// <param name="root">ชื่อไดเรคทอรี่</param>
        /// 
        public static List<FileInfo> GetFilePath(DirectoryInfo root)
        {
            _paths = new List<FileInfo>();
            WalkDirectoryTree(root);
            return _paths;
        }

        public static void WalkDirectoryTree(DirectoryInfo root)
        {
            FileInfo[] file = null;
            try
            {
                file = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Add(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                e.Message.println();
            }
            if (file != null)
            {
                foreach (FileInfo fi in file)
                {
                    _paths.Add(fi);
                }
                DirectoryInfo[] subDirs = root.GetDirectories();
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo);
                }
            }
        }

        //---------------------------------- End GetFile --------------------------------

        /// <summary>
        /// ทำให้ตัวอักษรตัวแรกเป็นตัวพิมพ์ใหญ่
        /// </summary>
        /// <param name="s">ข้อความ</param>
        /// <returns>ข้อความที่แปลงตัวแรกเป็นพิมพ์ใหญ่</returns>
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// ทำให้ตัวอักษรตัวแรกเป็นตัวพิมพ์เล็ก
        /// </summary>
        /// <param name="s">ข้อความ</param>
        /// <returns>ข้อความที่แปลงตัวแรกเป็นพิมพ์เล็ก</returns>
        public static string LowercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToLower(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// ทำให้ตัวอักษรแรกของข้อความแต่ละวรรคเป็นตัวพิมพ์ใหญ่
        /// </summary>
        /// <param name="value">ข้อความ</param>
        /// <returns>ข้อความแต่ละวรรคเป็นตัวพิมพ์ใหญ่</returns>
        public static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        /// <summary>
        /// แปลงข้อความเป็นชื่อคลาส
        /// [ตัวแรกของแต่ละคำเป็นพิมพ์ใหญ่เรียงติดกันไม่เว้นวรรค]
        /// </summary>
        /// <param name="value">ข้อความที่ต้องการแปลง</param>
        /// <returns>ชื่อคลาส</returns>
        public static string ToClassName(string value)
        {
            string str = value.Replace('_', ' ');
            string str2 = UppercaseWords(str);
            int i = str2.IndexOf(' ');

            if (i >= 0)
            {
                str = "";
                while (str2 != "")
                {
                    string str3 = CutString(ref str2, " ");
                    str += str3;
                }
            }
            else
            {
                str = str2;
            }
            return str;
        }


        /// <summary>
        /// เช็คชนิดของข้อมูลที่ได้รับมาจาก Database
        /// </summary>
        /// <param name="value">ชนิดข้อมูล</param>
        /// <returns>ข้อมูลที่แปลงแล้ว</returns>
        public static string CheckType(string value)
        {
            string checkString = value.Remove(3);
            switch (checkString)
            {
                case "int":
                    return "int";
                case "var":
                    return "string";
                case "dat":
                    return "DateTime";
                case "cha":
                    return "string";
                case "tex":
                    return "string";
                case "blo":
                    return "Object";
                case "dou":
                    return "Double";
                case "lon":
                    return "string";
            }
            return null;
        }

        /// <summary>
        /// แปลง Type จาก Database ให้เป็น C# สำหรับการรับค่าจาก Database
        /// </summary>
        /// <param name="value">Type ที่จะแปลง</param>
        /// <returns></returns>
        public static string CheckTypeForORM(string value)
        {
            string checkString = value;
            switch (checkString)
            {
                case "int":
                    return "Int32";
                case "string":
                    return "String";
                case "dat":
                    return "DateTime";
                case "char":
                    return "Char";
                case "Object":
                    return "Byte";
                case "Double":
                    return "double";
            }
            return checkString;
        }

        public static bool HaveSearch(object searchStr)
        {
            bool chk = true;
            if (searchStr == null)
            {
                chk = false;
            }
            else if (searchStr.GetType().ToString() == "System.String")
            {
                if ((string) searchStr == "")
                {
                    chk = false;
                }
            }
            else if (searchStr.GetType().ToString() == "System.Int16")
            {
                if ((Int16) searchStr == Int16.MinValue)
                {
                    chk = false;
                }
            }
            else if (searchStr.GetType().ToString() == "System.Int32")
            {
                if ((Int32) searchStr == Int32.MinValue)
                {
                    chk = false;
                }
            }
            else if (searchStr.GetType().ToString() == "System.Int64")
            {
                if ((Int64) searchStr == Int64.MinValue)
                {
                    chk = false;
                }
            }
            else if (searchStr.GetType().ToString() == "System.Double")
            {
                if (Math.Abs((Double) searchStr - Double.MinValue) < 0.00001)
                {
                    chk = false;
                }
            }
            else if (searchStr.GetType().ToString() == "System.Single")
            {
                if (Math.Abs((Single) searchStr - Single.MinValue) < 0.00001)
                {
                    chk = false;
                }
            }
            else if (searchStr.GetType().ToString() == "System.Single")
            {
                if (Math.Abs((Single) searchStr - Single.MinValue) < 0.00001)
                {
                    chk = false;
                }
            }

            return chk;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

        public static string EncryptPassword(string strPassword)
        {
            var p = new MD5CryptoServiceProvider();
            byte[] arr = Encoding.UTF8.GetBytes(strPassword);
            arr = p.ComputeHash(arr);
            var sb = new StringBuilder();
            foreach (byte b in arr)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public static List<string> GetCommandLineParameter()
        {
            List<string> str = Environment.GetCommandLineArgs().ToList();
            File.WriteAllText(@"C:\Sriphatmed\zArgs.dat", string.Join("$",str.ToArray()));
            return str;
        }

        public static List<string> GetSharedFolders()
        {
            var sharedFolders = new List<string>();

            // Object to query the WMI Win32_Share API for shared files...

            var searcher = new ManagementObjectSearcher("select * from win32_share");


            foreach (ManagementObject share in searcher.Get())
            {
                string type = share["Type"].ToString();

                if (type == "0") // 0 = DiskDrive (1 = Print Queue, 2 = Device, 3 = IPH)
                {
                    string path = share["Path"].ToString(); //getting share path


                    sharedFolders.Add(path);
                }
            }

            return sharedFolders;
        }
    }
}