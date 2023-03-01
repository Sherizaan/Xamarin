using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Spider_App.Utils
{
    class Encrypt : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string encryptedpassword;
        private string decryptedpassword;
        public string EncryptedPassword
        {
            get { return encryptedpassword; }
            set { encryptedpassword = ReturnEncryptedString(value); OnPropertyChanged(nameof(EncryptedPassword)); }
        }
        public string DecryptedPassword
        {
            get { return decryptedpassword; }
            set { decryptedpassword = ReturnDecryptedString(value); OnPropertyChanged(nameof(DecryptedPassword)); }
        }

        static string ReturnEncryptedString(string text)
        {
            string encryptedstring;
            using (Aes aes = Aes.Create())
            {
                byte[] encrypted = EncryptString(text, aes.Key, aes.IV);
                //encryptedstring = Encoding.UTF8.GetString(encrypted);
                encryptedstring = Convert.ToBase64String(encrypted);
                //encryptedstring = DecryptStringFromBytes(encrypted, aes.Key, aes.IV);
            }
            return encryptedstring;
        }
        static string ReturnDecryptedString(string text)
        {
            string decryptedstring;
            byte[] encryptedarray = Convert.FromBase64String(text);
            using (Aes aes = Aes.Create())
            {
                decryptedstring = DecryptStringFromBytes(encryptedarray, aes.Key, aes.IV);
            }
            return decryptedstring;
        }
        static byte[] EncryptString(string string2encrpt, byte[] Key, byte[] IV)
        {
            byte[] encryption;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(string2encrpt);
                        }
                        encryption = ms.ToArray();
                    }
                }
            }
            return encryption;
        }
        static string DecryptStringFromBytes(byte[] encryptedstring, byte[] Key, byte[] IV)
        {
            string returnstring;
            using (Aes aes_algorithm = Aes.Create())
            {
                aes_algorithm.Key = Key;
                aes_algorithm.IV = IV;

                ICryptoTransform decryptor = aes_algorithm.CreateDecryptor(aes_algorithm.Key, aes_algorithm.IV);

                using (MemoryStream ms = new MemoryStream(encryptedstring))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            returnstring = sr.ReadToEnd();
                        }
                    }
                }
            }
            return returnstring;
        }
    }
}
