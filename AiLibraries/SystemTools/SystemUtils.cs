using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Microsoft.Win32;

namespace AiLibraries.SystemTools
{
    public class SystemUtils
    {
        /// <summary>
        /// สั่งปิด/เปิด การใช้งาน Handy drive
        /// </summary>
        /// <param name="on"></param>
        public static void UsbStorageEnabled(bool on)
        {
            if (on)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 3,
                                  RegistryValueKind.DWord);
            }
            else
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 4,
                                  RegistryValueKind.DWord);
            }
        }

        /// <summary>
        /// เรียก Console argument ที่ส่งมาตอนเปิดโปรแกรม
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCommandLineParameter()
        {
            List<string> str = Environment.GetCommandLineArgs().ToList();
            return str;
        }

        /// <summary>
        /// เรียกใช้ \public\shared\
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSharedFolders()
        {
            var sharedFolders = new List<string>();

            // Object to query the WMI Win32_Share API for shared files

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