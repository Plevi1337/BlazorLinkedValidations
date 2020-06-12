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
    /// Returns the fields linked to the parameter field.
    /// </summary>
    /// <param name="origin">The origin field.</param>
    /// <returns>The list of the linked fields, empty if there are none.</returns>
    public IEnumerable<FieldIdentifier> GetLinkedFields(FieldIdentifier origin) =>
      _links.ContainsKey(origin) ? _links[origin] : Enumerable.Empty<FieldIdentifier>();

    /// <summary>
    /// Gets the registered origin fields.
    /// </summary>
    public IEnumerable<FieldIdentifier> OriginFields => _links.Keys;

    /// <summary>
    /// Sets up a one way link between the origin and every target field.
    /// </summary>
    /// <param name="origin">The origin of the links.</param>
    /// <param name="targets">The targets of the links.</param>
    /// <exception cref="ArgumentNullException">Throws if origin or targets is null.</exception>
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
    /// Sets up a one way link between every origin and the target field.
    /// </summary>
    /// <param name="target">The target of the links.</param>
    /// <param name="origins">The origins of the links.</param>
    /// <exception cref="ArgumentNullException">Throws if target or origins is null.</exception>
    protected void LinkBackward(Expression<Func<object>> target, params Expression<Func<object>>[] origins)
    {
      _ = target ?? throw new ArgumentNullException(nameof(target));
      _ = origins ?? throw new ArgumentNullException(nameof(origins));

      FieldIdentifier targetField = FieldIdentifier.Create(target);
      foreach (FieldIdentifier originField in origins.Select(FieldIdentifier.Create).Distinct())
      {
        AddLink(originField, targetField);
      }
    }

    /// <summary>
    /// Sets up a two way link between every field.
    /// </summary>
    /// <param name="fields">The list of fields to link together.</param>
    /// <exception cref="ArgumentNullException">Throws if fields is null.</exception>
    protected void LinkCross(params Expression<Func<object>>[] fields)
    {
      _ = fields ?? throw new ArgumentNullException(nameof(fields));
      IEnumerable<FieldIdentifier> fieldIdentifiers = fields.Where(x => x != null).Select(FieldIdentifier.Create);
      foreach (var origin in fieldIdentifiers)
      {
        foreach (var target in fieldIdentifiers)
        {
          AddLink(origin, target);
        }
      }
    }

    /// <summary>
    /// Sets up a linker for a nested property.
    /// </summary>
    /// <param name="childLinker">Linker class for a child property.</param>
    /// <exception cref="ArgumentNullException">Throws if child links is null.</exception>
    protected void SetChildLinker(ValidationLinkerBase childLinker)
    {
      _ = childLinker ?? throw new ArgumentNullException(nameof(childLinker));
      foreach (var childOriginField in childLinker.OriginFields)
      {
        IEnumerable<FieldIdentifier> connectedChildFields = childLinker.GetLinkedFields(childOriginField);
        AddLink(childOriginField, connectedChildFields.ToArray());
      }
    }

    /// <summary>
    /// Sets up a one way link between the origin and the target field.
    /// </summary>
    /// <param name="origin">The origin of the one way link.</param>
    /// <param name="targets">The targets of the one way link.</param>
    private void AddLink(FieldIdentifier origin, params FieldIdentifier[] targets)
    {
      if (_links.ContainsKey(origin))
      {
        _links[origin] = _links[origin].Concat(targets).Distinct();
      }
      else
      {
        _links.Add(origin, targets);
      }
    }
  }
}
