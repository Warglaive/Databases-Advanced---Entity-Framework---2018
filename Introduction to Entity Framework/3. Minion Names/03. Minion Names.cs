using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _3._Minion_Names
{
    public class Program
    {
        public static void Main()
        {
            using (var sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                var id = int.Parse(Console.ReadLine());

                var villainName = GetVillainName(sqlConnection, id);
                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villainName}");
                    PrintNames(id, sqlConnection);
                }


                sqlConnection.Close();
            }
        }

        private static void PrintNames(int id, SqlConnection sqlConnection)
        {
            var selectMinions = @"SELECT Name, Age 
                            	    FROM Minions AS m
                                JOIN MinionsVillains AS mv
                                ON mv.MinionId = m.Id
                                WHERE mv.VillainId = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(selectMinions, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Id", id);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        var counter = 0;
                        while (reader.Read())
                        {
                            Console.WriteLine($"{++counter}. {reader[0]} {reader[1]}");
                        }
                    }
                }
            }
        }

        private static string GetVillainName(SqlConnection sqlConnection, int villainId)
        {
            var command = "SELECT Name FROM Villains WHERE Id = @id";
            using (var sqlCommand = new SqlCommand(command, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", villainId);
                return (string)sqlCommand.ExecuteScalar();
            }
        }
    }
}