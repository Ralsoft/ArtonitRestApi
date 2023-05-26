using ArtonitRestApi.Annotation;
using ArtonitRestApi.Models;
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
                            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);

                            foreach (var property in properties)
                            {
                                switch (property.PropertyType.Name)
                                {
                                    case "String":
                                        {
                                            property.SetValue(instance, dr.GetValue(i).ToString());
                                            break;
                                        }
                                    case "Int32":
                                        {
                                            try
                                            {
                                                property.SetValue(instance, Convert.ToInt32(dr.GetValue(i)));
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
                                                property.SetValue(instance, DateTime.Parse(dr.GetValue(i).ToString()));
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggerService.Log<DatabaseService>("Exception",
                                                    $"{ex.Message} | value = {dr.GetValue(i)} | i = {i} row = {row}| field.Name = {property.Name}");
                                            }

                                            break;
                                        }
                                    case "TimeSpan":
                                        {
                                            try
                                            {
                                                property.SetValue(instance, TimeSpan.Parse(dr.GetValue(i).ToString()));
                                            }
                                            catch (Exception ex)
                                            {
                                                LoggerService.Log<DatabaseService>("Exception",
                                                    $"{ex.Message} | value = {dr.GetValue(i)}");
                                            }

                                            break;
                                        }
                                    default:
                                        property.SetValue(instance, dr.GetValue(i).ToString());
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
                            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);

                            foreach (var property in properties)
                            {
                                switch (property.PropertyType.Name)
                                {
                                    case "String":
                                        {
                                            property.SetValue(instance, dr.GetValue(i).ToString());
                                            break;
                                        }
                                    case "Int32":
                                        {
                                            property.SetValue(instance, Convert.ToInt32(dr.GetValue(i)));
                                            break;
                                        }
                                    case "DateTime":
                                        {
                                            property.SetValue(instance, DateTime.Parse(dr.GetValue(i).ToString()));
                                            break;
                                        }
                                    case "TimeSpan":
                                        {
                                            property.SetValue(instance, TimeSpan.Parse(dr.GetValue(i).ToString()));
                                            break;
                                        }
                                    default:
                                        property.SetValue(instance, dr.GetValue(i).ToString());
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

                    return "ok";
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

                    return "ok";
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
            var classNameAttribute = (DatabaseNameAttribute)
                        Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseNameAttribute));
            string query;

            if(classNameAttribute != null)
                query = $"update {classNameAttribute.Value} set ";
            else
                query = $"update {typeof(T).Name} set ";

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Static |
               BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var property in properties)
            {
                var value = property.GetValue(instance);

                if (value != null)
                {
                   var databaseNameAttribute = (DatabaseNameAttribute)
                        Attribute.GetCustomAttribute(property, typeof(DatabaseNameAttribute));

                    if (databaseNameAttribute != null)
                    {
                        var systemWord = (DataBaseSystemWordAttribute)
                            Attribute.GetCustomAttribute(property, typeof(DataBaseSystemWordAttribute));

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

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var property in properties)
            {
                var databasePrimaryKeyAttribute = (DatabasePrimaryKeyAttribute)
                    Attribute.GetCustomAttribute(property, typeof(DatabasePrimaryKeyAttribute));

                if (databasePrimaryKeyAttribute != null) continue;

                var databaseNameAttribute = (DatabaseNameAttribute) 
                    Attribute.GetCustomAttribute(property, typeof(DatabaseNameAttribute));
               
                if (databaseNameAttribute != null)
                {
                    var systemWord = (DataBaseSystemWordAttribute) 
                        Attribute.GetCustomAttribute(property, typeof(DataBaseSystemWordAttribute));

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

            foreach (var property in properties)
            {
                var databasePrimaryKeyAttribute = (DatabasePrimaryKeyAttribute)
                   Attribute.GetCustomAttribute(property, typeof(DatabasePrimaryKeyAttribute));

                if (databasePrimaryKeyAttribute != null) continue;

                var value = property.GetValue(instance);

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
