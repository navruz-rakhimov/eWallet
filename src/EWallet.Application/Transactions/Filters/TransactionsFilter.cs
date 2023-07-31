namespace EWallet.Application.Transactions.Filters;

public class TransactionsFilter
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}