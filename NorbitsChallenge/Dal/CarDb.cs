using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }

        public int GetTireCount(int companyId, string licensePlate)
        {
            int result = 0;

            var connectionString = _config.GetConnectionString("DefaultConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT tireCount FROM car WHERE companyId = @CompanyId AND licenseplate = @LicensePlate", connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@LicensePlate", licensePlate);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader.IsDBNull(reader.GetOrdinal("tireCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("tireCount"));
                        }
                    }
                }
            }

            return result;
        }

        public void AddCar(Car car)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand("INSERT INTO car (LicensePlate, Description, Model, Brand, TireCount, CompanyId) VALUES (@LicensePlate, @Description, @Model, @Brand, @TireCount, @CompanyId)", connection))
                {
                    command.Parameters.AddWithValue("@LicensePlate", car.LicensePlate);
                    command.Parameters.AddWithValue("@Description", car.Description);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Brand", car.Brand);
                    command.Parameters.AddWithValue("@TireCount", car.TireCount);
                    command.Parameters.AddWithValue("@CompanyId", car.CompanyId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Car> GetAllCars(int companyId)
        {
            var cars = new List<Car>();

            var connectionString = _config.GetConnectionString("DefaultConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand("SELECT * FROM Car WHERE companyId = @CompanyId", connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var car = new Car
                            {
                                LicensePlate = reader["LicensePlate"].ToString(),
                                Description = reader["Description"].ToString(),
                                Model = reader["Model"].ToString(),
                                Brand = reader["Brand"].ToString(),
                                TireCount = reader.GetInt32(reader.GetOrdinal("TireCount")),
                                CompanyId = companyId
                            };

                            cars.Add(car);  
                        }
                    }
                }
            }

            return cars;
        }

    }
}
