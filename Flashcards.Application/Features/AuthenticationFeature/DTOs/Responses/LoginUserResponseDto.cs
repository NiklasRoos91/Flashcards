namespace Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses
{
    public class LoginUserResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
