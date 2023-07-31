using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Application.Accounts
{
    public static class AccountConstants
    {
        public static class Errors
        {
            public static string ACCOUNT_WITH_GIVEN_ID_DOES_NOT_EXIST(int id) => $"{nameof(ACCOUNT_WITH_GIVEN_ID_DOES_NOT_EXIST)}; ID = {id}";
        }

        public const decimal MaxBalanceForIdentifiedAccount = 100000;
        public const decimal MaxBalanceForNotIdentifiedAccount = 10000;
    }
}
