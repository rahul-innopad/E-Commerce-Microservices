using E_Commerce.APIResponseLibrary.EnumManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIResponseLibrary.ConvertReturner
{
    public class MasterValueConvertorOrReturner
    {
        public const string Auth = "auth";
        public const string Product = "product";
        public const string Category = "category";

        public static string GetUniqueStringValue()
        {
            var uniqueValue= Guid.NewGuid().ToString("N");
            return "_" + uniqueValue.Substring(0, 20);
        }
        public static string GetUniqueRandomStringValue()
        {
            int length = 30;
            string chars = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            string uniqueValue = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return "_" + uniqueValue.Substring((int)UniversalEnumManager.SubStringLengthZero, (int)UniversalEnumManager.SubStringLengthFive).ToString();
        }

        public static string GetUniqueRandomNumber()
        {
            int length = 16;
            string chars = "123456789";
            var random = new Random();
            string username = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return username;
        }

        public static DateTime GetCurrentDateTimeNow()
        {
            return DateTime.Now;
        }

        public static string GetSubStringValue(string t)
        {
            return t.Substring((int)UniversalEnumManager.SubStringLengthZero, (int)UniversalEnumManager.SubStringLengthThree).ToString();
        }
    }
}
