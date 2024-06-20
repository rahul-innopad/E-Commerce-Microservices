using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.APIResponseLibrary.Constant.APIConstants
{
    public enum APIResponseEnumManager
    {
        Success = 200,


        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        AlreadyReactiveUser = 420,
        Failed = 421,
        IsDeleted = 422,
        IsDeactivate = 423,


        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GateTimeOut = 504,
        HTTPVersionNotSupported = 505,
        AlreadyExist = 520,

        PasswordNotMatch = 521,
        NotHaveActivePlan = 522,
        SubscriptionNotActive = 533,
    }
}
