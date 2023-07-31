using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Responses
{
    public static class ResponseFactory
    {
        public static BaseResponse<T> Ok<T>(T data)
        {
            return new BaseResponse<T>(HttpStatusCode.OK, data);
        }

        public static BaseResponse Ok()
        {
            return new BaseResponse(HttpStatusCode.OK);
        }

        public static BaseResponse NoContent()
        {
            return new BaseResponse(HttpStatusCode.NoContent);
        }

        public static BaseResponse NotFound(string errorMessage = "")
        {
            return new BaseResponse(HttpStatusCode.NotFound, errorMessage);
        }

        public static BaseResponse<T> NotFoundObject<T>(T notFoundObject)
        {
            return new BaseResponse<T>(HttpStatusCode.NotFound, notFoundObject);
        }

        public static BaseResponse BadRequest(string errorMessage = "")
        {
            return new BaseResponse(HttpStatusCode.BadRequest, errorMessage);
        }

        public static BaseResponse BadRequest(ValidationResult validationResult)
        {
            return BadRequest(validationResult.ToString());
        }

        public static BaseResponse Conflict(string errorMessage = "")
        {
            return new BaseResponse(HttpStatusCode.Conflict, errorMessage);
        }

        public static BaseResponse Created()
        {
            return new BaseResponse(HttpStatusCode.Created);
        }

        public static BaseResponse<T> Created<T>(T data)
        {
            return new BaseResponse<T>(HttpStatusCode.Created, data);
        }

        public static BaseResponse<T> Accepted<T>(T data)
        {
            return new BaseResponse<T>(HttpStatusCode.Accepted, data);
        }

        public static BaseResponse Accepted()
        {
            return new BaseResponse(HttpStatusCode.Accepted);
        }

        public static BaseResponse Forbid(string errorMessage = "")
        {
            return new BaseResponse(HttpStatusCode.Forbidden, errorMessage);
        }
    }
}
