namespace EWallet.Application.Users;

public static class UserConstants
{
    public static class Errors
    {
        public static string USER_WITH_GIVEN_ID_DOES_NOT_EXIST(int id) =>
            $"{nameof(USER_WITH_GIVEN_ID_DOES_NOT_EXIST)}; ID = {id}";
    }
}
