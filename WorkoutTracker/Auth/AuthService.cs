using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using WorkoutTracker.Application.Contracts;
using WorkoutTracker.Application.Service;
using WorkoutTracker.Auth.Requests;
using WorkoutTracker.Common.Models;
using WorkoutTracker.Domain.Entities;

namespace WorkoutTracker.Auth;

public class AuthService
{
    private readonly UserService _userService;
    private readonly IConfiguration _configuration;
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly IUserContext _userContext;
    private const string UserClaim = "User";

    public AuthService(UserService userService, IConfiguration configuration, ProtectedLocalStorage protectedLocalStorage, IUserContext userContext)
    {
        _userService = userService;
        _configuration = configuration;
        _protectedLocalStorage = protectedLocalStorage;
        _userContext = userContext;
    }
    
    public async Task<string> Login(LoginRequest request)
    {
        var users = await _userService.Get(x => x.Email == request.Email && x.Password == request.Password);
        
        if (!users.Any())
        {
            throw new Exception($"User with email {request.Email} not found or wrong password");
        }
        
        var user = users.First();
           
        var token = GenerateJwtToken(user.Email, user.Id);

        FillContext(user);
        
        return token;
    }
    
    public async Task<bool> Register(RegisterModel request)
    {
        var user = (await _userService.Get(x => x.Email == request.Email)).FirstOrDefault();
        if (user != null)
        {
            return false;
        }

        await _userService.Add(new UserEntity()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            Email = request.Email,
        });

        return true;
    }

    private void FillContext(UserEntity user)
    {
        _userContext.FirstName = user.FirstName;
        _userContext.LastName = user.LastName;
        _userContext.User = user;
        _userContext.Id = user.Id;
    }

    public async Task Logout()
    {
        await _protectedLocalStorage.DeleteAsync(TokenConsts.TokenKey);
    }

    public async Task TryRestoringSession(ClaimsPrincipal userClaims)
    {
        var value = userClaims.Claims.FirstOrDefault(x => x.Type == WorkoutTrackerClaims.UserId)?.Value;
        if (value != null)
        {
            var userId = Guid.Parse(value);
            var user = await _userService.Get(userId);
            if (user != null)
            {
                FillContext(user);
            }
        }
    }

    private string GenerateJwtToken(string email, Guid id)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.Email, email),
            new(WorkoutTrackerClaims.UserId, id.ToString()),
            new(ClaimTypes.Role, UserClaim)
        };

        var jwtToken = _configuration.GetValue<string>("jwt:Token");

        if (jwtToken == null)
        {
            throw new InvalidOperationException("JWT Token is missing.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:Issuer"],
            audience: _configuration["jwt:Audience"],
            claims: claims,
            expires:  DateTime.Now.AddDays(10),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}