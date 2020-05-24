using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace BlazorLinkedValidations
{
  /// <summary>
  /// Validation linker component, fires a changed event for all the linked fields.
  /// The events fired by this component won't fire another round of events. Only the direct links work.
  /// </summary>
  public class ValidationLinker : ComponentBase, IDisposable
  {

    /// <summary>
    /// Cascading Edit Context parameter, if null the OnInitialized method throws an ArgumentNullException.
    /// </summary>
    [CascadingParameter]
    public EditContext EditContext { get; set; } = null!;

    /// <summary>
    /// Linker parameter, if null the OnInitialized method throws an ArgumentNullException
    /// </summary>
    [Parameter]
    public ValidationLinkerBase Linker { get; set; } = null!;

    /// <summary>
    /// Indicates whether the this component is firing the changed events or another source.
    /// </summary>
    private bool _isFiring = false;

    /// <summary>
    /// Checks the component parameters for nullability and subscribes to the FieldChanged event of the EditContext.
    /// </summary>
    protected override void OnInitialized()
    {
      _ = EditContext ?? throw new ArgumentNullException(nameof(EditContext), "ValidationLinker component needs a cascading EditContext parameter.");
      _ = Linker ?? throw new ArgumentNullException(nameof(Linker));
      EditContext.OnFieldChanged += OnFieldChanged;
    }

    /// <summary>
    /// Field Change event handler. Fires a changed event for every linked field.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnFieldChanged(object? sender, FieldChangedEventArgs e)
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

    /// <summary>
    /// Unsubscribes from the FieldChanged event of the EditContext.
    /// </summary>
    public void Dispose()
    {
      EditContext.OnFieldChanged -= OnFieldChanged;
    }
  }
}
