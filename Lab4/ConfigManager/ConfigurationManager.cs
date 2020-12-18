using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace ConfigurationManager
{
    public class ConfigurationManager
    {
        public Options LoadedOptionsSection { get; private set; } = null;

        public enum ConfigType
        {
            XML,
            JSON
        }

        public ConfigurationManager()
        {
            
        }

        public void LoadOptions<T>(string OptionsDirectory) where T : Options, new()
        {
            if (File.Exists($"{OptionsDirectory}\\config.xml")) LoadXML<T>($"{OptionsDirectory}\\config.xml");
            else if (File.Exists($"{OptionsDirectory}\\appsettings.json")) LoadJSON<T>($"{OptionsDirectory}\\appsettings.json").GetAwaiter().GetResult();
            else LoadedOptionsSection = new T();
        }

        public void SaveOptions<T>(T options,string path,ConfigType configType = ConfigType.XML) where T: Options
        {
            if (configType == ConfigType.XML)
            {
                WriteXML<T>(path, options);
            }
            else if (configType == ConfigType.JSON)
            {
                WriteJSON<T>(path, options).GetAwaiter().GetResult();
            }
        }

        private async Task LoadJSON<T>(string path) where T : Options
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                LoadedOptionsSection = await JsonSerializer.DeserializeAsync<T>(fs);
            }
        }

        private void LoadXML<T>(string path) where T: Options
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                LoadedOptionsSection = (T)formatter.Deserialize(fs);
            }
        }

        private async Task WriteJSON<T>(string path,T options)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, options);
            }
        }

        private void WriteXML<T>(string path, T options)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, options);
            }
        }

        public Options GetOptions<T>() where T : Options
        {
            if (typeof(T).Name.Equals(typeof(T).Name)) return LoadedOptionsSection;
            return LoadedOptionsSection.GetType().GetProperty(typeof(T).Name).GetValue(LoadedOptionsSection) as Options;
        }

    }
}
