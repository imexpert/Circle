using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Circle.Core.Extensions
{
    public static class StringExtensions
    {
        #region TitleCase

        /// <summary>
        /// Mevcut thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the culture info with the specified name
        /// </summary>
        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            var cultureInfo = new CultureInfo(cultureInfoName);
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the specified culture info
        /// </summary>
        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        #endregion

        #region LeftRightTrim

        public static string Left(this string str, int count)
        {
            if (string.IsNullOrEmpty(str) || count < 1)
                return string.Empty;
            else
                return str.Substring(0, Math.Min(count, str.Length));
        }

        public static string Right(this string str, int count)
        {
            if (string.IsNullOrEmpty(str) || count < 1)
                return string.Empty;
            else if (str.Length >= count)
                return str.Substring(str.Length - count, Math.Min(count, str.Length));
            else
                return string.Empty;
        }

        public static string TrimPrefix(this string str, string prefix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(prefix) && str.StartsWith(prefix))
            {
                return str.Substring(prefix.Length);
            }
            return str;
        }

        public static string TrimSuffix(this string str, string suffix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(suffix) && str.EndsWith(suffix))
            {
                return str.Remove(str.Length - suffix.Length);
            }
            return str;
        }

        public static string RemoveLastCharacter(this string str)
        {
            return str.Substring(0, str.Length - 1);
        }

        public static string RemoveLast(this string str, int number)
        {
            return str.Substring(0, str.Length - number);
        }

        public static string RemoveFirstCharacter(this string str)
        {
            return str.Substring(1);
        }

        public static string RemoveFirst(this string str, int number)
        {
            return str.Substring(number);
        }

        public static string Truncate(this string str, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";
            string truncatedString = str;

            if (maxLength <= 0) return truncatedString;
            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (str == null || str.Length <= maxLength) return truncatedString;

            truncatedString = str.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        #endregion

        #region Encrypt/Decrypt

        public static string Encrypt(this string str, string key)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            CspParameters cspp = new CspParameters();
            cspp.KeyContainerName = key;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(str), true);

            return BitConverter.ToString(bytes);
        }

        public static string Decrypt(this string str, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key))
            {
                return result;
            }

            try
            {
                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = str.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                result = System.Text.UTF8Encoding.UTF8.GetString(bytes);

            }
            catch { result = null; }
            return result;
        }

        #endregion

        public static string Format(this string s, params object[] args)
        {
            if (args != null && args.Length > 0)
                return string.Format(s, args);
            else
                return s;
        }

        public static bool IsMatch(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsBlank(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsTcKimlikNo(this string str)
        {
            if (str.Length < 11)
                return false;
            else if (str.Substring(0, 1) == "0")
                return false;

            int toplam1 = Convert.ToInt32(str[0].ToString()) + Convert.ToInt32(str[2].ToString()) + Convert.ToInt32(str[4].ToString()) + Convert.ToInt32(str[6].ToString()) + Convert.ToInt32(str[8].ToString());
            int toplam2 = Convert.ToInt32(str[1].ToString()) + Convert.ToInt32(str[3].ToString()) + Convert.ToInt32(str[5].ToString()) + Convert.ToInt32(str[7].ToString());

            int sonuc = ((toplam1 * 7) - toplam2) % 10;

            if (sonuc.ToString() != str[9].ToString())
                return false;

            int toplam3 = 0;
            for (int i = 0; i < 10; i++)
                toplam3 += Convert.ToInt32(str[i].ToString());

            if ((toplam3 % 10).ToString() != str[10].ToString())
                return false;

            return true;
        }

        public static bool IsEmail(this string str)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(str);
        }

        public static bool IsUrl(this string str)
        {
            string strRegex = "^(https?://)"
        + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
        + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP Address
        + "|" // IP or domain
        + @"([0-9a-z_!~*'()-]+\.)*" // www.
        + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level
        + @"(\.[a-z]{2,6})?)" // .com or .ext
        + "(:[0-9]{1,5})?" // port number
        + "((/?)|" // slash
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            return new Regex(strRegex).IsMatch(str);
        }

        public static bool IsGuid(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex format = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            Match match = format.Match(str);

            return match.Success;
        }

        public static bool IsNumeric(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            long retNum;
            return long.TryParse(str, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static bool IsDouble(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            double retNum;
            return double.TryParse(str, out retNum);
        }

        public static bool IsDate(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            DateTime dt;
            return (DateTime.TryParse(str, out dt));
        }

        public static T ToEnum<T>(this string str) where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), str, true);
        }

        public static string ToCompareableString(this string str)
        {
            str = str.Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S").Replace("İ", "I").Replace("Ö", "O").Replace("Ç", "C");
            str = str.Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ı", "i").Replace("ö", "o").Replace("ç", "c");
            str = str.Replace(".", string.Empty).Replace(",", string.Empty).Replace("'", string.Empty).Replace("´", string.Empty).Replace("`", string.Empty);
            str = str.Replace("_", string.Empty).Replace("\"", string.Empty);
            str = str.Replace(" ", string.Empty);
            str = str.ToLower();
            return str;
        }

        public static bool? ToBoolean(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                str = str.ToLower();
                if (string.Compare("true", str, true) == 0)
                {
                    return true;
                }
                if (string.Compare("false", str, true) == 0)
                {
                    return false;
                }
                bool result;
                if (bool.TryParse(str, out result))
                {
                    return result;
                }
                else return null;
            }
        }

        public static Guid ToGuid(this string str)
        {
            if (str == null)
                return Guid.Empty; //throw new ArgumentNullException("s");

            Guid tmp;
            if (Guid.TryParse(str, out tmp))
                return tmp;
            else
                return Guid.Empty;
        }

        public static string ToDigits(this string str)
        {
            return new string(str.Where(c => char.IsDigit(c)).ToArray());
        }

        public static long ToLong(this string str)
        {
            if (str == null)
                return 0;

            long tmp = 0;
            if (long.TryParse(str, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out tmp))
                return tmp;
            else
                return 0;

        }

        public static int ToInt(this string str)
        {
            if (str == null)
                return 0;

            int tmp;
            if (int.TryParse(str, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out tmp))
                return tmp;
            else
                return 0;

        }

        public static long ToInt16(this string value)
        {
            Int16 result = 0;

            if (!string.IsNullOrEmpty(value))
                Int16.TryParse(value, out result);

            return result;
        }

        public static long ToInt32(this string value)
        {
            Int32 result = 0;

            if (!string.IsNullOrEmpty(value))
                Int32.TryParse(value, out result);

            return result;
        }

        public static long ToInt64(this string value)
        {
            Int64 result = 0;

            if (!string.IsNullOrEmpty(value))
                Int64.TryParse(value, out result);

            return result;
        }

        public static double ToDouble(this string str)
        {
            if (str == null)
                return 0.0;

            double tmp;
            if (double.TryParse(str, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out tmp))
                return tmp;
            else
                return 0.0;

        }

        public static DateTime ToDateTime(this string str)
        {
            if (str == null)
                return DateTime.MinValue;

            DateTime tmp;
            CultureInfo cultureinfo = new System.Globalization.CultureInfo("tr-TR");
            DateTimeStyles styles;
            styles = DateTimeStyles.AssumeLocal;

            if (DateTime.TryParse(str, cultureinfo, styles, out tmp))
                return tmp;
            else
                return DateTime.MinValue;

        }
    }
}
