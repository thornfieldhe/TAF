// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Encrypt.cs" company="">
//   
// </copyright>
// <summary>
//   The encrypt.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Utility;

    /// <summary>
    /// The encrypt.
    /// </summary>
    public static class Encrypt
    {
        /// <summary>
        /// 加密字符串长度应该大于8
        /// </summary>
        public const string EncrKey = "P@ssw0rd+";

        /// <summary>
        /// 获取新密码
        /// </summary>
        /// <param name="pwdlen">
        /// 密码长度
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetNewPassword(int pwdlen)
        {
            const string Randomchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var tmpstr = string.Empty;
            var rnd = new Random();
            for (var i = 0; i < pwdlen; i++)
            {
                var iRandNum = rnd.Next(Randomchars.Length);
                tmpstr += Randomchars[iRandNum];
            }

            return tmpstr;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strText">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DesEncrypt(string strText)
        {
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                var byKey = Encoding.UTF8.GetBytes(EncrKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Encoding.UTF8.GetBytes(strText);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strText">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string DesDecrypt(string strText)
        {
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                var byKey = Encoding.UTF8.GetBytes(EncrKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Convert.FromBase64String(strText);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="input">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMD5Hash(string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By16(string text)
        {
            return Md5By16(text, Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <param name="encoding">
        /// 字符编码
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By16(string text, Encoding encoding)
        {
            return Md5(text, encoding, 4, 8);
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="encoding">
        /// The encoding.
        /// </param>
        /// <param name="startIndex">
        /// The start Index.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string Md5(string text, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                result = startIndex == null
                             ? BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)))
                             : BitConverter.ToString(
                                                     md5.ComputeHash(encoding.GetBytes(text)),
                                 startIndex.SafeValue(),
                                 length.SafeValue());
            }
            finally
            {
                md5.Clear();
            }

            return result.Replace("-", string.Empty);
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By32(string text)
        {
            return Md5By32(text, Encoding.UTF8);
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">
        /// 待加密字符串
        /// </param>
        /// <param name="encoding">
        /// 字符编码
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Md5By32(string text, Encoding encoding)
        {
            return Md5(text, encoding, null, null);
        }
    }
}