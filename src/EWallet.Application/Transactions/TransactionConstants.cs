namespace EWallet.Application.Transactions;

public static class TransactionConstants
{
    public static class Errors
    {
        public const string InsufficientFunds = "Insufficient funds";
        public const string ReceiverAccountBalanceExceedsLimit = "Receiver balance exceeds allowed limit";
    }
}