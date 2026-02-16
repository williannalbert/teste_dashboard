using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AgroSolutions.Identity.Web.Infrastructure.Auth;

public class MockAuthStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Produtor Rural"),
            new Claim(ClaimTypes.Email, "produtor@fazenda.com"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "MockAuth");

        var user = new ClaimsPrincipal(identity);

        return Task.FromResult(new AuthenticationState(user));
    }
}