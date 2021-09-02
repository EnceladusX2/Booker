using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Booker.Services
{
    // TODO: Redo all of this, like all of it.
    public class ConfigService
    {
        private const string ConfigFilename = "config.json";
        private string BasePath;
        private ConfigData config = null;

        public ConfigService()
        {
            BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private ConfigData LoadFromFile()
        {
            var cfg = new ConfigData();
            var path = Path.Combine(BasePath, ConfigFilename);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                cfg = JsonSerializer.Deserialize<ConfigData>(json);
            }
            else
            {
                File.Create(path).Close();
            }

            File.WriteAllText(path, JsonSerializer.Serialize(cfg, new JsonSerializerOptions() { WriteIndented = true }));

            return cfg;
        }

        public ConfigData GetConfig()
        {
            if (config == null)
            {
                this.config = LoadFromFile();
            }

            return this.config;
        }
    }

    public class ConfigData
        {
            [JsonInclude]
            public string Token = "";

            [JsonInclude]
            public string DefaultPrefix = "";
        }
}