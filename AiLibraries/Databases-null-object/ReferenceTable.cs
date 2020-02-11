namespace AiLibraries.Databases
{
    public class ReferenceTable
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumnName { get; set; }
    }
}