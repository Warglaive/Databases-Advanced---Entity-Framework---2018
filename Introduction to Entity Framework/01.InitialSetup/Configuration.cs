namespace _01.InitialSetup
{
    public static class Configuration
    {
        public const string Minionsdb = @"MinionsDB";
        private const string Server = @"Server=WARGLAIVE\SQLEXPRESS;";
        private const string Db = @"Database=MinionsDB;";
        private const string Security = @"Integrated Security=True";
        public const string ConnectionString = Server + Db + Security;
    }
}