using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ArtonitRestApi.Services
{
    public class DatabaseService
    {
        public static List<T> GetList<T>(string query)
        {
            var rows = new List<T>();

            var connectionString = SettingsService.DatabaseConnectionString;

            using (var connection = new FbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var cmd = new FbCommand(query, connection);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var instance = (T)Activator.CreateInstance(typeof(T));

                            int i = 0;
                            var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);

                            foreach (var field in fields)
                            {
                                field.SetValue(instance, dr.GetValue(i).ToString());
                                i++;
                            }

                            rows.Add(instance);
                        }
                    }

                    return rows;
                }
                catch (Exception ex)
                {
                    LoggerService.Log<DatabaseService>("Exception", $"{ex.Message}");
                }
            }

            return rows;
        }


        public static T Get<T>(string query)
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var connectionString = SettingsService.DatabaseConnectionString;

            using (var connection = new FbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var cmd = new FbCommand(query, connection);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int i = 0;
                            var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);

                            foreach (var field in fields)
                            {
                                field.SetValue(instance, dr.GetValue(i).ToString());
                                i++;
                            }

                            return instance;
                        }

                        LoggerService.Log<DatabaseService>("Info", $"Результат запроса пустой");
                    }
                }
                catch (Exception ex)
                {
                    LoggerService.Log<DatabaseService>("Exception", $"{ex.Message}");
                }
            }

            return instance;
        }
    }
}
