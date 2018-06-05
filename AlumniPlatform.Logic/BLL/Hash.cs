using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AlumniPlatform.Logic.BLL
{
    /// <summary>
    /// 用于密码的hash操作
    /// </summary>
    public class Hash
    {
        /// <summary>
        /// DSecription:用于产生一个随机字符串，与用户输入密码
        /// 一起形成前端加密
        /// </summary>
        /// <returns>返回产生的该字符串</returns>
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            Random m_ran = new Random();
            int size = m_ran.Next(1, 20);
            byte[] m_buff = new byte[20];
            rng.GetBytes(m_buff);
            return Convert.ToBase64String(m_buff);

        }

        /// <summary>
        /// CreateHash 產生雜湊運算後的字串
        /// </summary>
        /// <param name="pwd">為前端使用者在畫面所輸入的密碼</param>
        /// <param name="salt">為CreateSalt() 所產生的加密字串</param>
        /// <returns>返回一个加密后的密码字符串</returns>
        public static string CreateHash(string pwd, string salt)
        {
            string saltPwd = string.Concat(pwd, salt);
            byte[] data = new byte[20];
            data = Encoding.UTF8.GetBytes(saltPwd);
            byte[] result = new byte[20];
            SHA1Managed shaM = new SHA1Managed();
            result = shaM.ComputeHash(data);
            string hashPwd = Convert.ToBase64String(result);
            return hashPwd;
        }
    }
}
