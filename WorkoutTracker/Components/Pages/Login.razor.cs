using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using WorkoutTracker.Auth;
using WorkoutTracker.Auth.Requests;
using WorkoutTracker.Auth.Responses;
using WorkoutTracker.Common.Models;

namespace WorkoutTracker.Components.Pages;

public partial class Login : ComponentBase
{
    private LoginModel _loginModel = new();

    [Inject] 
    private AuthService AuthService { get; set; } = null!;

    [Inject] 
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;
    
    private async Task OnLogin(LoginArgs args)
    {
        var request = new LoginRequest()
        {
            Email = args.Username,
            Password = args.Password
        };

        var token = await AuthService.Login(request);

        if (!string.IsNullOrEmpty(token))
        {
            await ((WorkoutTrackerAuthStateProvider)AuthenticationStateProvider).MarkAsAuthenticated(token);
            NavigationManager.NavigateTo("/exercises");
        }
    }
}