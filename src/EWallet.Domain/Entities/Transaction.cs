using EWallet.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Domain.Entities
{
    public class Transaction : BaseAuditableEntity<int>
    {
        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public Account FromAccount { get; set; } = default!;
        public Account ToAccount { get; set; } = default!;
    }
}
