using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EWallet.Application.Common.Mediatr.Handlers;

public abstract class BaseHandler<TRequest> : IRequestHandler<TRequest, BaseResponse> 
    where TRequest : BaseRequest
{
    protected int? CurrentUserId { get; }

    protected BaseHandler(IHttpContextAccessor httpContextAccessor)
    {
        if(httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-UserId", out var userId))
        {
            CurrentUserId = int.Parse(userId);
        }
    }

    public abstract Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>> 
    where TRequest : BaseRequest<TResponse>
{
    protected int? CurrentUserId { get; }

    protected BaseHandler(IHttpContextAccessor httpContextAccessor)
    {
        if(httpContextAccessor.HttpContext.Request.Headers.TryGetValue("X-UserId", out var userId))
        {
            CurrentUserId = int.Parse(userId);
        }
    }

    public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}