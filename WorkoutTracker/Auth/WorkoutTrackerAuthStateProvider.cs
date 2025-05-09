using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WorkoutTracker.Auth;

public class WorkoutTrackerAuthStateProvider(ProtectedLocalStorage protectedLocalStorage, AuthService authService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var sessionModel = (await protectedLocalStorage.GetAsync<string>(TokenConsts.TokenKey)).Value;
        var identity = sessionModel == null ? new ClaimsIdentity() : GetClaimsIdentity(sessionModel);
        var user = new ClaimsPrincipal(identity);
        await authService.TryRestoringSession(user);
        return new AuthenticationState(user);
    }

    private ClaimsIdentity GetClaimsIdentity(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var claims = jwtToken.Claims;
        return new ClaimsIdentity(claims, TokenConsts.AuthenticationType);
    }

    public async Task MarkAsAuthenticated(string token)
    {
        await protectedLocalStorage.SetAsync(TokenConsts.TokenKey, token);
        var identity = GetClaimsIdentity(token);
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}