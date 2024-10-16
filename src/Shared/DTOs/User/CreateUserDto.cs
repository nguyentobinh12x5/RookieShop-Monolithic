namespace Shared.DTOs.User
{
    public class CreateUserDto
    {
        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;

        public string Address { get; init; } = null!;

        public string UserName { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Password { get; init; } = null!;

        public string? Role { get; init; }

    }
}
