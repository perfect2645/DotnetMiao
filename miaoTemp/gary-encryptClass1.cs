using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Class1
    {
		private string DecdRes(string content)
		{
			WriteLog("开始解密数据");
			string text = mysignature;
			if (string.IsNullOrEmpty(text))
			{
				text = Getsignature();
			}
			WriteLog("sig:" + text);
			var key = text.Substring(0, 16);
			var tt = Decrypt(content, key, "1234567890000000");
			WriteLog(tt);
			WriteLog("解密数据完成");
			return tt;
		}

		private string EncodeRes(string content)
		{
			WriteLog("开始解密数据");
			string text = mysignature;
			if (string.IsNullOrEmpty(text))
			{
				text = Getsignature();
			}
			WriteLog("sig:" + text);
			var key = text.Substring(0, 16);
			var tt = Encrypt(content, key, "1234567890000000");
			WriteLog(tt);
			WriteLog("解密数据完成");
			return tt;
		}

		public string Decrypt(string toDecrypt, string key, string iv)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
			byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
			var str1 = UnHex(toDecrypt, "utf-8");
			byte[] toEncryptArray = str1;
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = keyArray;
			rDel.IV = ivArray;
			rDel.Mode = CipherMode.CBC;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateDecryptor();
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

			stopwatch.Stop();
			var time = stopwatch.ElapsedMilliseconds;
			return UTF8Encoding.UTF8.GetString(resultArray);
		}

		public string Encrypt(string toDecrypt, string key, string iv)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
			byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);

			if ((toDecrypt.Length % 2) != 0)
			{
				toDecrypt += " ";//空格
								 //throw new ArgumentException("s is not valid chinese string!");
			}


			byte[] toEncryptArray = System.Text.Encoding.GetEncoding("utf-8").GetBytes(toDecrypt);
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = keyArray;
			rDel.IV = ivArray;
			rDel.Mode = CipherMode.CBC;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateEncryptor();
			byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
			stopwatch.Stop();
			var time = stopwatch.ElapsedMilliseconds;
			return ToHex(resultArray, "utf-8", false);
		}

		public string ToHex(byte[] bytes, string charset, bool fenge)
		{
			string str = "";
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] < 16)
				{
					str += "0" + string.Format("{0:X0}", bytes[i]);
				}
				else
				{
					str += string.Format("{0:X0}", bytes[i]);
				}

				if (fenge && (i != bytes.Length - 1))
				{
					str += string.Format("{0}", ",");
				}
			}
			return str.ToLower();
		}

		public byte[] UnHex(string hex, string charset)
		{
			try
			{
				//hex = hex.Replace(",", "");
				//hex = hex.Replace("\n", "");
				//hex = hex.Replace("\\", "");
				//hex = hex.Replace(" ", "");
				if (hex.Length % 2 != 0)
				{
					hex += "20";//空格
					WriteLog("hex is not a valid number!");
				}

				// 需要将 hex 转换成 byte 数组。
				byte[] bytes = new byte[hex.Length / 2];
				for (int i = 0; i < bytes.Length; i++)
				{
					try
					{
						// 每两个字符是一个 byte。
						bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
						System.Globalization.NumberStyles.HexNumber);
					}
					catch (Exception ex)
					{
						WriteLog(ex.Message);
					}
				}

				return bytes;
			}
			catch (Exception ex)
			{
				WriteLog("UnHex:" + ex.Message);
				return new byte[] { };
			}
		}
	}
}
