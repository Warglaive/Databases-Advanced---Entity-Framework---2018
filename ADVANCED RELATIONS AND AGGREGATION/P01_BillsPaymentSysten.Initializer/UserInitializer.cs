using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSysten.Initializer
{
    public class UserInitializer
    {
        public static User[] GetUsers()
        {
            var users = new User[20];

            for (int i = 0; i < users.Length; i++)
            {
                var currentUser = new User { FirstName = $"{i}.Gosho", LastName = $"{i}.Goshev", Email = $"{i}.EmailGAbv", Password = $"{i}.StrashenPass" };
                users[i] = currentUser;
            }
            return users;
        }
    }
}