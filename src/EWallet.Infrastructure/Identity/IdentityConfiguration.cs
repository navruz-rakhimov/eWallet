namespace EWallet.Infrastructure.Identity;

public class IdentityConfiguration
{
    public bool RequireConfirmedAccount { get; set; }

    public IdentityPasswordConfiguration Password { get; set; }

    public class IdentityPasswordConfiguration
    {
        public bool RequireDigit { get; init; }

        public bool RequireLowercase { get; init; }

        public int RequiredLength { get; init; }
    }
}