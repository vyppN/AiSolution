using System;
using System.Collections.Generic;
using System.IO;

namespace AiLibraries.Helper
{
    public class HelpMe
    {
        private static List<FileInfo> _paths;


        /// <summary>
        /// ไล่เก็บรายชื่อแฟ้มในไดเรคทอรี่ที่กำหนด
        /// </summary>
        /// <param name="root">ชื่อไดเรคทอรี่</param>
        public static List<FileInfo> GetAllFilesPathFromDirectory(DirectoryInfo root)
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
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
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

        /// <summary>
        /// แปลงเอกพจน์เป็นพหูพจน์
        /// </summary>
        /// <param name="singularWord"></param>
        /// <returns></returns>
        public static string SingularToPural(string singularWord)
        {
            switch (singularWord.ToLower().Substring(singularWord.Length - 1))
            {
                case "y":
                    return singularWord.ToLower().Substring(0, singularWord.Length - 1) + "ies";
                case "s":
                    return singularWord.ToLower() + "es";
                case "f":
                    return singularWord.ToLower().Substring(0, singularWord.Length - 1) + "ves";
                default:
                    return singularWord.ToLower() + "s";
            }
        }

        public static string GetTableName(string className)
        {
            return SingularToPural(className);
            //switch (className.ToLower().Substring(className.Length - 1))
            //{
            //    case "y":
            //        return className.ToLower().Substring(0, className.Length - 1) + "ies";
            //    case "s":
            //        return className.ToLower() + "es";
            //    case "f":
            //        return className.ToLower().Substring(0, className.Length - 1) + "ves";
            //    default:
            //        return className.ToLower() + "s";
            //}
        }
    }
}