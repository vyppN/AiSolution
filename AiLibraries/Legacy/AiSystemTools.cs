using Microsoft.Win32;

namespace AiLibraries.Legacy
{
    public class AiSystemTools
    {
        public static void USBStorage(bool on)
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
    }
}