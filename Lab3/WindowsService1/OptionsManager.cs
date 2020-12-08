using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;

namespace WindowsService1
{
    class OptionsManager
    {
        private EtlOptions loadedOptions;

        public OptionsManager(string path)
        {
            /*
            loadedOptions.archiverOptions = new ArchiverOptions();
            loadedOptions.loggerOptions = new LoggerOptions() { LogPath = "C:\\Users\\Anton\\Desktop\\TargetDirectory\\Log.txt" };
            loadedOptions.TargetDirectory = "C:\\Users\\Anton\\Desktop\\TargetDirectory";
            loadedOptions.trackerOptions = new TrackerOptions() { Path = "C:\\Users\\Anton\\Desktop\\SourceDirectory" };

            WriteXML($"{path}\\config.xml");
            WriteJSON($"{path}\\appsettings.json").GetAwaiter().GetResult();
            */

            if (File.Exists($"{path}\\config.xml")) LoadXML($"{path}\\config.xml");
            else if (File.Exists($"{path}\\appsettings.json")) LoadJSON($"{path}\\appsettings.json").GetAwaiter().GetResult();
            else
            {
                loadedOptions = new EtlOptions();

                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\TargetDir");
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\SourceDir");

                loadedOptions.archiverOptions = new ArchiverOptions();
                loadedOptions.loggerOptions = new LoggerOptions() { LogPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\ETL_logs\\Log.txt" };
                loadedOptions.TargetDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\TargetDir";
                loadedOptions.trackerOptions = new TrackerOptions() { Path = $"{AppDomain.CurrentDomain.BaseDirectory}\\SourceDir" };
                loadedOptions.encryptorOptions = new EncryptorOptions();

                WriteXML($"{AppDomain.CurrentDomain.BaseDirectory}\\config.xml");
            }

        }

        private async Task LoadJSON(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                loadedOptions = await JsonSerializer.DeserializeAsync<EtlOptions>(fs);
            }
        }

        private void LoadXML(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(EtlOptions));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                loadedOptions = (EtlOptions)formatter.Deserialize(fs);
            }
        }

        private async Task WriteJSON(string path)
        {   
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, loadedOptions);
            }
        }

        private void WriteXML(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(EtlOptions));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, loadedOptions);
            }
        }

        public Options GetOptions<T>()
        {
            if (typeof(T).Name.Equals(typeof(EtlOptions).Name)) return loadedOptions;
            return loadedOptions.GetType().GetProperty(typeof(T).Name).GetValue(loadedOptions) as Options;
        }

    }
}
