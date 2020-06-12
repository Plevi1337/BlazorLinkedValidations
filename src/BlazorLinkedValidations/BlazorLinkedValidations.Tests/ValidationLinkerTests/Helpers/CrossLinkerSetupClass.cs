using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorLinkedValidations.Tests.ValidationLinkerTests.Helpers
{
  public class CrossLinkerSetupClass : ValidationLinkerBase
  {
    public CrossLinkerSetupClass(IEnumerable<IEnumerable<Expression<Func<object>>>> crossLinkFields)
    {
      foreach (var fields in crossLinkFields)
      {
        LinkCross(fields.ToArray());
      }
    }
  }
}
