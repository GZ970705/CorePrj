using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PrjWebJWTandSwwage.Common.Helpers
{
    /// <summary>
    /// 加密解密辅助类
    /// </summary>
    public static class EncryptHelper
    {
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string encryptKey = "abc123";

        /// <summary>  
        /// DES加密字符串  
        /// </summary>  
        /// <param name="encryptString">待加密的字符串</param>  
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>  
        public static string EncryptDES(string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>  
        /// DES解密字符串  
        /// </summary>  
        /// <param name="decryptString">待解密的字符串</param>  
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>  
        public static string DecryptDES(string decryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        /// <summary>
        /// Base64加密【标准编码方式】
        /// </summary>
        /// <param name="encryptStr">需要加密的字符串</param>
        /// <returns></returns>
        public static string EncryptBase64(string encryptStr)
        {
            byte[] bytes = Encoding.Default.GetBytes(encryptStr);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64加密【非标准编码方式】
        /// </summary>
        /// <param name="encryptStr">需要加密的字符串</param>
        /// <param name="sKey">密钥</param>
        /// <param name="encoding">编码格式，默认传null或不传，默认UTF-8编码</param>
        /// <returns></returns>
        public static string EncryptByBase64(string encryptStr, string sKey = "", System.Text.Encoding encoding = null)
        {
            string base64Key = sKey;
            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
         'U','V','W','X','Y','Z','0','a','1','b','2','c','3','d','4','e','5','f','6','g','7','h','8','i','9','j','k','l','m','n',
         'o','p','q','r','s','t','u','v','w','x','y','z','+','/','='};
            if (!string.IsNullOrEmpty(base64Key))
            {
                List<char> base64CodeList = Base64Code.ToList<char>();
                List<char> base64KeyList = base64Key.ToList<char>();
                //将Base64Code中与base64Key中相同字符移除，并将此字符添加到Base64Code最前面
                for (int i = base64KeyList.Count - 1; i >= 0; i--)
                {
                    base64CodeList.Remove(base64KeyList[i]);
                    base64CodeList.Insert(0, base64KeyList[i]);
                }
                Base64Code = base64CodeList.ToArray<char>();
            }
            //将需要加密的字符串转换为byte数组
            byte empty = (byte)0;
            byte[] by;
            if (encoding == null)
                by = System.Text.Encoding.UTF8.GetBytes(encryptStr);
            else
                by = encoding.GetBytes(encryptStr);
            //将转换后的byte数组转换为数组集合
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(by);
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            //将字符分成3个字节一组，如果不足，则以0补齐
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            //将3个字节的每组字符转换成4个字节一组的。3个一组，一组一组变成4个字节一组
            //方法是：转换成ASCII码，按顺序排列24位数据，再把这24位数据分成4组，即每组6位。
            //再在每组的的最高位前补两个0凑足一个字节。
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                //取一组3个字节的组
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                //六个位为一组，补0变成4个字节 
                int[] outstr = new int[4];
                //第一个输出字节：取第一输入字节的前6位，并且在高位补0，使其变成8位（一个字节） 
                outstr[0] = instr[0] >> 2;
                //第二个输出字节：取第一输入字节的后2位和第二个输入字节的前4位（共6位），并且在高位补0，使其变成8位（一个字节） 
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                //第三个输出字节：取第二输入字节的后4位和第三个输入字节的前2位（共6位），并且在高位补0，使其变成8位（一个字节）
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                //第四个输出字节：取第三输入字节的后6位，并且在高位补0，使其变成8位（一个字节） 
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }


        /// <summary>
        /// Base64解密【标准编码方式】
        /// </summary>
        /// <param name="decryptStr">需要解密的字符串</param>
        /// <returns></returns>
        public static string DecryptBase64(string decryptStr)
        {
            byte[] bytes = Convert.FromBase64String(decryptStr);
            return Encoding.Default.GetString(bytes);
        }

        #region MD5加密
        /// <summary>
        /// MD5加密摘要算法
        /// </summary>
        /// <param name="encryptStr">需要加密的字符串</param>
        /// <returns>返回：已加密的字符串</returns>
        public static string EncryptByMD5(string encryptStr)
        {
            if (string.IsNullOrEmpty(encryptStr))
                return "";
#pragma warning disable 618
            //string md5Str = FormsAuthentication.HashPasswordForStoringInConfigFile(encryptStr, "md5");
#pragma warning restore 618

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(encryptStr);
            bs = md5.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string md5Str = s.ToString();
            return md5Str.Length > 0 ? md5Str : "";
        }

        /// <summary>
        /// MD5两次加密
        /// </summary>
        /// <param name="encryptStr">需要加密的字符串</param>
        /// <param name="isDouble">是否两次加密</param>
        /// <returns>返回：已加密的字符串</returns>
        public static string EncryptByMD5(string encryptStr, bool isDouble)
        {
            string md5Dbl = isDouble ? EncryptByMD5(EncryptByMD5(encryptStr).ToUpper()) : EncryptByMD5(encryptStr);
            return md5Dbl;
        }
        /// <summary>
        /// MD5两次加密，并截取掉后几位
        /// </summary>
        /// <param name="encryptStr">需要加密的字符串</param>
        /// <param name="isDouble">是否两次加密</param>
        /// <param name="length">需要截取的长度</param>
        /// <returns></returns>
        public static string EncryptByMD5(string encryptStr, bool isDouble, int length)
        {
            string md5Dbl = EncryptByMD5(encryptStr, isDouble);
            if (length > 31 || length < 0)
                return md5Dbl;
            return md5Dbl.Length > 0 ? md5Dbl.Substring(0, md5Dbl.Length - length) : "";
        }
        #endregion MD5

        #region DES加密/解密  说明： 对称算法，数据加密标准，速度较快，适用于加密大量数据的场合；

        private const string sKeyDefa = "qJzGEh6hESZDVJeCnFPGuxzaiB7NLQM3";

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptStr">需要加密的字符串</param>
        /// <param name="sKey">密钥，且必须为8位</param>
        /// <returns>返回：已加密的字符串</returns>
        public static string EncryptByDES(string encryptStr, string sKey = null)
        {
            if (string.IsNullOrEmpty(encryptStr))
                return "";
            if (string.IsNullOrEmpty(sKey))
                sKey = sKeyDefa.Substring(0, 8);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptStr);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptStr">要解密的字符串</param>
        /// <param name="sKey">密钥，且必须为8位</param>
        /// <returns>返回：已解密的字符串</returns>
        public static string DecryptByDES(string decryptStr, string sKey = null)
        {
            if (string.IsNullOrEmpty(decryptStr))
                return "";
            if (string.IsNullOrEmpty(sKey))
                sKey = sKeyDefa.Substring(0, 8);
            byte[] inputByteArray = Convert.FromBase64String(decryptStr);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion
    }
}
