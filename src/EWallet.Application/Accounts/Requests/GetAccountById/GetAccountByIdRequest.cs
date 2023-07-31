using AutoMapper;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using MediatR;
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

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdRequest, BaseResponse<AccountDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAccountByIdHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<AccountDto>> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.GetAsync(request.AccountId, cancellationToken: cancellationToken);

        if (account == null)
        {
            return ResponseFactory.BadRequest(Errors.ACCOUNT_WITH_GIVEN_ID_DOES_NOT_EXIST(request.AccountId));
        }

        var organizationDto = _mapper.Map<AccountDto>(account);
        return ResponseFactory.Ok(organizationDto);
    }
}


