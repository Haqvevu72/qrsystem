using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using qrsystem.Data;
using qrsystem.Models.Dtos.Auth;
using qrsystem.Models.Entities;
using qrsystem.Services.JWT;
using LoginRequest = qrsystem.Models.Dtos.Auth.LoginRequest;
using RegisterRequest = qrsystem.Models.Dtos.Auth.RegisterRequest;

namespace qrsystem.Services.AuthServices;

public class AuthService(QrSystemDB context , ITokenService tokenService , IPasswordService passwordService): IAuthService
{
    private readonly QrSystemDB _context = context;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPasswordService _passwordService = passwordService;
    
    
    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var isUserExist = await _context.Users.AnyAsync(u => u.Email == request.Email);

        if (isUserExist) throw new InvalidOperationException("Username is already registered.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return "User registered successfully.";
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !_passwordService.VerifyPassword(user.PasswordHash, request.Password)) return null!;


        var accessToken = _tokenService.GenerateToken(user);
        var refreshToken = GenerateRefreshToken();


        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.Now.AddDays(1);
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenResponse?> RefreshTokenAsync(RefreshRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

        if (user == null) return null;
        if (user.RefreshTokenExpiry <= DateTime.Now)
        {
            user.RefreshToken = string.Empty;
            user.RefreshTokenExpiry = DateTime.MinValue;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return null;
        }

        var newAccessToken = _tokenService.GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.Now.AddDays(1);
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}