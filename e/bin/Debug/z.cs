using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace xRan
{
    class z
    {
        public static void Main()//need to change to run alone
        {
            string m = "df21326011359da62cc35853a4bb80d18762808bc90724cc14c79f1a718f4af3";
            string n = cp(22);
            Directory.CreateDirectory("xRan");
            string path = "xRan/flag.txt.xRan";

            z z = new z();
            byte [] w = z.encFunc(Encoding.UTF8.GetBytes(m), SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(n)));
            Console.WriteLine("enc length -- " + w.Length);
            try
            {
                File.WriteAllBytes(path, w);

            }
            catch (IOException e)
            {
                Console.Write("Cannot write to file - ", e.Message);
            }           
            return;
        }

        public void encFl(string file, string password)
        {
            byte[] bytesToBeEncrypted;
            try
            {
                bytesToBeEncrypted = File.ReadAllBytes(file);
            }
            catch (IOException e)
            {
                Console.Write("Cannot open file for writing: File used by another process");
                return;
            }
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesEncrypted = encFunc(bytesToBeEncrypted, passwordBytes);
            try
            {
                File.WriteAllBytes(file, bytesEncrypted);

            }
            catch (IOException e)
            {
                Console.Write("Cannot write to file");
            }
        }

        public byte[] encFunc(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        public string cp(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
