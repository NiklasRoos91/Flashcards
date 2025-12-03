using System.Net.Http.Json;
using FluentAssertions;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Flashcards.Tests.IntegrationTests
{
    [TestFixture]
    public class AuthIntegrationTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            // Skapa factory och klient
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            // Disposa resurser efter varje test
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task RegisterAndLogin_ShouldReturnToken()
        {
            // Arrange
            var registerUser = new RegisterUserDto
            {
                FirstName = "Test",
                LastName = "User",
                Username = "testuser123",
                Email = "testuser123@example.com",
                Password = "Password123!"
            };

            var loginUser = new LoginUserDto
            {
                Email = registerUser.Email,
                Password = registerUser.Password
            };

            // Act: Register user
            var registerResponse = await _client.PostAsJsonAsync("/api/Auth/register", registerUser);

            // Kontrollera att statuscode är 2xx
            registerResponse.EnsureSuccessStatusCode();

            // Mappa respons till RegisterUserResponseDto
            var registerResult = await registerResponse.Content.ReadFromJsonAsync<RegisterUserResponseDto>();
            registerResult.Should().NotBeNull();
            registerResult.Email.Should().Be(registerUser.Email);
            registerResult.Username.Should().Be(registerUser.Username);
            registerResult.UserId.Should().NotBeEmpty();

            // Act: Login user
            var loginResponse = await _client.PostAsJsonAsync("/api/Auth/login", loginUser);
            loginResponse.EnsureSuccessStatusCode();

            var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginUserResponseDto>();
            loginResult.Should().NotBeNull();
            loginResult.Token.Should().NotBeNullOrEmpty();

            // Optional: enkel kontroll av JWT-format (3 delar separerade med .)
            loginResult.Token.Split('.').Length.Should().Be(3);
        }
    }
}
