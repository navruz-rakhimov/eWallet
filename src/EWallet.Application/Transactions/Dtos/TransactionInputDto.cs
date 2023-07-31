using FluentValidation;

namespace EWallet.Application.Transactions.Dtos;

public class TransactionInputDto
{
    public decimal Amount { get; set; }
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
}

public class TransactionInputDtoValidator : AbstractValidator<TransactionInputDto>
{
    public TransactionInputDtoValidator()
    {
        RuleFor(dto => dto.Amount).GreaterThan(0);
    }
}