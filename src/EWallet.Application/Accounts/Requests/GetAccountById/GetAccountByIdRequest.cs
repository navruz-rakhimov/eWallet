using AutoMapper;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Handlers;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using static EWallet.Application.Accounts.AccountConstants;

namespace EWallet.Application.Accounts.Requests.GetAccountById;

public class GetAccountByIdRequest : BaseRequest<AccountDto>
{
    public int AccountId { get; }

    public GetAccountByIdRequest(int accountId)
    {
        AccountId = accountId;
    }
}

public class GetAccountByIdHandler : BaseHandler<GetAccountByIdRequest, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAccountByIdHandler(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
        : base(httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public override async Task<BaseResponse<AccountDto>> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
    {
        var accounts = await _unitOfWork.AccountRepository
            .GetByFilterAsync(account => account.Id == request.AccountId && account.UserId == CurrentUserId, cancellationToken);

        var account = accounts.FirstOrDefault();
        if (account == null)
        {
            return ResponseFactory.BadRequest(Errors.ACCOUNT_WITH_GIVEN_ID_DOES_NOT_EXIST(request.AccountId));
        }

        var organizationDto = _mapper.Map<AccountDto>(account);
        return ResponseFactory.Ok(organizationDto);
    }
}


