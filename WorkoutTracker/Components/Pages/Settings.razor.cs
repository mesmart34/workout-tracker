using Microsoft.AspNetCore.Components;
using WorkoutTracker.Auth;

namespace WorkoutTracker.Components.Pages;

public partial class Settings : ComponentBase
{
    [Inject] 
    private AuthService AuthService { get; set; } = null!;
    
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    private async Task Logout()
    {
        await AuthService.Logout();
        NavigationManager.NavigateTo("/login", true);
    }
}