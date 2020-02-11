using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AiLibraries.Cryptographies
{
    /// <summary>
    /// ใช้เข้ารหัสทุกสิ่งอัน หลักๆจะมี Rijndael ที่ถอดกลับได้ กะ MD5 ที่เป็น Hash
    /// ส่วน SHA1 ไม่ได้เขียนไว้
    /// </summary>
    public class Crypto
    {
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        /// <summary>
        /// Encrypt ด้วย AES.  สามารถถอดกลับด้วยฟังชั่น DecryptStringAES() โดยใส่คีย์ให้ตรงกัน.
        /// </summary>
        /// <param name="plainText">ตัวที่จะเข้ารหัส</param>
        /// <param name="sharedSecret">คีย์ที่ใช้เข้ารหัส</param>
        public static string EncryptStringAES(string plainText, string sharedSecret)
        {
            if (plainText != "")
            {
                if (string.IsNullOrEmpty(plainText))
                    throw new ArgumentNullException("plainText");
                if (string.IsNullOrEmpty(sharedSecret))
                    throw new ArgumentNullException("sharedSecret");

                string outStr; 
                RijndaelManaged aesAlg = null;

                try
                {
                    var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize/8);

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    //using(X x = y){} หมายถึงจะทำการ dispose object ใน () ทิ้งหลังออกจาก {}
                    using (var msEncrypt = new MemoryStream())
                    {
                        msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof (int));
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
                finally
                {
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                return outStr;
            }
            return "";
        }

        /// <summary>
        /// Decrypt ด้วย AES.  สามารถถอดกลับด้วยฟังชั่น EncryptStringAES() โดยใส่คีย์ให้ตรงกัน.
        /// </summary>
        /// <param name="cipherText">ตัวที่จะเข้ารหัส</param>
        /// <param name="sharedSecret">คีย์ที่ใช้เข้ารหัส</param>
        public static string DecryptStringAES(string cipherText, string sharedSecret)
        {
            if (cipherText != "")
            {
                if (string.IsNullOrEmpty(cipherText))
                    throw new ArgumentNullException("cipherText");
                if (string.IsNullOrEmpty(sharedSecret))
                    throw new ArgumentNullException("sharedSecret");

                RijndaelManaged aesAlg = null;

                string plaintext;

                try
                {
                    var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                    byte[] bytes = Convert.FromBase64String(cipherText);
                    using (var msDecrypt = new MemoryStream(bytes))
                    {
                        aesAlg = new RijndaelManaged();
                        aesAlg.Key = key.GetBytes(aesAlg.KeySize/8);
                        aesAlg.IV = ReadByteArray(msDecrypt);
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                                plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
                finally
                {
                    if (aesAlg != null)
                        aesAlg.Clear();
                }

                return plaintext;
            }
            return "";
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof (int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

        /// <summary>
        /// เช้ารหัส MD5
        /// </summary>
        /// <param name="input">ค่าที่จะเข้ารหัส</param>
        /// <returns>รหัส MD5</returns>
        public static string EnCryptToMD5(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(input);
            bs = md5.ComputeHash(bs);
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}