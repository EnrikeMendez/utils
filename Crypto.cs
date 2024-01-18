using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace utils
{
    public class Crypto
    {
        private string publickey = string.Format("14012024");
        private string secretkey = string.Format("20240114");
        public string encrypt(string text)
        {
            CryptoStream cs = null;
            MemoryStream ms = null;
            byte[] secretkeyByte = { };
            byte[] publickeyByte = { };
            byte[] inputbyteArray = { };

            try
            {
                secretkeyByte = Encoding.UTF8.GetBytes(secretkey);
                publickeyByte = Encoding.UTF8.GetBytes(publickey);
                inputbyteArray = Encoding.UTF8.GetBytes(text);

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeyByte, secretkeyByte),
                                          CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                ex.Source += string.Format("\n original-value: {0}\n", text);
            }
            finally
            {
                if (cs != null)
                {
                    cs.Dispose();
                    GC.SuppressFinalize(cs);
                }
                if (ms != null)
                {
                    ms.Dispose();
                    GC.SuppressFinalize(ms);
                }
                if (secretkeyByte != null)
                {
                    GC.SuppressFinalize(secretkeyByte);
                }
                if (publickeyByte != null)
                {
                    GC.SuppressFinalize(publickeyByte);
                }
                if (inputbyteArray != null)
                {
                    GC.SuppressFinalize(inputbyteArray);
                }
                GC.Collect();
            }

            return text;
        }
        public string decrypt(string text)
        {
            CryptoStream cs = null;
            MemoryStream ms = null;
            Encoding encoding = null;
            byte[] publickeybyte = { };
            byte[] privatekeyByte = { };
            byte[] inputbyteArray = null;

            try
            {
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                inputbyteArray = new byte[text.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(text.Replace(" ", "+"));

                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte),
                                          CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    encoding = Encoding.UTF8;
                    text = encoding.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                ex.Source += string.Format("\n original-value: {0}\n", text);
            }
            finally
            {
                if (cs != null)
                {
                    cs.Dispose();
                    GC.SuppressFinalize(cs);
                }
                if (ms != null)
                {
                    ms.Dispose();
                    GC.SuppressFinalize(ms);
                }
                if (publickeybyte != null)
                {
                    GC.SuppressFinalize(publickeybyte);
                }
                if (privatekeyByte != null)
                {
                    GC.SuppressFinalize(privatekeyByte);
                }
                if (inputbyteArray != null)
                {
                    GC.SuppressFinalize(inputbyteArray);
                }
                if (encoding != null)
                {
                    GC.SuppressFinalize(encoding);
                }
                GC.Collect();
            }

            return text;
        }
    }
}