using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace BlazorLinkedValidations
{
  /// <summary>
  /// This is the abstract base class of the linkers.
  /// </summary>
  public abstract class ValidationLinkerBase
  {
    private readonly Dictionary<FieldIdentifier, IEnumerable<FieldIdentifier>> _links = new Dictionary<FieldIdentifier, IEnumerable<FieldIdentifier>>();

    /// <summary>
    /// Adds the targets to the list of fields to validate when the origin field gets edited.
    /// </summary>
    /// <param name="origin">The origin of the links.</param>
    /// <param name="targets">The targets of the links.</param>
    protected void LinkForward(Expression<Func<object>> origin, params Expression<Func<object>>[] targets)
    {
      _ = origin ?? throw new ArgumentNullException(nameof(origin));
      _ = targets ?? throw new ArgumentNullException(nameof(targets));

      FieldIdentifier originField = FieldIdentifier.Create(origin);
      IEnumerable<FieldIdentifier> targetFields = targets.Select(FieldIdentifier.Create).Distinct();
      if (_links.ContainsKey(originField))
      {
        _links[originField] = _links[originField].Concat(targetFields).Distinct();
      }
      else
      {
        _links.Add(FieldIdentifier.Create(origin), targetFields);
      }
    }

    /// <summary>
    /// Returns the fields linked to the parameter field.
    /// </summary>
    /// <param name="origin">The origin field.</param>
    /// <returns>The list of the linked fields, empty if there are none.</returns>
    public IEnumerable<FieldIdentifier> GetLinkedFields(FieldIdentifier origin) =>
      _links.ContainsKey(origin) ? _links[origin] : Enumerable.Empty<FieldIdentifier>();
  }
}
