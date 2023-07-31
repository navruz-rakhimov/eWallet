﻿using AutoMapper;
using EWallet.Application.Common.Interfaces.UnitOfWork;
using EWallet.Application.Common.Mediatr.Requests;
using EWallet.Application.Common.Responses;
using EWallet.Application.Users.Dtos;
using EWallet.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EWallet.Application.Users.Requests.CreateUser;

public class CreateUserRequest : BaseRequest<UserDto>
{
    public UserInputDto Payload { get; }

    public CreateUserRequest(UserInputDto payload)
    {
        Payload = payload;
    }
}

public class CreateUserHandler : IRequestHandler<CreateUserRequest, BaseResponse<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public CreateUserHandler(
        IMapper mapper,
        UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<BaseResponse<UserDto>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = _mapper.Map<User>(request.Payload);
        var passwordHasher = new PasswordHasher<User>();
        userToCreate.PasswordHash = passwordHasher.HashPassword(userToCreate, request.Payload.Password);
        
        await _userManager.CreateAsync(userToCreate);

        return ResponseFactory.Ok(_mapper.Map<UserDto>(userToCreate));
    }
}