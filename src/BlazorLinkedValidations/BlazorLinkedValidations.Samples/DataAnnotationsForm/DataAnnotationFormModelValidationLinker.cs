namespace BlazorLinkedValidations.Samples.DataAnnotationsForm
{
  public class DataAnnotationFormModelValidationLinker : ValidationLinkerBase
  {
    public DataAnnotationFormModelValidationLinker(DataAnnotationFormModel model)
    {
      Link(() => model.IntegerProperty, () => model.StringProperty, () => model.DateProperty);
    }
  }
}
