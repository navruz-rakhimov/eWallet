using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Responses
{
    #region Response with Data

    public class BaseResponse<T> : IBaseResponse
    {
        private HttpStatusCode _statusCode;

        public int Status
        {
            get => (int)_statusCode;
            set => _statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), value.ToString());
        }

        public virtual bool IsSuccess => BaseResponseHelper.CheckIfStatusSuccessful(Status);

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(IBaseResponse result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            Message = result.Message;
            Status = result.Status;
        }

        public BaseResponse(HttpStatusCode statusCode, T data)
        {
            _statusCode = statusCode;
            Data = data;
        }

        public BaseResponse(HttpStatusCode statusCode, string message, T data)
            : this(statusCode, data)
        {
            Message = message;
        }

        public BaseResponse(int status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static implicit operator BaseResponse<T>(BaseResponse result)
        {
            return new BaseResponse<T>(result);
        }
    }

    #endregion

    #region Response without Data

    public class BaseResponse : IBaseResponse
    {
        private HttpStatusCode _statusCode;

        public int Status
        {
            get => (int)_statusCode;
            set => _statusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), value.ToString());
        }

        public virtual bool IsSuccess => BaseResponseHelper.CheckIfStatusSuccessful(Status);

        public string Message { get; set; } = string.Empty;

        public BaseResponse()
        {
        }

        public BaseResponse(IBaseResponse result)
        {
            Message = result.Message;
            Status = result.Status;
        }

        public BaseResponse(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }

        public BaseResponse(HttpStatusCode statusCode, string message)
            : this(statusCode)
        {
            Message = message;
        }

        public BaseResponse(int status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    #endregion
}
