using Shopaholic.Application.DTOs.Response;
using Shopaholic.Domain.Identity;

namespace Shopaholic.Application.Abstraction.Services
{
    public interface IJWTService
    {
        Task<TokenResponseDto> CreateTokenAsync(AppUser user);
    }
}
