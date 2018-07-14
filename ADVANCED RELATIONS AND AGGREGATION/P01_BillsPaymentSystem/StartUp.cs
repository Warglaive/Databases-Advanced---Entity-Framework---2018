using P01_BillsPaymentSystem.Data;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            using (var Context = new BillsPaymentSystemContext())
            {
                Context.Database.EnsureDeleted();
                Context.Database.EnsureCreated();
            }
        }
    }
}