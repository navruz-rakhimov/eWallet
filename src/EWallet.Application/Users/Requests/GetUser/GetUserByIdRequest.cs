using AutoMapper;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using EWallet.Application.Users.Dtos;
using EWallet.Domain.Entities;
using MediatR;
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

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, BaseResponse<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(
        IMapper mapper,
        UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<BaseResponse<UserDto>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
        {
            return ResponseFactory.BadRequest(UserConstants.Errors.USER_WITH_GIVEN_ID_DOES_NOT_EXIST(request.UserId));
        }

        return ResponseFactory.Ok(_mapper.Map<UserDto>(user));
    }
}