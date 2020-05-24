using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorLinkedValidations.Tests
{
  public class TestLinkerStub : ValidationLinkerBase
  {
    public TestLinkerStub(IEnumerable<(Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)> links)
    {
      foreach ((Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets) link in links)
      {
        LinkForward(link.origin, link.targets.ToArray());
      }
    }
  }
}
