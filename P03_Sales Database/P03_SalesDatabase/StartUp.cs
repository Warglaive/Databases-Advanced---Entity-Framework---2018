using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    public class Program
    {
        public static void Main()
        {
            using (var context = new SalesDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}