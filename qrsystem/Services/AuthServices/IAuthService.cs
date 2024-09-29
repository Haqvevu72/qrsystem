using Microsoft.AspNetCore.Identity.Data;
using qrsystem.Models.Dtos.Auth;
using LoginRequest = qrsystem.Models.Dtos.Auth.LoginRequest;
using RefreshRequest = qrsystem.Models.Dtos.Auth.RefreshRequest;
using RegisterRequest = qrsystem.Models.Dtos.Auth.RegisterRequest;

namespace qrsystem.Services.AuthServices;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<TokenResponse> LoginAsync(LoginRequest request);
    Task<TokenResponse?> RefreshTokenAsync(RefreshRequest request);
}