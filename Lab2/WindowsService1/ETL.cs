﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Security.Cryptography;

namespace WindowsService1
{
    partial class ETL
    {
        public string TargetDirectory { get; set; }
        public string SourceDirectory { get; set; }

        Tracker tracker;
        Logger logger;

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
            Logger backupLog = new Logger();

            if (TargetDirectory.CompareTo(SourceDirectory) == 0)
            {
                backupLog.Log("ERROR: The source directory cannot be the same as the target directory. Application stopped...");
                this.Stop();
            }

            if (!Directory.Exists(TargetDirectory))
            {
                backupLog.Log("Error: Target directory does not exist. Application stopped...");
                this.Stop();
            }

            if (!Directory.Exists(SourceDirectory))
            {
                backupLog.Log("Error: Source directory does not exist. Application stopped...");
                this.Stop();
            }

            try
            {
                logger = new Logger(Path.Combine(TargetDirectory, "Log.txt"));
                tracker = new Tracker(SourceDirectory);

                tracker.Created += OnDirectoryCreate;
            }
            catch (Exception e)
            {
                backupLog.Log($"Error: {e.Message} Application stopped...");
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
                    byte[] encrypt = Encryptor.Encrypt(File.ReadAllBytes(filePath), aes.Key, aes.IV);
                    File.WriteAllBytes(filePath, encrypt);

                    Archiver.Compress(filePath, compressPath);
                    Archiver.Decompress(compressPath, decompressPath);

                    byte[] decrypt = Encryptor.Decrypt(File.ReadAllBytes(decompressPath), aes.Key, aes.IV);
                    File.WriteAllBytes(decompressPath, decrypt.ToArray());

                }


                File.Delete(compressPath);
                //File.Delete(filePath);

                FileInfo info = new FileInfo(filePath);
                DateTime lwt = info.LastWriteTime;

                string arcPath = Path.Combine(TargetDirectory, $"archive\\{lwt.Year}\\{lwt.ToString("MMMM", CultureInfo.InvariantCulture)}\\{lwt.Day}");


                if (!Directory.Exists(arcPath)) Directory.CreateDirectory(arcPath);
             

                File.Copy(decompressPath, Path.Combine(arcPath,Path.GetFileName(decompressPath)));


                logger.Log(decompressPath + "Moved successfully");
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
