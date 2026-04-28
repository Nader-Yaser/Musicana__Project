using Musicana.Api.Responses;
using Musicana.Api.Requests;
namespace Musicana.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
