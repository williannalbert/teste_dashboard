using AgroSolutions.Identity.Web.Application.DTOs;
using AgroSolutions.Identity.Web.Application.Interfaces;

namespace AgroSolutions.Identity.Web.Infrastructure.Services;

public class AuthServiceMock : IAuthService
{
    public static TokenResponse? CurrentSession { get; private set; }

    public async Task<TokenResponse?> LoginAsync(LoginRequest request)
    {
        await Task.Delay(1000); 

        if (request.Email == "admin@agrosolutions.com" && request.Password == "123456")
        {
            CurrentSession = new TokenResponse
            {
                AccessToken = "eyJMOCK.TOKEN.JWT",
                RefreshToken = "refreshtoken123",
                ExpiresIn = 3600,
                User = new UserDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = request.Email,
                    Name = "Produtor Admin"
                }
            };
            return CurrentSession;
        }
        return null;
    }

    public async Task LogoutAsync()
    {
        CurrentSession = null;
        await Task.CompletedTask;
    }
}