using ArtonitRestApi.Annotation;
using ArtonitRestApi.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                        int row = 0;
                        while (dr.Read())
                        {
                            row++;

                            var instance = (T)Activator.CreateInstance(typeof(T));

                            int i = 0;
                            var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);

                            foreach (var field in fields)
                            {
                                switch (field.FieldType.Name)
                                {
                                    case "String":
                                        {
                                            field.SetValue(instance, dr.GetValue(i).ToString());
                                            break;
                                        }
                                    case "Int32":
                                        {
                                            try
                                            {
                                                field.SetValue(instance, Convert.ToInt32(dr.GetValue(i)));
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggerService.Log<DatabaseService>("Exception", 
                                                    $"{ex.Message} | value = {dr.GetValue(i)}");
                                            }

                                            break;
                                        }
                                    case "DateTime":
                                        {
                                            try
                                            {
                                                field.SetValue(instance, DateTime.Parse(dr.GetValue(i).ToString()));
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggerService.Log<DatabaseService>("Exception",
                                                    $"{ex.Message} | value = {dr.GetValue(i)} | i = {i} row = {row}| field.Name = {field.Name}");
                                            }

                                            break;
                                        }
                                    case "TimeSpan":
                                        {
                                            try
                                            {
                                                field.SetValue(instance, TimeSpan.Parse(dr.GetValue(i).ToString()));
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggerService.Log<DatabaseService>("Exception",
                                                    $"{ex.Message} | value = {dr.GetValue(i)}");
                                            }

                                            break;
                                        }
                                    default:
                                        field.SetValue(instance, dr.GetValue(i).ToString());
                                        break;
                                }

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
                                LoggerService.Log<DatabaseService>("Info", $"{field.FieldType.Name}");

                                switch (field.FieldType.Name)
                                {
                                    case "String":
                                        {
                                            field.SetValue(instance, dr.GetValue(i).ToString());
                                            break;
                                        }
                                    case "Int32":
                                        {
                                            field.SetValue(instance, Convert.ToInt32(dr.GetValue(i)));
                                            break;
                                        }
                                    case "DateTime":
                                        {
                                            field.SetValue(instance, DateTime.Parse(dr.GetValue(i).ToString()));
                                            break;
                                        }
                                    case "TimeSpan":
                                        {
                                            field.SetValue(instance, TimeSpan.Parse(dr.GetValue(i).ToString()));
                                            break;
                                        }
                                    default:
                                        field.SetValue(instance, dr.GetValue(i).ToString());
                                        break;
                                }

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


        public static string ExecuteNonQuery(string query)
        {
            var connectionString = SettingsService.DatabaseConnectionString;

            using (var connection = new FbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var cmd = new FbCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    return "successful";
                }
                catch (Exception ex)
                {
                    LoggerService.Log<DatabaseService>("Exception", $"{ex.Message}");
                    return ex.Message;
                }
            }
        }


        public static string Create<T>(T instance)
        {
            string query = GenerateCreateQuery(instance);

            var connectionString = SettingsService.DatabaseConnectionString;

            using (var connection = new FbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var cmd = new FbCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    return "successful addition";
                }
                catch (Exception ex)
                {
                    LoggerService.Log<DatabaseService>("Exception", $"{ex.Message}");
                    return ex.Message;
                }
            }
        }



        public static string GenerateUpdateQuery<T>(T instance, string condition)
        {
            string query = $"update {typeof(T).Name} set ";

            var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Static |
               BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                var value = field.GetValue(instance);

                if (value != null)
                {
                   var databaseNameAttribute = (DatabaseNameAttribute)
                   Attribute.GetCustomAttribute(field, typeof(DatabaseNameAttribute));

                    if (databaseNameAttribute != null)
                    {
                        var systemWord = (DataBaseSystemWordAttribute)
                            Attribute.GetCustomAttribute(field, typeof(DataBaseSystemWordAttribute));

                        switch (value.GetType().Name)
                        {
                            case "String":
                                {
                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value}"" = ";
                                    else
                                        query += databaseNameAttribute.Value + " = ";

                                    query += $"'{value}', ";
                                    break;
                                }
                            case "Int32":
                                {
                                    var valueInt = Convert.ToInt32(value);
                                    if (valueInt == 0) continue;

                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value}"" = ";
                                    else
                                        query += databaseNameAttribute.Value + " = ";

                                    query += value + ", ";
                                    break;
                                }
                            case "DateTime":
                                {
                                    DateTime parsedDate = DateTime.ParseExact(value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                                    string output = parsedDate.ToString("dd.MM.yyyy");

                                    if (output == "01.01.0001") continue;

                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value}"" = ";
                                    else
                                        query += databaseNameAttribute.Value + " = ";

                                    query += $"'{output}', ";
                                    break;
                                }
                            case "TimeSpan":
                                {
                                    if (value.ToString() == "00:00:00") continue;

                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value}"" = ";
                                    else
                                        query += databaseNameAttribute.Value + " = ";

                                    query += $"'{value}', ";

                                    break;
                                }
                            default:
                                query += value + ", ";
                                break;
                        }
                    }
                }
            }


            query = query.Remove(query.Length - 2);

            query += $" where {condition}";


            return query;
        }


        public static string GenerateCreateQuery<T>(T instance)
        {
            string query = $"insert into {typeof(T).Name} (";

            var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                var databaseNameAttribute = (DatabaseNameAttribute) 
                    Attribute.GetCustomAttribute(field, typeof(DatabaseNameAttribute));
               
                if (databaseNameAttribute != null)
                {
                    var systemWord = (DataBaseSystemWordAttribute) 
                        Attribute.GetCustomAttribute(field, typeof(DataBaseSystemWordAttribute));

                    if(systemWord != null)
                    {
                        query += $@"""{databaseNameAttribute.Value}"", ";
                    }
                    else
                    {
                        query += databaseNameAttribute.Value + ", ";
                    }
                }
            }

            query = query.Remove(query.Length - 2);

            query += ") values (";

            foreach (var field in fields)
            {
                var value = field.GetValue(instance);

                if (value != null)
                {
                    switch (value.GetType().Name)
                    {
                        case "String":
                            {
                                query += $"'{value}', ";
                                break;
                            }
                        case "Int32":
                            {
                                query += value + ", ";
                                break;
                            }
                        case "DateTime":
                            {
                                DateTime parsedDate = DateTime.ParseExact(value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                                string output = parsedDate.ToString("dd.MM.yyyy");
                                query += $"'{output}', ";
                                break;
                            }
                        case "TimeSpan":
                            {
                                query += $"'{value}', ";
                                break;
                            }
                        default:
                            query += value + ", ";
                            break;
                    }
                }
                else
                    query += "null, ";
            }

            query = query.Remove(query.Length - 2);

            query += ");";

            return query;
        }
    }
}
