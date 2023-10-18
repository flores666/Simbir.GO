using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Simbir.GO.DataAccess;
using Simbir.GO.DataAccess.Objects;
using Simbir.GO.Models;
using Simbir.GO.Repositories.Interfaces;

namespace Simbir.GO.Repositories;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration config)
    {
        _db = context;
        _configuration = config;
    }

    public Response Register(string name, string password)
    {
        if (Get(name) != null) return new Response(StatusCodes.Status409Conflict, "Такой пользователь уже существует");
        var passwordHash = PasswordHasher.Hash(password);
        var user = new User { Name = name, PasswordHash = passwordHash };
        _db.Users.Add(user);
        _db.SaveChanges();
        return new Response(StatusCodes.Status201Created, "Пользователь успешно зарегистрировался");
    }
    
    public User Get(string name)
    {
        return _db.Users.FirstOrDefault(u => u.Name == name);
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Response Authenticate(LoginModel model)
    {
        var user = _db.Users.Include(u => u.RefreshToken).FirstOrDefault(u => u.Name == model.Name);
        if (user == null) return new Response(StatusCodes.Status404NotFound, "Пользователь не найден");
        if (!PasswordHasher.Validate(model.Password, user.PasswordHash))
            return new Response(StatusCodes.Status400BadRequest, "Данные не верны или отсутствуют");

        var jwtToken = GenerateJwt(model.Name);
        var refreshToken = GenerateRefreshToken();

        if (!string.IsNullOrEmpty(user.RefreshToken?.Token)) _db.Tokens.Remove(user.RefreshToken);
        
        user.RefreshToken = refreshToken;
        _db.Update(user);
        _db.SaveChanges();

        var tokenModel = new TokenModel
        {
            JWT = jwtToken,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpTime = refreshToken.ExpiryTime
        };
        
        return new Response(StatusCodes.Status200OK, tokenModel);
    }

    public Response Logout(string name)
    {
        var user = _db.Users.Include(u => u.RefreshToken).FirstOrDefault(u => u.Name == name);
        if (user == null) return new Response(StatusCodes.Status404NotFound, "Пользователь не найден");
        if (user.RefreshToken == null) return new Response(StatusCodes.Status400BadRequest, "Некорректный запрос");
        _db.Tokens.Remove(user.RefreshToken);
        _db.SaveChanges();

        return new Response(StatusCodes.Status200OK, "Пользователь успешно деавторизовался");
    }

    private string GenerateJwt(string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken()
    {
        using (var serviceProvider = new RNGCryptoServiceProvider())
        {
            var randomBytes = new byte[64];
            serviceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiryTime = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}