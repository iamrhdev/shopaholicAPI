using MediatR;
using Shopaholic.Application.Abstraction.Services;
using Shopaholic.Application.CommandsQueries.Accounts.Commands;
using Shopaholic.Application.DTOs.Response;

namespace Shopaholic.Application.CommandsQueries.Accounts.CommandHandlers
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, TokenResponseDto>
    {
        private readonly IAccountsService _accountsService;

        public UserLoginCommandHandler(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<TokenResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            return await _accountsService.Login(request.UserSignInDto, cancellationToken);
        }
    }
}
