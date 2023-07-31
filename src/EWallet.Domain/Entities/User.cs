using EWallet.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Domain.Entities
{
    public class User : IdentityUser<int>, IAuditableEntity<int>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
