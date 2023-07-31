namespace EWallet.Application.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
}