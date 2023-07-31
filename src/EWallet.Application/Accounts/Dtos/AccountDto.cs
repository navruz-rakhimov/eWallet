using EWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Accounts.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public bool IsIdentified { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
    }
}
