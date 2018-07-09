using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        static void Main()
        {
            using (var hospitalContext = new HospitalContext())
            {
                //DatabaseInitializer.InitialSeed(hospitalContext);
            }
        }
    }
}