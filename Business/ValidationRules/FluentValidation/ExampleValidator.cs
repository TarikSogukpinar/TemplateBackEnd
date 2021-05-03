using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ExampleValidator : AbstractValidator<Example>
    {
        public ExampleValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.LastName).NotEmpty();
        }
    }
}