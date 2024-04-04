using Microsoft.AspNetCore.Identity;
using Shopaholic.Application.Abstraction.Services;
using Shopaholic.Application.DTOs.Accounts;
using Shopaholic.Application.DTOs.Response;
using Shopaholic.Domain.Identity;
using Shopaholic.Persistence.Exceptions;
using System.Text;
using System.Transactions;

namespace Shopaholic.Persistence.Implementations.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; 
        private readonly IJWTService _jwtService;

        public AccountsService(UserManager<AppUser> userManager, IJWTService jwtService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }

        public async Task AccountRegister(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(nameof(userRegisterDto));
            using (TransactionScope scope = new(
                TransactionScopeOption.Required,
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    AppUser newUser = new()
                    {
                        Email = userRegisterDto.email,
                        PhoneNumber = userRegisterDto.phonenumber,
                        UserName = userRegisterDto.userName
                    };
                    IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegisterDto.password);

                    if (!identityResult.Succeeded)
                        HandleIdentityErrors(identityResult);

                    IdentityResult roleResult = await _userManager.AddToRoleAsync(newUser, userRegisterDto.role.ToString());

                    if (!roleResult.Succeeded)
                        HandleIdentityErrors(roleResult);

                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<TokenResponseDto> Login(UserSignInDto userSignInDto, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(userSignInDto.email) ?? throw new NotFoundException("User with given email was not found");

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, userSignInDto.password, true);

            if (!signInResult.Succeeded)
                throw new AccountException("Failed to sign in user");

            return await TokenGenerator(user);

        }
        private void HandleIdentityErrors(IdentityResult identityResult)
        {
            StringBuilder identityErrors = new();
            foreach (var error in identityResult.Errors)
            {
                identityErrors.AppendLine(error.Description);
            }
            throw new AccountException(nameof(identityErrors));
        }
        private async Task<TokenResponseDto> TokenGenerator(AppUser user)
        {
            TokenResponseDto responseDto = await _jwtService.CreateTokenAsync(user);

            user.RefreshToken = responseDto.refreshToken;
            user.RefreshTokenExpiration = responseDto.refreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return responseDto;
        }
    }
}
