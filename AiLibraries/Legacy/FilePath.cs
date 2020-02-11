namespace AiLibraries.Legacy
{
    public class FilePath
    {
        private static FilePath _instant;
        public string Name { get; set; }
        public string Path { get; set; }

        public static FilePath GetInstance()
        {
            if (_instant == null)
            {
                _instant = new FilePath();
            }

            return _instant;
        }
    }
}