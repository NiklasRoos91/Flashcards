using System.Security.Claims;

namespace Flashcards.Api.Helpers
{
    public static class UserHelper
    {
        public static Guid GetCurrentUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            // Validate the format of the User ID claim
            // Assuming the User ID is a GUID, we can use Guid.TryParse to ensure it's valid
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid User ID claim format.");
            }

            return userId;
        }
    }
}
