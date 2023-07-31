using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Responses
{
    internal static class BaseResponseHelper
    {
        public static bool CheckIfStatusSuccessful(int statusCode) => statusCode is >= (int)HttpStatusCode.OK and < (int)HttpStatusCode.Ambiguous;
    }

}
