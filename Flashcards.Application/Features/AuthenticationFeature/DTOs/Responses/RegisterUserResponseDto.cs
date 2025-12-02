namespace Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses
{
    public class RegisterUserResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
