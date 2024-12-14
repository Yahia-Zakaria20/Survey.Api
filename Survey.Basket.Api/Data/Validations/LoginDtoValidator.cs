using FluentValidation;
using Survey.Basket.Api.Dto;

namespace Survey.Basket.Api.Data.Validations
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty();
        }
    }
}
