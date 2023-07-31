using FluentValidation;

namespace EWallet.Application.Users.Dtos;

public class UserInputDto
{
    public string UserName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class UserInputDtoValidator : AbstractValidator<UserInputDto>
{
    public UserInputDtoValidator()
    {
        RuleFor(dto => dto.PhoneNumber)
            .Length(min: 12, max: 13)
            .Matches(@"^\d+$");
        
        RuleFor(dto => dto.UserName).MinimumLength(minimumLength: 6);
        RuleFor(dto => dto.Password).MinimumLength(minimumLength: 6);
    }
}