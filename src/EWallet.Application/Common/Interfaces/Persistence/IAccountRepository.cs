using EWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Interfaces.Persistence
{
    public interface IAccountRepository : IBaseRepository<Account, int>
    {
    }
}
