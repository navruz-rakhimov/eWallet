using EWallet.API.Authentication.Filters;
using EWallet.Application.Common.Responses;
using EWallet.Application.Transactions.Dtos;
using EWallet.Application.Transactions.Requests.CreateTransaction;
using EWallet.Application.Transactions.Requests.GetTransactionList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[HmacAuthentication]
public class TransactionController : BaseController
{
    public TransactionController(IMediator mediator) 
        : base(mediator)
    {
    }

    [HttpPost(nameof(CreateTransaction))]
    public async Task<BaseResponse<TransactionDto>> CreateTransaction([FromBody] TransactionInputDto payload)
    {
        return await Mediator.Send(new CreateTransactionRequest(payload));
    }
    
    [HttpPost(nameof(GetTransactionList))]
    public async Task<BaseResponse<List<TransactionDto>>> GetTransactionList([FromBody] GetTransactionListRequest request)
    {
        return await Mediator.Send(request);
    }
}
    