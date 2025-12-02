namespace Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests
{
    public class RegisterUserDto
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
