using AutoMapper;
using EWallet.Application.Accounts.Dtos;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Handlers;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using EWallet.Application.Users;
using EWallet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EWallet.Application.Accounts.Requests.CreateAccount;

public class CreateAccountRequest : BaseRequest<AccountDto>
{
    public AccountInputDto Payload { get; }

    public CreateAccountRequest(AccountInputDto payload)
    {
        Payload = payload;
    }
}

public class CreateAccountRequestHandler : BaseHandler<CreateAccountRequest, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public CreateAccountRequestHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor) 
        : base(httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    
    public override async Task<BaseResponse<AccountDto>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Payload.UserId.ToString());

        if (user is null)
        {
            return ResponseFactory.BadRequest(
                UserConstants.Errors.USER_WITH_GIVEN_ID_DOES_NOT_EXIST(request.Payload.UserId));
        }
        
        var account = _mapper.Map<Account>(request.Payload);
        
        await _unitOfWork.AccountRepository.AddAsync(account, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Created(_mapper.Map<AccountDto>(account));
    }
}