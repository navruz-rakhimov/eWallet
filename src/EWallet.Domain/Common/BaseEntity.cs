using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Domain.Common
{
    public abstract class BaseEntity<TIndex> : IEntity<TIndex>
    {
        public TIndex Id { get; set; } = default!;
    }
}
