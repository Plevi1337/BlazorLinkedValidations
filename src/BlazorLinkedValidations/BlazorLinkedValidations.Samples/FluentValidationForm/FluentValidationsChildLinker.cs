namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsChildLinker : ValidationLinkerBase
  {
    public FluentValidationsChildLinker(FluentValidationsFormExampleModelChild model)
    {
      LinkForward(() => model.City, () => model.Number);
    }
  }
}
