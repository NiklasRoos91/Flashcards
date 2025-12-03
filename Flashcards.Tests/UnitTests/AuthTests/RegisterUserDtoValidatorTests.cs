using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Validators;
using FluentValidation.TestHelper;

namespace Flashcards.Tests.AuthenticationTests
{
    public class RegisterUserDtoValidatorTests
    {
        private RegisterUserDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RegisterUserDtoValidator();
        }

        [Test]
        public void Validator_Should_Have_Error_When_FirstName_Is_Empty()
        {
            var dto = new RegisterUserDto { FirstName = "" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void Validator_Should_Have_Error_When_Password_Does_Not_Meet_Criteria()
        {
            var dto = new RegisterUserDto { Password = "short" }; // För kort och saknar krav
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Validator_Should_Not_Have_Error_For_Valid_Dto()
        {
            var dto = new RegisterUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Username = "johndoe",
                Password = "Valid123!"
            };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
