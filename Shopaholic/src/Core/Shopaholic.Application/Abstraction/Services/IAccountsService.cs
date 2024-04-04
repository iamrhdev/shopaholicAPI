using Shopaholic.Application.DTOs.Accounts;
using Shopaholic.Application.DTOs.Response;

namespace Shopaholic.Application.Abstraction.Services
{
    public interface IAccountsService
    {
        Task AccountRegister(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);
        Task<TokenResponseDto> Login(UserSignInDto userSignInDto,CancellationToken cancellationToken);
    }
}
