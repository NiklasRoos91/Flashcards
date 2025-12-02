namespace Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests
{
    public class LoginUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
