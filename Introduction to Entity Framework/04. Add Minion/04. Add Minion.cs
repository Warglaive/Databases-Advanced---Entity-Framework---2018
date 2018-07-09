using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _04._Add_Minion
{
    public class Program
    {
        public static void Main()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.ChangeDatabase(Configuration.Minionsdb);
                var minionArgs = Console.ReadLine().Split();

                var minionName = minionArgs[1];
                var minionAge = int.Parse(minionArgs[2]);
                var minionTownName = minionArgs[3];

                var villainArgs = Console.ReadLine().Split();
                var villainName = villainArgs[1];

                if (!IsTownInDb(sqlConnection, minionTownName))
                {
                    AddTownToDb(sqlConnection, minionTownName);
                }
                InsertIntoMinions(sqlConnection, minionName, minionAge, minionTownName);
                if (!IsVillainInDb(sqlConnection, villainName))
                {
                    AddVillainToDb(sqlConnection, villainName);
                }
                SetMinionToVillain(sqlConnection, minionName, villainName);
                sqlConnection.Close();
            }
        }

        private static void SetMinionToVillain(SqlConnection sqlConnection, string minionName, string villainName)
        {
            var insert = @"INSERT INTO MinionsVillains
                        VALUES((SELECT Id FROM Minions
                        	WHERE Name = @MinionName),(SELECT Id FROM Villains 
                        	WHERE Name = @VillainName))";
            using (SqlCommand sqlCommand = new SqlCommand(insert, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("MinionName", minionName);
                sqlCommand.Parameters.AddWithValue("VillainName", villainName);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }

        private static void AddVillainToDb(SqlConnection sqlConnection, string villainName)
        {
            var insert = @"INSERT INTO Villains
                        VALUES
                        (@Name, (SELECT Id 
                                    FROM EvilnessFactors
                        WHERE Name = 'Evil'))";
            using (SqlCommand sqlCommand = new SqlCommand(insert, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Name", villainName);
                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }
            }
        }

        private static bool IsVillainInDb(SqlConnection sqlConnection, string villainName)
        {
            var selectVillain = @"SELECT Name FROM Villains
                                WHERE Name = @Name";
            using (SqlCommand sqlCommand = new SqlCommand(selectVillain, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Name", villainName);
                using (var reader = sqlCommand.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        private static void AddTownToDb(SqlConnection sqlConnection, string minionTownName)
        {
            var insert = @"INSERT INTO Towns(Name)
                            VALUES
                            ('@Name');";
            using (SqlCommand sqlCommand = new SqlCommand(insert, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Name", minionTownName);
                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine($"Town {minionTownName} was added to the database");
                }
            }
        }

        private static void InsertIntoMinions(SqlConnection sqlConnection, string minionName, int minionAge, string minionTownName)
        {
            var insert = @"INSERT INTO Minions
                    VALUES
                    ('@Name', @Age, (SELECT Id FROM Towns
                	WHERE Name = @TownName))";
            using (SqlCommand sqlCommand = new SqlCommand(insert, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Name", minionName);
                sqlCommand.Parameters.AddWithValue("Age", minionAge);
                sqlCommand.Parameters.AddWithValue("TownName", minionTownName);
                sqlCommand.ExecuteNonQuery();
            }
        }

        private static bool IsTownInDb(SqlConnection sqlConnection, string minionTownName)
        {
            var selectTown = @"SELECT Name FROM Towns
                                WHERE Name = @Name";
            using (var sqlCommand = new SqlCommand(selectTown, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("Name", minionTownName);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }
    }
}