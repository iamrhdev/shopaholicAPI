using MediatR;
using Shopaholic.Application.DTOs.Accounts;

namespace Shopaholic.Application.CommandsQueries.Accounts.Commands
{
    public class UserRegisterCommand : IRequest
    {
        public UserRegisterDto UserRegisterDto { get; set; }
        public UserRegisterCommand(UserRegisterDto userRegisterDto)
        {
            UserRegisterDto = userRegisterDto;
        }
    }
}
