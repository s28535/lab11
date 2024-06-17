using WebApplication1.DTO;

namespace WebApplication1.Services;

public interface IUserService
{
    public Task RegisterUser(RegisterRequest registerRequest);
    public Task<object?> LoginUser(LoginRequest loginRequest);
    public Task<object?> RefreshUserToken(RefreshTokenRequest refreshTokenRequest);
}