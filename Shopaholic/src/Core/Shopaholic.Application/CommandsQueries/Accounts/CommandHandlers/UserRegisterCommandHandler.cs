using MediatR;
using Shopaholic.Application.Abstraction.Services;
using Shopaholic.Application.CommandsQueries.Accounts.Commands;

namespace Shopaholic.Application.CommandsQueries.Accounts.CommandHandlers
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand>
    {
        private readonly IAccountsService _accountsService;

        public UserRegisterCommandHandler(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            await _accountsService.AccountRegister(request.UserRegisterDto, cancellationToken);
        }
    }
}
