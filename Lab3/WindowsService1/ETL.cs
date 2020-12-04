using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;

namespace WindowsService1
{
    partial class ETL
    {
        public string TargetDirectory { get; set; } = "C:\\Users\\Anton\\Desktop\\TargetDirectory";
        public string SourceDirectory { get; set; } = "C:\\Users\\Anton\\Desktop\\SourceDirectory";

        Tracker tracker;
        Logger logger;

        OptionsManager optionsManager;
        EtlOptions etlOptions;


        protected override void OnStart(string[] args) 
        {
            try
            {
                logger.Log("ETL service started...");
                tracker.Start();
            }
            catch (Exception e)
            {
                logger.Log($"Error :{e}");
            }
        }

        protected override void OnStop()
        {
            tracker.Stop();
        }

        private void Init()
        {
            Logger backupLogger = new Logger();

            try
            {
                optionsManager = new OptionsManager(AppDomain.CurrentDomain.BaseDirectory);
                etlOptions = optionsManager.GetOptions<EtlOptions>() as EtlOptions;

                SourceDirectory = etlOptions.trackerOptions.Path;
                TargetDirectory = etlOptions.TargetDirectory;
            }
            catch (Exception e)
            {
                backupLogger.Log($"Error: {e.Message} Application stopped...");
                Stop();
            }


            if (TargetDirectory.CompareTo(SourceDirectory) == 0)
            {
                backupLogger.Log("ERROR: The source directory cannot be the same as the target directory. Application stopped...");
                this.Stop();
            }

            if (!Directory.Exists(TargetDirectory))
            {
                backupLogger.Log("Error: Target directory does not exist. Application stopped...");
                this.Stop();
            }

            if (!Directory.Exists(SourceDirectory))
            {
                backupLogger.Log("Error: Source directory does not exist. Application stopped...");
                this.Stop();
            }

            try
            {
                logger = new Logger(etlOptions.loggerOptions.LogPath);
                logger.Enabled = etlOptions.loggerOptions.EnableLogging;

                tracker = new Tracker(etlOptions.trackerOptions.Path);

                tracker.Created += OnDirectoryCreate;
            }
            catch (Exception e)
            {
                backupLogger.Log($"Error: {e.Message} Application stopped...");
                Stop();
            }

        }


        private void Move(string filePath)
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath);
                var name = Path.GetFileNameWithoutExtension(filePath);

                var compressPath = TargetDirectory + "\\" + name + ".gz";
                var decompressPath = TargetDirectory + "\\" + name + ".txt";

                Thread.Sleep(100); //?? System.IO.IOException: Процесс не может получить доступ к файлу


                using (Aes aes = Aes.Create())
                {
                    if (etlOptions.encryptorOptions.EnableEncryption)
                    {
                        byte[] encrypt = Encryptor.Encrypt(File.ReadAllBytes(filePath), aes.Key, aes.IV);
                        File.WriteAllBytes(filePath, encrypt);
                    }

                    Archiver.Compress(filePath, compressPath, etlOptions.archiverOptions.compressionLevel);
                    Archiver.Decompress(compressPath, decompressPath);

                    if (etlOptions.encryptorOptions.EnableEncryption)
                    {
                        byte[] decrypt = Encryptor.Decrypt(File.ReadAllBytes(decompressPath), aes.Key, aes.IV);
                        File.WriteAllBytes(decompressPath, decrypt.ToArray());
                    }
                }


                File.Delete(compressPath);


                FileInfo info = new FileInfo(filePath);
                DateTime lwt = info.LastWriteTime;

                string arcPath = Path.Combine(TargetDirectory, $"archive\\{lwt.Year}\\{lwt.ToString("MMMM", CultureInfo.InvariantCulture)}\\{lwt.Day}");


                if (!Directory.Exists(arcPath)) Directory.CreateDirectory(arcPath);
             

                File.Copy(decompressPath, Path.Combine(arcPath,Path.GetFileName(decompressPath)));


                logger.Log(decompressPath + " Moved successfully");
            }

            catch (Exception e)
            {
                logger.Log($"Move error: {e}");
            }
        }
        
        private void OnDirectoryCreate(Object sender, FileSystemEventArgs e)
        {
            Move(e.FullPath);
        }
    }
}
