using Newtonsoft.Json;
using System.IO;

namespace ArtonitRestApi.Services
{
    public class SettingsService
    {
        private static string fileName = "appsettings.json";

        public int _port
        {
            get { return Port; }
            set { Port = value; }
        }

        public static int Port = 8000;


        public string _databaseConnectionString
        {
            get { return DatabaseConnectionString; }
            set { DatabaseConnectionString = value; }
        }

        public static string DatabaseConnectionString = "User = SYSDBA; Password = temp; " +
            "Database = C:\\Program Files (x86)\\Cardsoft\\DuoSE\\Access\\ShieldPro_rest.gdb; " +
            "DataSource = 127.0.0.1; Port = 3050; Dialect = 3; Charset = win1251; Role =; " +
            "Connection lifetime = 15; Pooling = true; MinPoolSize = 0; MaxPoolSize = 50; " +
            "Packet Size = 8192; ServerType = 0;";


        public static void Update()
        {
            if (!File.Exists($@"{Service1.MainPath}\{fileName}"))
            {
                string json = JsonConvert.SerializeObject(new SettingsService());
                File.WriteAllText($@"{Service1.MainPath}\{fileName}", json);
            }
            else
            {
                var json = File.ReadAllText($@"{Service1.MainPath}\{fileName}");
                var settings = JsonConvert.DeserializeObject<SettingsService>(json);

                Port = settings._port;
                DatabaseConnectionString = settings._databaseConnectionString;
            }
        }
    }
}
