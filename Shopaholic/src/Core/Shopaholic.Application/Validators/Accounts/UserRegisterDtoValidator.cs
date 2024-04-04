using FluentValidation;
using Shopaholic.Application.DTOs.Accounts;

namespace Shopaholic.Application.Validators.Accounts
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(dto => dto.userName)
                .NotEmpty()
                .MaximumLength(50);

            //RuleFor(dto => dto.FirstName)
            //    .NotEmpty()
            //    .MaximumLength(50);

            //RuleFor(dto => dto.LastName)
            //    .NotEmpty()
            //    .MaximumLength(50);

            RuleFor(dto => dto.email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(dto => dto.password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(dto => dto.passwordConfirm)
                .NotEmpty()
                .Equal(dto => dto.password).WithMessage("Password and confirm password do not match.");

            RuleFor(dto => dto.phonenumber)
                .NotEmpty()
                .Matches(@"^[0-9]+$");

            RuleFor(dto => dto.role)
                .IsInEnum();
        }
    }
}
