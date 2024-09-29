using SaraswatiConstruction.Domain.Constant;
using System.Security.Cryptography;
using System.Text;

namespace SaraswatiConstruction.Utility
{
    public static class CommonFunctions
    {
        private const string SecretKey = CommonConstants.SecretKey;
        private const string IV = CommonConstants.IV;

        /// <summary>
        /// this will encrypt string value
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Encrypt(string? token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                {
                    using var aes = Aes.Create();
                    aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                    aes.IV = Encoding.UTF8.GetBytes(IV);

                    using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using var ms = new MemoryStream();
                    using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(token);
                    }
                    var encryptedBytes = ms.ToArray();
                    var encryptedToken = Convert.ToBase64String(encryptedBytes).Replace(CommonConstants.Plus, CommonConstants.Minus).Replace(CommonConstants.Slash, CommonConstants.Underscore);
                    return encryptedToken;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// this will decrypt encrypted value.
        /// </summary>
        /// <param name="encryptedToken"></param>
        /// <returns></returns>
        public static string Decrypt(string? encryptedToken)
        {
            try
            {
                if (!String.IsNullOrEmpty(encryptedToken))
                {
                    var encryptedBytes = encryptedToken.Replace(CommonConstants.Minus, CommonConstants.Plus).Replace(CommonConstants.Underscore, CommonConstants.Slash);
                    using var aes = Aes.Create();
                    aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                    aes.IV = Encoding.UTF8.GetBytes(IV);

                    using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using var ms = new MemoryStream(Convert.FromBase64String(encryptedBytes));
                    using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                    using var sr = new StreamReader(cs);
                    var decryptedToken = sr.ReadToEnd();

                    return decryptedToken;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// This will encrypt Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string? password)
        {
            try
            {
                if (!String.IsNullOrEmpty(password))
                {
                    byte[] iv = new byte[16];
                    byte[] array;

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                        aes.IV = iv;

                        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                                {
                                    streamWriter.Write(password);
                                }

                                array = memoryStream.ToArray();
                            }
                        }
                    }
                    var encryptedPassword = Convert.ToBase64String(array);
                    return encryptedPassword;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// This will dencrypt Password
        /// </summary>
        /// <param name="encryptedPassword"></param>
        /// <returns></returns>
        public static string DecryptPassword(string? encryptedPassword)
        {
            try
            {
                if (!String.IsNullOrEmpty(encryptedPassword))
                {
                    byte[] iv = new byte[16];
                    byte[] buffer = Convert.FromBase64String(encryptedPassword);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = Encoding.UTF8.GetBytes(SecretKey);
                        aes.IV = iv;
                        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                        using (MemoryStream memoryStream = new MemoryStream(buffer))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                                {
                                    var decryptedPassword = streamReader.ReadToEnd();
                                    return decryptedPassword;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GenerateToken(string? Id)
        {
            var token = Convert.ToString(Guid.NewGuid());
            var expirationDate = Convert.ToString(DateTime.UtcNow.AddMinutes(5));
            var tokenString = $"{Id}:{token}:{expirationDate}";
            var encryptedToken = CommonFunctions.Encrypt(tokenString);
            return encryptedToken;
        }
    }
}
