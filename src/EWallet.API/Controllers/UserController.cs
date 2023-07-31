using EWallet.Application.Common.Responses;
using EWallet.Application.Users.Dtos;
using EWallet.Application.Users.Requests.CreateUser;
using EWallet.Application.Users.Requests.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    public UserController(IMediator mediator) 
        : base(mediator)
    {
    }
    
    [HttpPost(nameof(GetUser))]
    public async Task<BaseResponse<UserDto>> GetUser([FromBody] GetUserByIdRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPost(nameof(CreateUser))]
    public async Task<BaseResponse<UserDto>> CreateUser([FromBody] UserInputDto payload)
    {
        return await Mediator.Send(new CreateUserRequest(payload));
    }
}
