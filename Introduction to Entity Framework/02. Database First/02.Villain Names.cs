using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _02._Database_First
{
    public class Program
    {
        public static void Main()
        {
            using (var sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                var selectCommand = @"SELECT v.[Name], COUNT(mv.MinionId) AS [mCount]
                                        FROM Villains AS v
                                     JOIN MinionsVillains AS mv
                                     ON mv.VillainId = v.Id
                                     GROUP BY v.Name
                                     HAVING COUNT(mv.MinionId) > 3
                                     ORDER BY [mCount] DESC";
                using (var command = new SqlCommand(selectCommand, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} - {reader[1]}");
                        }
                    }
                }

                sqlConnection.Close();
            }
        }
    }
}