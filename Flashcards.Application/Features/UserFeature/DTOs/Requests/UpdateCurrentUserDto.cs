namespace Flashcards.Application.Features.UserFeature.DTOs.Requests
{
    public class UpdateCurrentUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
