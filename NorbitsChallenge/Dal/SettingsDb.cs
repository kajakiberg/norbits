using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Bll;

namespace NorbitsChallenge.Dal
{
    public class SettingsDb
    {
        private readonly string _connectionString;

        public SettingsDb(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("ConnectionString for SQLite is missing in appsettings.json");
            }
        }

        public string GetCompanyName(int companyId)
        {
            string companyName = "";

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = "SELECT * FROM settings WHERE setting = 'companyname'";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["companyId"].ToString() == companyId.ToString())
                            {
                                companyName = reader["settingValue"].ToString();
                            }
                        }
                    }
                }
            }

            return companyName;
        }

        public List<Setting> GetSettings(int companyId)
        {
            var settings = new List<Setting>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"SELECT * FROM settings WHERE companyId = {companyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var setting = new Setting
                            {
                                Key = reader["setting"].ToString(),
                                Value = reader["settingValue"].ToString(),
                                CompanyId = companyId
                            };

                            settings.Add(setting);
                        }
                    }
                }
            }

            return settings;
        }

        public void UpdateSetting(Setting setting, int companyId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"UPDATE settings SET settingValue = @SettingValue WHERE setting = @Setting";
                    command.Parameters.AddWithValue("@SettingValue", setting.Value);
                    command.Parameters.AddWithValue("@Setting", setting.Key);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
