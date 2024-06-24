using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIResponseLibrary.Constant.APIConstants
{
    public class APIMessage
    {
        public static string Failed = "Failed";
        public static string Success = "Success";
        public static string Deleted = "Success, deleted successfully";
        public static string UserNotFound = "User is not found";
        public static string IsDeactivate = "User is deactive";
        public static string InvalidPassword = "Invalid Password";
        public static string InternalServerError = "Internal Server Error, Please try after some time";
        public static string AtLeastProvideOne = "At least one of email, username, or mobile must be provided.";
        public static string LoggedOut = "Logged Out Successfully";

        public static string NotFound = "{0} not found";
        public static string NotNull = "{0} mut be required";
    }
}
