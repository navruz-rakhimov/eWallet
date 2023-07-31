using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Domain.Common
{
    public abstract class BaseAuditableEntity<TIndex> : BaseEntity<TIndex>, IAuditableEntity<TIndex>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
    }
}
