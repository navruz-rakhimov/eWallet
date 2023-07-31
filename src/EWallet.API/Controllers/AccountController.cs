using EWallet.API.Authentication.Filters;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Accounts.Requests.CreateAccount;
using EWallet.Application.Accounts.Requests.GetAccountById;
using EWallet.Application.Accounts.Requests.GetAccountList;
using EWallet.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[HmacAuthentication]
public class AccountController : BaseController
{
    public AccountController(IMediator mediator) 
        : base(mediator)
    {
    }
    
    [HttpPost(nameof(GetAccount))]
    public async Task<BaseResponse<AccountDto>> GetAccount([FromBody] GetAccountByIdRequest request)
    {
        return await Mediator.Send(request);
    }
    
    [HttpPost(nameof(GetAccountList))]
    public async Task<BaseResponse<List<AccountDto>>> GetAccountList()
    {
        return await Mediator.Send(new GetAccountListRequest());
    }
    
    [HttpPost(nameof(CreateAccount))]
    public async Task<BaseResponse<AccountDto>> CreateAccount([FromBody] AccountInputDto payload)
    {
        return await Mediator.Send(new CreateAccountRequest(payload));
    }
}