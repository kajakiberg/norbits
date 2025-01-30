using System;
using Microsoft.Data.Sqlite;

namespace NorbitsChallenge
{
    public class DatabaseService
    {
        private readonly string connectionString = "Data Source=database.db";

        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createCarTable = @"
                    CREATE TABLE IF NOT EXISTS Car (
                        LicensePlate VARCHAR(10) NOT NULL PRIMARY KEY,
                        Description VARCHAR(50),
                        Model VARCHAR(50),
                        Brand VARCHAR(50),
                        TireCount INT,
                        CompanyId INT
                    );";

                var createSettingsTable = @"
                    CREATE TABLE IF NOT EXISTS Settings (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        companyId INT,
                        setting VARCHAR(50),
                        settingValue VARCHAR(50)
                    );";

                using (var command = new SqliteCommand(createCarTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqliteCommand(createSettingsTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
