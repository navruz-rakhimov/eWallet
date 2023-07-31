using EWallet.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Domain.Entities
{
    public class Account : BaseAuditableEntity<int>
    {
        public decimal Balance { get; set; }
        public bool IsIdentified { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public ICollection<Transaction> Deposits { get; set; } = new List<Transaction>();
        public ICollection<Transaction> Withdrawals { get; set; } = new List<Transaction>();
    }
}
