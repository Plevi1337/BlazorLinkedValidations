using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace BlazorLinkedValidations
{
  public abstract class ValidationLinkerBase
  {
    private Dictionary<FieldIdentifier, IEnumerable<FieldIdentifier>> _links = new Dictionary<FieldIdentifier, IEnumerable<FieldIdentifier>>();

    protected void Link(Expression<Func<object>> origin, params Expression<Func<object>>[] targets)
    {
      FieldIdentifier originField = FieldIdentifier.Create(origin);
      IEnumerable<FieldIdentifier> targetFields = targets.Select(FieldIdentifier.Create);
      if (_links.ContainsKey(originField))
      {
        _links[originField] = _links[originField].Concat(targetFields).Distinct();
      }
      else
      {
        _links.Add(FieldIdentifier.Create(origin), targets.Select(FieldIdentifier.Create));
      }
    }

    public IEnumerable<FieldIdentifier> GetLinkedFields(FieldIdentifier origin) =>
      _links.ContainsKey(origin) ? _links[origin] : Enumerable.Empty<FieldIdentifier>();
  }
}
