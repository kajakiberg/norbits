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

                // Opprett tabellen "Car" hvis den ikke finnes
                var createCarTable = @"
                    CREATE TABLE IF NOT EXISTS Car (
                        LicensePlate VARCHAR(10) NOT NULL PRIMARY KEY,
                        Description VARCHAR(50),
                        Model VARCHAR(50),
                        Brand VARCHAR(50),
                        TireCount INT,
                        CompanyId INT
                    );";

                // Opprett tabellen "Settings" hvis den ikke finnes
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

        // Metode for å legge til en bil i databasen
        public void AddCar(string licensePlate, string description, string model, string brand, int tireCount, int companyId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var insertCar = @"
                    INSERT INTO Car (LicensePlate, Description, Model, Brand, TireCount, CompanyId)
                    VALUES (@LicensePlate, @Description, @Model, @Brand, @TireCount, @CompanyId);";

                using (var command = new SqliteCommand(insertCar, connection))
                {
                    command.Parameters.AddWithValue("@LicensePlate", licensePlate);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@Brand", brand);
                    command.Parameters.AddWithValue("@TireCount", tireCount);
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
