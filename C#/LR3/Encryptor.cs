using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace Lr2WindowsService
{
    static class Encryptor
    {
        internal static string encryptFileName;
        internal static string decryptFileName;

        internal static readonly DESCryptoServiceProvider crypto = new DESCryptoServiceProvider
        {
            Key = Encoding.ASCII.GetBytes("BruhLaba"),
            IV = Encoding.ASCII.GetBytes("BruhLaba")
        };

        internal static void Encrypt(Stream sourceStream, ZipArchiveEntry target)
        {
            using (var encryptionStream = new CryptoStream(target.Open(), crypto.CreateEncryptor(), CryptoStreamMode.Write))
            {
                sourceStream.CopyTo(encryptionStream);
            }
        }

        internal static void Decrypt(Stream targetStream, ZipArchiveEntry file)
        {
            using (var decryptionStream = new CryptoStream(file.Open(), crypto.CreateDecryptor(), CryptoStreamMode.Read))
            {
                decryptionStream.CopyTo(targetStream);
            }
        }

        internal static string TargetEncryptedFilePath(string fileName, string targetDir)
        {
            fileName = fileName.Replace(Path.GetDirectoryName(fileName), targetDir);

            string encryptedFileName = fileName.Replace(Path.GetFileName(fileName), Path.GetFileNameWithoutExtension(fileName) + "_encrypted" + Path.GetExtension(fileName));

            return encryptedFileName;
        }

        internal static string TargetDecryptedFilePath(string fileName, string targetDir)
        {
            fileName = Path.Combine(targetDir, fileName);

            string name = Path.GetFileNameWithoutExtension(fileName);

            name = name.Replace("_encrypted", "_decrypted");

            string encryptedFileName = fileName.Replace(Path.GetFileNameWithoutExtension(fileName), name);

            return encryptedFileName;
        }
    }
}
