using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Common.Responses
{
    public interface IBaseResponse
    {
        int Status { get; }
        bool IsSuccess { get; }
        string Message { get; }
    }
}
