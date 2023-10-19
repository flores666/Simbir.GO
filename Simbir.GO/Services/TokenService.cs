using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Simbir.GO.DataAccess;
using Simbir.GO.DataAccess.Objects;
using Simbir.GO.Models;
using Simbir.GO.Services.Interfaces;

namespace Simbir.GO.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _db;

    public TokenService(IConfiguration configuration, AppDbContext db)
    {
        _configuration = configuration;
        _db = db;
    }

    public string GenerateJwt(string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
            }),
            Expires = DateTime.UtcNow.AddSeconds(double.Parse(_configuration["Jwt:LifetimeMinutes"])),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        using (var serviceProvider = new RNGCryptoServiceProvider())
        {
            var randomBytes = new byte[64];
            serviceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiryTime = DateTime.UtcNow.AddDays(double.Parse(_configuration["RefreshToken:LifetimeDays"]))
            };
        }
    }

    public Response RefreshJwt(string jwt)
    {
        var userName = GetUsernameFromJwt(jwt);
        var user = _db.Users
            .Include(u => u.RefreshToken)
            .FirstOrDefault(u => u.Name == userName);

        if (user == null)
            return new Response(StatusCodes.Status404NotFound, "Пользователь не найден");
        if (user.RefreshToken == null || user.RefreshToken.IsExpired)
            return new Response(StatusCodes.Status401Unauthorized, "Требуется авторизация");

        var newRefreshToken = GenerateRefreshToken();
        
        if (!string.IsNullOrEmpty(user.RefreshToken?.Token)) _db.RefreshTokens.Remove(user.RefreshToken);
        user.RefreshToken = newRefreshToken;
        _db.Update(user);
        _db.SaveChanges();

        var tokenModel = new TokenModel
        {
            JWT = GenerateJwt(userName),
            RefreshToken = newRefreshToken.Token,
            RefreshTokenExpTime = newRefreshToken.ExpiryTime
        };
        
        return new Response(StatusCodes.Status200OK, tokenModel);
    }

    private string GetUsernameFromJwt(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.ReadJwtToken(jwt);
        var claims = token.Claims;
        var username = claims?.FirstOrDefault(c => c.Type == "unique_name")?.Value;

        return username ?? null;
    }
}