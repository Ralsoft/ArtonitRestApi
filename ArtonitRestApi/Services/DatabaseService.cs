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
            query = query.ToUpper();

            LoggerService.Log<DatabaseService>("Info", query);
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
                            var instance = (T) Activator.CreateInstance(typeof(T));

                            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);//СВОЙСТВА МОДЕЛИ

                            foreach (var property in properties)
                            {
                                var databaseNameAttribute = (DatabaseNameAttribute)
                                    Attribute.GetCustomAttribute(property, typeof(DatabaseNameAttribute));

                                if (databaseNameAttribute == null) continue;

                                LoggerService.Log<DatabaseService>("INFO", $"{property.PropertyType.Name} | V: {dr[databaseNameAttribute.Value.ToUpper()].ToString()}");
                                try
                                {
                                    switch (property.PropertyType.Name)
                                    {
                                        case "String":
                                            {
                                                property.SetValue(instance, dr[databaseNameAttribute.Value.ToUpper()].ToString());
                                                break;
                                            }
                                        case "Int32":
                                            {
                                                property.SetValue(instance,
                                                        Convert.ToInt32(dr[databaseNameAttribute.Value.ToUpper()]));
                                                break;
                                            }
                                        case "DateTime":
                                            {
                                                property.SetValue(instance,
                                                       DateTime.Parse(dr[databaseNameAttribute.Value.ToUpper()].ToString()));

                                                break;
                                            }
                                        case "TimeSpan":
                                            {
                                                property.SetValue(instance,
                                                        TimeSpan.Parse(dr[databaseNameAttribute.Value.ToUpper()].ToString()));

                                                break;
                                            }
                                        default:

                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LoggerService.Log<DatabaseService>("Exception", ex.Message);
                                }
                                
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
            query = query.ToUpper();

            LoggerService.Log<DatabaseService>("Info", query);
            var instance = (T) Activator.CreateInstance(typeof(T));

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
                            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.NonPublic | BindingFlags.Public);

                            foreach (var property in properties)
                            {
                                var databaseNameAttribute = (DatabaseNameAttribute)
                                    Attribute.GetCustomAttribute(property, typeof(DatabaseNameAttribute));

                                if (databaseNameAttribute == null) continue;

                                try
                                {
                                    switch (property.PropertyType.Name)
                                    {
                                        case "String":
                                            {
                                                property.SetValue(instance,
                                                    dr[databaseNameAttribute.Value.ToUpper()].ToString());
                                                break;
                                            }
                                        case "Int32":
                                            {
                                                property.SetValue(instance,
                                                    Convert.ToInt32(dr[databaseNameAttribute.Value.ToUpper()]));
                                                break;
                                            }
                                        case "DateTime":
                                            {
                                                property.SetValue(instance,
                                                    DateTime.Parse(dr[databaseNameAttribute.Value.ToUpper()].ToString()));
                                                break;
                                            }
                                        case "TimeSpan":
                                            {
                                                property.SetValue(instance,
                                                    TimeSpan.Parse(dr[databaseNameAttribute.Value.ToUpper()].ToString()));
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    LoggerService.Log<DatabaseService>("Exception", ex.Message);
                                }
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
            query = query.ToUpper();

            LoggerService.Log<DatabaseService>("Info", query);

            var connectionString = SettingsService.DatabaseConnectionString;

            using (var connection = new FbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var cmd = new FbCommand(query, connection);
                    var result = cmd.ExecuteNonQuery();

                    return result.ToString();
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
            LoggerService.Log<DatabaseService>("DEBUG", query);

            return ExecuteNonQuery(query);
        }




        public static string Update<T>(T instance, string condition)
        {
            string query = GenerateUpdateQuery(instance, condition);

            return ExecuteNonQuery(query);
        }


        public static string GenerateUpdateQuery<T>(T instance, string condition)
        {
            string query;

            var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseNameAttribute));

            if (attribute is DatabaseNameAttribute databaseName)
            {
                query = $"update {databaseName.Value} set ";
            }
            else
            {
                query = $"update {typeof(T).Name} set ";
            }

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
                                        query += $@"""{databaseNameAttribute.Value.ToUpper()}"" = ";
                                    else
                                        query += databaseNameAttribute.Value.ToUpper() + " = ";

                                    query += $"'{value}', ";
                                    break;
                                }
                            case "Int32":
                                {
                                    var valueInt = Convert.ToInt32(value);
                                   
                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value.ToUpper()}"" = ";
                                    else
                                        query += databaseNameAttribute.Value.ToUpper() + " = ";

                                    query += value + ", ";
                                    break;
                                }
                            case "DateTime":
                                {
                                    DateTime parsedDate = DateTime.ParseExact(value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                                    string output = parsedDate.ToString("dd.MM.yyyy");

                                    if (output == "01.01.0001") continue;

                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value.ToUpper()}"" = ";
                                    else
                                        query += databaseNameAttribute.Value.ToUpper() + " = ";

                                    query += $"'{output}', ";
                                    break;
                                }
                            case "TimeSpan":
                                {
                                    if (value.ToString() == "00:00:00") continue;

                                    if (systemWord != null)
                                        query += $@"""{databaseNameAttribute.Value.ToUpper()}"" = ";
                                    else
                                        query += databaseNameAttribute.Value.ToUpper() + " = ";

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
            string query;

            var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(DatabaseNameAttribute));
            if (attribute is DatabaseNameAttribute databaseName)
            {
                query = $"insert into {databaseName.Value} (";
            }
            else
            {
                query = $"insert into {typeof(T).Name} (";
            }

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
                        query += $@"""{databaseNameAttribute.Value.ToUpper()}"", ";
                    }
                    else
                    {
                        query += databaseNameAttribute.Value.ToUpper() + ", ";
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

            LoggerService.Log<DatabaseService>("INFO", query);
            return query;
        }
    }
}
