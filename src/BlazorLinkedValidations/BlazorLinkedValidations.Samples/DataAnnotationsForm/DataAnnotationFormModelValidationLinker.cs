namespace BlazorLinkedValidations.Samples.DataAnnotationsForm
{
  public class DataAnnotationFormModelValidationLinker : ValidationLinkerBase
  {
    public DataAnnotationFormModelValidationLinker(DataAnnotationFormModel model)
    {
      LinkForward(() => model.IntegerProperty, () => model.StringProperty, () => model.DateProperty);
    }
  }
}
