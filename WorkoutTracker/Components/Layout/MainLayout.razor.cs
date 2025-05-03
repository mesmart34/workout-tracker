using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WorkoutTracker.Shared.Resources;

namespace WorkoutTracker.Components.Layout;

public partial class MainLayout
{
    private bool _sidebarExpanded = true;
    
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

}