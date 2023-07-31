using AutoMapper;
using EWallet.Application.Common.Mediatr.Handlers;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using EWallet.Application.Users.Dtos;
using EWallet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EWallet.Application.Users.Requests.GetUser;

public class GetUserByIdRequest : BaseRequest<UserDto>
{
    public int UserId { get; }

    public GetUserByIdRequest(int userId)
    {
        UserId = userId;
    }
}

public class GetUserByIdHandler : BaseHandler<GetUserByIdRequest, UserDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager) 
        : base(httpContextAccessor)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public override async Task<BaseResponse<UserDto>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return ResponseFactory.BadRequest(UserConstants.Errors.USER_WITH_GIVEN_ID_DOES_NOT_EXIST(request.UserId));
        }

        return ResponseFactory.Ok(_mapper.Map<UserDto>(user));
    }
}