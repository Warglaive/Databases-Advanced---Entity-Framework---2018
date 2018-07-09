namespace MiniORM.App
{
    public class Config
    {
        private const string Server = @"Server=WARGLAIVE\SQLEXPRESS;";
        private const string Db = @"Database=MiniORM;";
        private const string Secutity = @"Integrated Security=True";
        public const string ConnectionString = Server + Db + Secutity;
    }
}