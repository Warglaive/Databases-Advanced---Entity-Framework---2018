using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _05._Change_Town_Names_Casing
{
    public class Program
    {
        public static void Main()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                var countryName = Console.ReadLine();
                sqlConnection.Open();
                sqlConnection.ChangeDatabase(Configuration.Minionsdb);
                var update = @"UPDATE Towns
                                SET Name = UPPER(Name)
                                WHERE CountryCode = (SELECT Id FROM Countries
                                					WHERE Name = @Name)";
                using (SqlCommand sqlCommand = new SqlCommand(update, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Name", countryName);
                    var namesAffected = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine(namesAffected > 0
                        ? $"{namesAffected} town names were affected. "
                        : "No town names were affected.");
                    var select = @"SELECT Name FROM Towns
                                    WHERE CountryCode = (SELECT Id FROM Countries
                                    WHERE Name = @Name)";
                    using (SqlCommand innerSqlCommand = new SqlCommand(select, sqlConnection))
                    {
                        innerSqlCommand.Parameters.AddWithValue("Name", countryName);
                        using (SqlDataReader reader = innerSqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader[0]}");
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
        }
    }
}