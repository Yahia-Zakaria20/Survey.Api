using FluentValidation;
using Survey.Basket.Api.Dto;

namespace Survey.Basket.Api.Data.Validations
{
    public class PollDtoValidator:AbstractValidator<PollDto>
    {
        public PollDtoValidator()
        {
            RuleFor(p => p.Summary)
                .NotNull();

            RuleFor(p => p.Titel)
               .NotNull();
        }
    }
}
