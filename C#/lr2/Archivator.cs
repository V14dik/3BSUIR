using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Threading;

namespace Lr2WindowsService
{
    static class Archivator
    {
        private static string errorFile = @"C:\PTUIR\3 semestr\ISP\Lr2\error.txt";

        public static void Archive(string fileName, string targetDir)
        {
            Encryptor.encryptFileName = Encryptor.TargetEncryptedFilePath(fileName, targetDir);

            try
            {
                using (var memory = new MemoryStream())
                {
                    using (var zip = new ZipArchive(memory, ZipArchiveMode.Create, true))
                    {
                        var memoryFile = zip.CreateEntry(Path.GetFileName(Encryptor.encryptFileName));

                        FileStream sourceStream = default;

                        while (true)//Catching file locked in other process
                        {           //Until it free from other process
                            try
                            {
                                sourceStream = new FileStream(fileName, FileMode.Open);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                            break;
                        }                    
                        Encryptor.Encrypt(sourceStream, memoryFile);
                        
                    }
                    using (var encryptedFileStream = new FileStream(Path.Combine(targetDir, Path.GetFileNameWithoutExtension(fileName) + ".zip"), FileMode.Create))
                    {
                        memory.Seek(0, SeekOrigin.Begin);
                        memory.CopyTo(encryptedFileStream);
                    }
                }
            }
            catch (Exception e)
            {
                using (var errorStream = new StreamWriter(new FileStream(errorFile, FileMode.OpenOrCreate)))
                {
                    errorStream.Write(e.Message + "\n\n" + e.StackTrace);
                }
                MessageBox.Show("Ошибка: " + e.Message);
            }
        }

        public static void Dearchive(string fileName, string targetDir)
        {
            try
            {
                using (var zip = ZipFile.OpenRead(fileName))
                {
                    var file = zip.Entries[0];

                    Encryptor.decryptFileName = Encryptor.TargetDecryptedFilePath(file.Name, targetDir);

                    using (var targetStream = new FileStream(Encryptor.decryptFileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        Encryptor.Decrypt(targetStream, file);
                    }
                }
 
            }
            catch (Exception e)
            {
                using (var errorStream = new StreamWriter(new FileStream(errorFile, FileMode.OpenOrCreate)))
                {
                    errorStream.Write(e.Message + "\n\n" + e.StackTrace);
                }
            }

            Thread.Sleep(1000);
        }
    }
}
