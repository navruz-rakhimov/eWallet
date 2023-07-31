using AutoMapper;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Accounts.Requests.GetAccountList;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Handlers;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using EWallet.Application.Transactions.Dtos;
using EWallet.Application.Transactions.Filters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Application.Transactions.Requests.GetTransactionList;

public class GetTransactionListRequest : BaseRequest<List<TransactionDto>>
{
    public TransactionsFilter? Filter { get; }

    public GetTransactionListRequest(TransactionsFilter filter)
    {
        Filter = filter;
    }
}

public class GetTransactionListRequestHandler : BaseHandler<GetTransactionListRequest, List<TransactionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetTransactionListRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) 
        : base(httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public override async Task<BaseResponse<List<TransactionDto>>> Handle(GetTransactionListRequest request, CancellationToken cancellationToken)
    {
        // todo: implement proper filtering
        
        var transactions = _unitOfWork.TransactionRepository
            .GetAllAsQueryable();

        if (request.Filter?.StartDate is not null)
            transactions = transactions.Where(t => t.CreatedAt >= request.Filter.StartDate);

        if (request.Filter?.EndDate is not null)
            transactions = transactions.Where(t => t.CreatedAt <= request.Filter.EndDate);
        
        var transactionDtoList = await _mapper.ProjectTo<TransactionDto>(transactions).ToListAsync(cancellationToken);

        return ResponseFactory.Ok(transactionDtoList);
    }
}