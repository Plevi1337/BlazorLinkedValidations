using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BlazorLinkedValidations
{
  public class ValidationLinker : ComponentBase, IDisposable
  {
    [CascadingParameter]
    EditContext EditContext { get; set; }

    [Parameter]
    public ValidationLinkerBase Linker { get; set; }

    private bool _isFiring = false;

    protected override void OnInitialized()
    {
      _ = EditContext ?? throw new ArgumentNullException(nameof(EditContext), "ValidationLinker component needs a cascading EditContext parameter.");
      _ = Linker ?? throw new ArgumentNullException(nameof(Linker));
      EditContext.OnFieldChanged += OnFieldChanged;
    }

    private void OnFieldChanged(object sender, FieldChangedEventArgs e)
    {
      if (!_isFiring)
      {
        _isFiring = true;
        foreach (FieldIdentifier groupedIdentifiers in Linker.GetLinkedFields(e.FieldIdentifier))
        {
          EditContext.NotifyFieldChanged(groupedIdentifiers);
        }
        _isFiring = false;
      }
    }

    public void Dispose()
    {
      EditContext.OnFieldChanged -= OnFieldChanged;
    }
  }
}
