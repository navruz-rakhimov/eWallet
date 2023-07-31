using AutoMapper;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Handlers;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Application.Accounts.Requests.GetAccountList;

public class GetAccountListRequest : BaseRequest<List<AccountDto>>
{
}

public class GetAccountListHandler : BaseHandler<GetAccountListRequest, List<AccountDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetAccountListHandler(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper) 
        : base(httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public override async Task<BaseResponse<List<AccountDto>>> Handle(GetAccountListRequest request, CancellationToken cancellationToken)
    {
        var accountList = _unitOfWork.AccountRepository
            .GetAllAsQueryable()
            .Where(account => account.UserId == CurrentUserId);
        
        var accountDtoList = _mapper.ProjectTo<AccountDto>(accountList);

        return ResponseFactory.Ok(await accountDtoList.ToListAsync(cancellationToken));
    }
}