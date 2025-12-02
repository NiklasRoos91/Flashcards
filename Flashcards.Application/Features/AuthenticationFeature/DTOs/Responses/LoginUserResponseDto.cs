namespace Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses
{
    public class LoginUserResponseDto
    {
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
