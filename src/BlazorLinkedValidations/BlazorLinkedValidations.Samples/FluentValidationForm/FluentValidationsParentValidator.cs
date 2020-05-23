using FluentValidation;

namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsParentValidator : AbstractValidator<FluentValidationsFormExampleModelParent>
  {
    public FluentValidationsParentValidator()
    {
      RuleFor(x => x.Child).SetValidator(new FluentValidationsChildValidator());
      RuleFor(x => x.Number1).Must((model, prop) =>
      model.Number1.HasValue && model.Number2.HasValue && model.Sum.HasValue && model.Number1 + model.Number2 == model.Sum)
        .WithMessage("Number1 + Number2 must be equal to Sum");
      RuleFor(x => x.Number2).Must((model, prop) =>
      model.Number1.HasValue && model.Number2.HasValue && model.Sum.HasValue && model.Number1 + model.Number2 == model.Sum)
        .WithMessage("Number1 + Number2 must be equal to Sum");
      RuleFor(x => x.Sum).Must((model, prop) =>
      model.Number1.HasValue && model.Number2.HasValue && model.Sum.HasValue && model.Number1 + model.Number2 == model.Sum)
        .WithMessage("Number1 + Number2 must be equal to Sum");
    }
  }
}
