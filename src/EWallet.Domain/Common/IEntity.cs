using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Domain.Common
{
    public interface IEntity<TIndex>
    {
        public TIndex Id { get; set; }
    }
}
