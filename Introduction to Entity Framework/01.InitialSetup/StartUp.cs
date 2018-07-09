using System;
using System.Data.SqlClient;

namespace _01.InitialSetup
{
    public class StartUp
    {
        public static void Main()
        {
            var sqlConnection = new SqlConnection(Configuration.ConnectionString);
            using (sqlConnection)
            {
                sqlConnection.Open();

                var createDB = "CREATE DATABASE MinionsDB";
                ExecuteNoNQueries(sqlConnection, createDB);

                sqlConnection.ChangeDatabase("MinionsDB");

                var createTableCountries = @"CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";
                ExecuteNoNQueries(sqlConnection, createTableCountries);

                var createTableTowns = @"CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))";
                ExecuteNoNQueries(sqlConnection, createTableTowns);

                var createTableMinions = @"CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))";
                ExecuteNoNQueries(sqlConnection, createTableMinions);

                var createTableEvilnessFactors = @"CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))";
                ExecuteNoNQueries(sqlConnection, createTableEvilnessFactors);

                var createTableVillains = @"CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))";
                ExecuteNoNQueries(sqlConnection, createTableVillains);

                var createTableMinionsVillains = @"CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id), CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId))";
                ExecuteNoNQueries(sqlConnection, createTableMinionsVillains);
                //fill in data
                var insertIntoCountries = @"INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')";
                ExecuteNoNQueries(sqlConnection, insertIntoCountries);
                var insertIntoTowns = @"INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";
                ExecuteNoNQueries(sqlConnection, insertIntoTowns);
                var insertIntoMinions = @"INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)";
                ExecuteNoNQueries(sqlConnection, insertIntoMinions);
                var insertIntoEvilnessFactors = @"INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";
                ExecuteNoNQueries(sqlConnection, insertIntoEvilnessFactors);
                var insertIntoVillains = @"INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)";
                ExecuteNoNQueries(sqlConnection, insertIntoVillains);
                var insertIntoMinionsVillains = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";
                ExecuteNoNQueries(sqlConnection, insertIntoMinionsVillains);

                sqlConnection.Close();
            }
        }

        private static void ExecuteNoNQueries(SqlConnection sqlConnection, string command)
        {
            using (var sqlCommand = new SqlCommand(command, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}