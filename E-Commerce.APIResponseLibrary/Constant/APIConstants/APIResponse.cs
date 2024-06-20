using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIResponseLibrary.Constant.APIConstants
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public object Response { get; set; }
        public object Message { get; set; }

    }
}
