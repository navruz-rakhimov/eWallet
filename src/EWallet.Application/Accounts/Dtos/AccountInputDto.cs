using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace EWallet.Application.Accounts.Dtos
{
    public class AccountInputDto
    {
        public decimal Balance { get; set; }
        public bool IsIdentified { get; set; }
        public int UserId { get; set; }
    }

    public class AccountInputDtoValidator : AbstractValidator<AccountInputDto>
    {
        public AccountInputDtoValidator()
        {
            RuleFor(dto => dto.Balance)
                .LessThanOrEqualTo(AccountConstants.MaxBalanceForIdentifiedAccount)
                .When(dto => dto.IsIdentified);

            RuleFor(dto => dto.Balance)
                .LessThanOrEqualTo(AccountConstants.MaxBalanceForNotIdentifiedAccount)
                .When(dto => !dto.IsIdentified);
        }
    }
}
