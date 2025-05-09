using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using WorkoutTracker.Auth;
using WorkoutTracker.Shared.Resources;

namespace WorkoutTracker.Components.Layout;

public partial class MainLayout
{
    private bool _sidebarExpanded = true;
    
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    private AuthService AuthService { get; set; } = null!;
    
    [Inject] 
    private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    
    [Inject] 
    private UserContext UserContext { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var authState = await ((WorkoutTrackerAuthStateProvider)AuthStateProvider).GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is { IsAuthenticated: false })
            {
                NavigationManager.NavigateTo("/login");
            }

        }
        catch(Exception ex){
            NavigationManager.NavigateTo("/login", true);
        }
    }
    
    private async Task LogOut()
    {
        await AuthService.Logout();
        NavigationManager.NavigateTo("/login", true);
    }
}