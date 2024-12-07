using FluentValidation;
using Survey.Basket.Api.Dto;
using System;

namespace Survey.Basket.Api.Data.Validations
{
    public class PollDtoValidator:AbstractValidator<PollDto>
    {
        public PollDtoValidator()
        {
            RuleFor(p => p.Summary)
                .NotNull()
                .Length(3, 1500);

            RuleFor(p => p.Titel)
               .NotNull()
               .Length(3, 1500);

            RuleFor(p => p.StartsAt)
                .NotNull()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(p => p)
                .Must(IsValidEnddate())
                .WithName(nameof(PollDto.EndsAt))
                .WithMessage("the {PropertyName} Must be more than StartDate");
        }

        private static Func<PollDto, bool> IsValidEnddate()
        {
            return p => p.EndsAt > p.StartsAt;
        }
    }
}
