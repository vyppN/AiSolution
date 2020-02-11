using System;

namespace AiLibraries.Databases
{
    internal class MySqlDBException : Exception
    {
        public new string Message;

        public MySqlDBException(string dataName)
        {
            Message = dataName + " data is NULL.";
        }
    }
}