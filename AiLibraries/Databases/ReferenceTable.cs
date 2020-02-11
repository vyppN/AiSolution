using System;

namespace AiLibraries.Databases
{
    /// <summary>
    /// จำไม่ได้แล้วว่าเขียนมาทำไม
    /// ไม่ได้ใช้แล้ว
    /// </summary>
    [Obsolete]
    public class ReferenceTable
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumnName { get; set; }
    }
}