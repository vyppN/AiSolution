using System;

namespace AiLibraries.Databases
{
    /// <summary>
    /// มันจะเด้ง Pop up มาเองตอนหา Table ไม่เจอ
    /// </summary>
    internal class MySqlDBException : Exception
    {
        public new string Message;

        public MySqlDBException(string dataName)
        {
            Message = dataName + " data is NULL.";
        }
    }
}