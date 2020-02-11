using System;

namespace AiLibraries.Legacy.Old
{
    /// <summary>
    /// ข้อมูล Table สำหรับ Database
    /// </summary>
    public class DBInfo
    {
        /// <summary>
        /// ชื่อตาราง
        /// </summary>
        public String tableName { get; set; }

        /// <summary>
        /// ชื่อคอลัมน์
        /// </summary>
        public String columnName { get; set; }

        /// <summary>
        /// ประเภทข้อมูล
        /// </summary>
        public String columnType { get; set; }

        /// <summary>
        /// เป็น Primary Key มั๊ย?
        /// </summary>
        public String primary { get; set; }
    }
}