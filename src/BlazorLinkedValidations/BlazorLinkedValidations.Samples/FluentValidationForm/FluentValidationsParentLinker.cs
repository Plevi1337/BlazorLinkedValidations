namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsParentLinker : ValidationLinkerBase
  {
    public FluentValidationsParentLinker(FluentValidationsFormExampleModelParent model)
    {
      LinkForward(() => model.Number1, () => model.Number2, () => model.Sum);
      LinkForward(() => model.Number2, () => model.Number1, () => model.Sum);
      LinkForward(() => model.Child.City, () => model.Sum);
      LinkForward(() => model.Sum, () => model.Child.Street);
      SetChildLinker(new FluentValidationsChildLinker(model.Child));
    }
  }
}
