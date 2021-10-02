using System.Text;

namespace Circle.Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        public static string CreatePasswordHash(string password)
        {
            return ToMd5(password);
        }

        public static bool VerifyPasswordHash(string inputPassword, string userPassword)
        {
            return ToMd5(inputPassword).ToUpper().Equals(userPassword.ToUpper());
        }

        private static string ToMd5(string plainText)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}