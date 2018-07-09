using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using _01.InitialSetup;

namespace _08._Increase_Minion_Age
{
    public class Program
    {
        public static void Main()
        {
            var selectedIds = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var connectionString = Configuration.ConnectionString;

            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var minionIds = new List<int>();
            var minionaNames = new List<string>();
            var minionAges = new List<int>();

            using (sqlConnection)
            {
                SqlCommand command =
                    new SqlCommand($"SELECT * FROM Minions WHERE Id IN ({String.Join(", ", selectedIds)})", sqlConnection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        sqlConnection.Close();
                        return;
                    }

                    while (reader.Read())
                    {
                        minionIds.Add((int)reader["Id"]);
                        minionaNames.Add((string)reader["Name"]);
                        minionAges.Add((int)reader["Age"]);
                    }
                }

                for (int i = 0; i < minionIds.Count; i++)
                {
                    int id = minionIds[i];
                    string name = String.Join(" ",
                        minionaNames[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                            .Select(n => n = char.ToUpper(n.First()) + n.Substring(1).ToLower()).ToArray());
                    int age = minionAges[i] + 1;

                    command = new SqlCommand("UPDATE Minions SET Name = @name, Age = @age WHERE Id = @Id", sqlConnection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }

                command = new SqlCommand($"SELECT * FROM Minions", sqlConnection);
                reader = command.ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        sqlConnection.Close();
                        return;
                    }

                    while (reader.Read())
                    {
                        Console.WriteLine($"{(int)reader["Id"]} {(string)reader["Name"]} {(int)reader["Age"]}");
                    }
                }
            }
        }
    }
}