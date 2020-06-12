using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorLinkedValidations.Tests.ValidationLinkerTests.Helpers
{
  public class BackwardLinkerSetupClass : ValidationLinkerBase
  {
    public BackwardLinkerSetupClass(IEnumerable<(Expression<Func<object>> target, IEnumerable<Expression<Func<object>>> origins)> links)
    {
      foreach ((Expression<Func<object>> target, IEnumerable<Expression<Func<object>>> origins) link in links)
      {
        LinkBackward(link.target, link.origins.ToArray());
      }
    }
  }
}
