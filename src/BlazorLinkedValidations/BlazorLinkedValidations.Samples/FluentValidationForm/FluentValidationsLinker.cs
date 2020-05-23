namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsLinker : ValidationLinkerBase
  {
    public FluentValidationsLinker(FluentValidationsFormExampleModelParent model)
    {
      Link(() => model.Number1, () => model.Number2, () => model.Sum);
      Link(() => model.Number2, () => model.Number1, () => model.Sum);
      Link(() => model.Child.City, () => model.Sum);
      Link(() => model.Sum, () => model.Child.Street);
    }
  }
}
