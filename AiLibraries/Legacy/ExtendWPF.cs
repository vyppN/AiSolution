using System.Windows;

namespace AiLibraries.Legacy
{
    public static class ExtendWPF
    {
        public static void Alert(this object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        public static void ErrorBox(this string header, string message)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void InformBox(this string header, string message)
        {
            MessageBox.Show(message, header, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}