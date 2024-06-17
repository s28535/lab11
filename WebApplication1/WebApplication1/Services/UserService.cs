using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTO;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public async Task RegisterUser(RegisterRequest registerRequest)
    {
        var hashedPasswordAndSalt = GetHashedPassword(registerRequest.Password);
        

        var user = new User()
        {
            Email = registerRequest.Email,
            Login = registerRequest.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        await _userRepository.AddUser(user);
    }

    public async Task<object?> LoginUser(LoginRequest loginRequest)
    {
        User user = await _userRepository.GetUserByLogin(loginRequest.Login);

        if (user == null)
        {
            return null;
        }

        string passwordHashFromDb = user.Password;
        string curHashedPassword = GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);
        
        if (passwordHashFromDb != curHashedPassword)
        {
            return null;
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
        
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges();

        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        };
    }

    public Task<object?> RefreshUserToken(RefreshTokenRequest refreshTokenRequest)
    {
        throw new NotImplementedException();
    }
    
    private Tuple<string, string> GetHashedPassword(string password)
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        ));

        string saltBase64 = Convert.ToBase64String(salt);

        return new Tuple<string, string>(hashed, saltBase64);
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private string GetHashedPasswordWithSalt(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);

        string currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return currentHashedPassword;
    }
}