using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorLinkedValidations.Samples.DataAnnotationsForm
{
  public class DataAnnotationFormModel
  {
    [Required]
    public int? IntegerProperty { get; set; }

    [Required]
    [StringLength(3)]
    public string StringProperty { get; set; }

    [Required]
    public DateTime? DateProperty { get; set; }
  }
}
