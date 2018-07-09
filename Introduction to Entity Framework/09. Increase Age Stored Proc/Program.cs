﻿using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace _09._Increase_Age_Stored_Proc
{
    public class Program
    {
        public static void Main()
        {
            string connectionString = Configuration.ConnectionString;

            SqlConnection dbCon = new SqlConnection(connectionString);
            dbCon.Open();

            int id = int.Parse(Console.ReadLine());

            using (dbCon)
            {
                var command = new SqlCommand("EXEC usp_GetOlder @Id", dbCon);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                command = new SqlCommand("SELECT * FROM Minions WHERE Id = @Id", dbCon);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();

                using (reader)
                {
                    reader.Read();

                    Console.WriteLine($"{(string)reader["Name"]} - {(int)reader["Age"]} years old");
                }
            }
        }
    }
}