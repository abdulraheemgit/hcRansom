using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace e
{
    class Program
    {
        const int CHAR_FS = 28;

        public static byte[] EncryptMessage(byte[] text, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 256;
            aes.Padding = PaddingMode.Zeros;
            aes.Mode = CipherMode.CBC;

            aes.Key = Encoding.Default.GetBytes(key);
            aes.GenerateIV();

            string IV = Encoding.Default.GetString(aes.IV);

            ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] buffer = text;

            return Encoding.Default.GetBytes(Encoding.Default.GetString(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length)) + IV);
        }
        static void appendContents(String fileName)
        {
            FileStream fstream = new FileStream(fileName, FileMode.Append);
            fstream.WriteByte(CHAR_FS);
            fstream.WriteByte(CHAR_FS);
            fstream.WriteByte(CHAR_FS);


            string[] sourceFiles = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.cs", SearchOption.AllDirectories);


            for (int i = 0; i < sourceFiles.Length; i++)
            {
                byte[] buffer = File.ReadAllBytes(sourceFiles[i]);


                // removing UTF8's byte order mark...
                if (buffer.Length > 2 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0XBF)
                {
                    byte[] newBuffer = new byte[buffer.Length - 3];
                    Array.Copy(buffer, 3, newBuffer, 0, buffer.Length - 3);

                    newBuffer = EncryptMessage(newBuffer, "RkBoNZ42gBQwdhiyQ5y0GVIDY7FKEcVb");

                    fstream.Write(newBuffer, 0, newBuffer.Length);
                }
                else
                {
                    buffer = EncryptMessage(buffer, "RkBoNZ42gBQwdhiyQ5y0GVIDY7FKEcVb");

                    fstream.Write(buffer, 0, buffer.Length);
                }

                fstream.WriteByte(CHAR_FS);
                fstream.WriteByte(CHAR_FS);
                fstream.WriteByte(CHAR_FS);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                appendContents(args[0]);
                return;
            }
        }
    }
}
