using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSysten.Initializer;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            using (var context = new BillsPaymentSystemContext())
            {
                Initialize.Seed(context);
            }
        }
    }
}