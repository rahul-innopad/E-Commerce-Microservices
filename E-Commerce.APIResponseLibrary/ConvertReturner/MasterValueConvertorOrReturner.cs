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

        public static string GetUniqueStringValue()
        {
            var uniqueValue= Guid.NewGuid().ToString("N");
            return "_" + uniqueValue.Substring(0, 20);
        }
    }
}
