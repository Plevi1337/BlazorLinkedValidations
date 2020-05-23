using FluentValidation;

namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsChildValidator : AbstractValidator<FluentValidationsFormExampleModelChild>
  {
    public FluentValidationsChildValidator()
    {
      RuleFor(x => x.Number).GreaterThan(0);
      RuleFor(x => x.Number).NotNull();
      RuleFor(x => x.City).NotNull();
      RuleFor(x => x.PostalCode).NotNull();
      RuleFor(x => x.Street).NotNull();
    }
  }
}
