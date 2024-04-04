using Shopaholic.Domain.Enums;

namespace Shopaholic.Application.DTOs.Accounts
{
    public record UserRegisterDto(string userName,
                                  string email,
                                  string password,
                                  string passwordConfirm,
                                  string phonenumber,
                                  Roles role);
}
