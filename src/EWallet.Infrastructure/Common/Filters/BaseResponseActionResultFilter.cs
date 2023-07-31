using EWallet.Application.Common.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Infrastructure.Common.Filters
{
    public class BaseResponseActionResultFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            var objectResult = executedContext.Result as ObjectResult;

            switch (objectResult?.Value)
            {
                case IBaseResponse baseResponse:
                {
                    executedContext.HttpContext.Response.StatusCode = baseResponse.Status;

                    if (!StatusCanHaveBody(baseResponse.Status))
                    {
                        executedContext.Result = null;
                    }

                    break;
                }

                default: return;
            }
        }

        /// <summary>
        /// Logic to determine if the code is allowed to have HttpBody.
        /// Logic is the same used in the Kestrel Server source code:
        /// <a href="https://source.dot.net/#Microsoft.AspNetCore.Server.Kestrel.Core/Internal/Http/HttpProtocol.cs,e2fb69fa3e605bb0,references">
        /// Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.StatusCanHaveBody
        /// </a>
        /// </summary>
        /// <param name="statusCode">Status code as an int value</param>
        /// <returns></returns>
        private static bool StatusCanHaveBody(int statusCode)
        {
            // List of status codes taken from Microsoft.Net.Http.Server.Response
            return statusCode != StatusCodes.Status204NoContent &&
                   statusCode != StatusCodes.Status205ResetContent &&
                   statusCode != StatusCodes.Status304NotModified;
        }
    }

}
