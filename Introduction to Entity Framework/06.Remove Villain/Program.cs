using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _06.Remove_Villain
{
    public class Program
    {
        public static void Main()
        {
            using (var sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.ChangeDatabase(Configuration.Minionsdb);
                var input = int.Parse(Console.ReadLine());
                //check if id is valid
                var villainId = GetVillainId(input, sqlConnection);
                if (villainId == 0)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }
                DeleteVillain(sqlConnection, villainId);
                sqlConnection.Close();
            }
        }

        private static void DeleteVillain(SqlConnection sqlConnection, int villainId)
        {
            //take villain Name
            string villainName = GetVillainName(sqlConnection, villainId);
            //delete from MinionsVillain
            var delete = @"DELETE FROM MinionsVillains
                                WHERE VillainId = @Id";

            using (SqlCommand sqlCommand = new SqlCommand(delete, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Id", villainId);
                //get released minions count before deleting villain
                var minionsCount = GetMinionsCount(sqlConnection, villainId);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine(villainName.Length > 0
                    ? $"{villainName} was deleted."
                    : "No such villain was found.");

                Console.WriteLine($"{minionsCount} minions were released.");

                //Delete villain from villains
                var deleteVillain = @"DELETE FROM Villains
                                        WHERE Name = @villainName";
                using (SqlCommand deleteVillainSqlCommand = new SqlCommand(deleteVillain, sqlConnection))
                {
                    deleteVillainSqlCommand.Parameters.AddWithValue("villainName", villainName);
                    deleteVillainSqlCommand.ExecuteNonQuery();
                }
            }
        }

        private static int GetMinionsCount(SqlConnection sqlConnection, int villainId)
        {
            var getMinionCount = @"SELECT COUNT(*) FROM MinionsVillains
                                   WHERE VillainId = @villainId";
            using (SqlCommand releaseMinionSqlCommand = new SqlCommand(getMinionCount, sqlConnection))
            {
                releaseMinionSqlCommand.Parameters.AddWithValue("villainId", villainId);
                var releasedMinions = (int)releaseMinionSqlCommand.ExecuteScalar();
                return releasedMinions < 0 ? 0 : releasedMinions;
            }
        }

        private static string GetVillainName(SqlConnection sqlConnection, int villainId)
        {
            var villainName = @"SELECT Name FROM Villains 
                                    WHERE Id = @Id";
            using (SqlCommand getNameCommand = new SqlCommand(villainName, sqlConnection))
            {
                getNameCommand.Parameters.AddWithValue($"Id", villainId);
                villainName = (string)getNameCommand.ExecuteScalar();
            }

            return villainName;
        }

        private static int GetVillainId(int input, SqlConnection sqlConnection)
        {
            var select = @"SELECT Id FROM Villains WHERE Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(select, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Id", input);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return input;
                    }
                    return 0;
                }
            }
        }
    }
}