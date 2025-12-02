using Flashcards.Application.Features.UserFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.UserFeature.DTOs.Validators
{
    public class UpdateCurrentUserDtoValidator : AbstractValidator<UpdateCurrentUserDto>
    {
        public UpdateCurrentUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .Length(2, 50)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName))
                .WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(x => x.LastName)
                .Length(2, 50)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName))
                .WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(x => x.Address)
                .Length(5, 100)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage("Address must be between 5 and 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[0-9]{10,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Phone number must only contain digits and may start with '+'. No spaces or special characters are allowed. Example: +46701234567");
        }
    }
}
