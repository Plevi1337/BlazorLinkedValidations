namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsLinker : ValidationLinkerBase
  {
    public FluentValidationsLinker(FluentValidationsFormExampleModelParent model)
    {
      LinkForward(() => model.Number1, () => model.Number2, () => model.Sum);
      LinkForward(() => model.Number2, () => model.Number1, () => model.Sum);
      LinkForward(() => model.Child.City, () => model.Sum);
      LinkForward(() => model.Sum, () => model.Child.Street);
    }
  }
}
