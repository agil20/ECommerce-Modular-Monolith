using Modules.Identity.Contracts.AuthDTOs;

namespace Modules.Identity.Contracts.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}