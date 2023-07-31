using EWallet.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Mediatr.Requests
{
    public abstract class BaseRequest : IRequest<BaseResponse>
    {
    }

    public abstract class BaseRequest<TResponse> : IRequest<BaseResponse<TResponse>>
    {
    }
}
