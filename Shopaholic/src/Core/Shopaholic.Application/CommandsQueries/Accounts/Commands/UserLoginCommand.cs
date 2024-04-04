using MediatR;
using Shopaholic.Application.DTOs.Accounts;
using Shopaholic.Application.DTOs.Response;

namespace Shopaholic.Application.CommandsQueries.Accounts.Commands
{
    public class UserLoginCommand : IRequest<TokenResponseDto>
    {
        public UserSignInDto UserSignInDto { get; set; }
        public UserLoginCommand(UserSignInDto userSignInDto)
        {
            UserSignInDto = userSignInDto;
        }
    }
}
