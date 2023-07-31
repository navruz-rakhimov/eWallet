using AutoMapper;
using EWallet.Application.Accounts;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using EWallet.Application.Transactions.Dtos;
using EWallet.Domain.Entities;
using MediatR;

namespace EWallet.Application.Transactions.Requests.CreateTransaction;

public class CreateTransactionRequest : BaseRequest<TransactionDto>
{
    public TransactionInputDto Payload { get; }

    public CreateTransactionRequest(TransactionInputDto payload)
    {
        Payload = payload;
    }
}

public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, BaseResponse<TransactionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransactionRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<BaseResponse<TransactionDto>> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        // todo: create validator service
        
        var fromAccount =
            await _unitOfWork.AccountRepository.GetAsync(request.Payload.FromAccountId, cancellationToken: cancellationToken);

        if (fromAccount is null)
        {
            return ResponseFactory.BadRequest(
                AccountConstants.Errors.ACCOUNT_WITH_GIVEN_ID_DOES_NOT_EXIST(request.Payload.FromAccountId));
        }
        
        var toAccount = 
            await _unitOfWork.AccountRepository.GetAsync(request.Payload.ToAccountId, cancellationToken: cancellationToken);

        if (toAccount is null)
        {
            return ResponseFactory.BadRequest(
                AccountConstants.Errors.ACCOUNT_WITH_GIVEN_ID_DOES_NOT_EXIST(request.Payload.ToAccountId));
        }

        var transaction = _mapper.Map<Transaction>(request.Payload);

        if (transaction.Amount > fromAccount.Balance)
        {
            return ResponseFactory.BadRequest(TransactionConstants.Errors.InsufficientFunds);
        }

        if (!toAccount.IsIdentified &&
            toAccount.Balance + transaction.Amount > AccountConstants.MaxBalanceForNotIdentifiedAccount)
        {
            return ResponseFactory.BadRequest(TransactionConstants.Errors.ReceiverAccountBalanceExceedsLimit);
        }
        
        if (toAccount.IsIdentified &&
            toAccount.Balance + transaction.Amount > AccountConstants.MaxBalanceForIdentifiedAccount)
        {
            return ResponseFactory.BadRequest(TransactionConstants.Errors.ReceiverAccountBalanceExceedsLimit);
        }

        fromAccount.Balance -= transaction.Amount;
        toAccount.Balance += transaction.Amount;

        await _unitOfWork.TransactionRepository.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return ResponseFactory.Created(_mapper.Map<TransactionDto>(transaction));
    }
}