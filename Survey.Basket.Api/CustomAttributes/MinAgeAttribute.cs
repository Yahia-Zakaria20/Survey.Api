using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using System.ComponentModel.DataAnnotations;

namespace Survey.Basket.Api.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MinAgeAttribute:ValidationAttribute 
    {
        private readonly int _age;

        public MinAgeAttribute(int Age)
        {
            _age = Age;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                var dataofbirth = (DateTime)value;

                if (DateTime.Today < dataofbirth.AddYears(_age)) 
                {
                    return new ValidationResult(ErrorMessage = $"Error in {validationContext.DisplayName} becouse Min Age is {_age}");
                }                
            }

            return ValidationResult.Success;

        }
    }
}
