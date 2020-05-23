namespace BlazorLinkedValidations.Samples.FluentValidationForm
{
  public class FluentValidationsFormExampleModelParent
  {
    public FluentValidationsFormExampleModelChild Child { get; set; } = new FluentValidationsFormExampleModelChild();

    public int? Number1 { get; set; }

    public int? Number2 { get; set; }

    public int? Sum { get; set; }
  }
}
