using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _07._Print_All_Minion_Names
{
    public class Program
    {
        public static void Main()
        {
           var minionsInitial = new List<string>();
           var minionsArranged = new List<string>();
            using (SqlConnection sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                sqlConnection.ChangeDatabase(Configuration.Minionsdb);
                var selectName = @"SELECT Name FROM Minions";
                using (SqlCommand sqlCommand = new SqlCommand(selectName, sqlConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            reader.Close();
                            sqlConnection.Close();
                            return;
                        }

                        while (reader.Read())
                        {
                            minionsInitial.Add((string)reader["Name"]);
                        }
                    }
                }
                while (minionsInitial.Count > 0)
                {
                    minionsArranged.Add(minionsInitial[0]);
                    minionsInitial.RemoveAt(0);

                    if (minionsInitial.Count > 0)
                    {
                        minionsArranged.Add(minionsInitial[minionsInitial.Count - 1]);
                        minionsInitial.RemoveAt(minionsInitial.Count - 1);
                    }
                }

                minionsArranged.ForEach(Console.WriteLine);
                sqlConnection.Close();
            }
        }
    }
}