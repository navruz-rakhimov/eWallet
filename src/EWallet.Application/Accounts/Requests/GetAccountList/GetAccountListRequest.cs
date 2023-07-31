using AutoMapper;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Application.Accounts.Requests.GetAccountList;

public class GetAccountListRequest : BaseRequest<List<AccountDto>>
{
}

public class GetAccountListHandler : IRequestHandler<GetAccountListRequest, BaseResponse<List<AccountDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetAccountListHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<List<AccountDto>>> Handle(GetAccountListRequest request, CancellationToken cancellationToken)
    {
        var accountList = _unitOfWork.AccountRepository.GetAllAsQueryable();
        var accountDtoList = _mapper.ProjectTo<AccountDto>(accountList);

        return ResponseFactory.Ok(await accountDtoList.ToListAsync(cancellationToken));
    }
}