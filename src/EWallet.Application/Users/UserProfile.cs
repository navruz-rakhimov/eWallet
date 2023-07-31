using AutoMapper;
using EWallet.Application.Users.Dtos;
using EWallet.Domain.Entities;

namespace EWallet.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserInputDto, User>();
    }
}