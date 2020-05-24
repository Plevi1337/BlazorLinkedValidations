using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace BlazorLinkedValidations.Tests
{
  public class ValidationLinkerBaseLinkForwardTests
  {
    [Fact]
    public void LinkForwardOneToOneProperty()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
        })};
      TestLinkerStub stub = new TestLinkerStub(links);

      // Act
      IEnumerable<FieldIdentifier> linkedFields = stub.GetLinkedFields(FieldIdentifier.Create(() => poco.IntegerProperty));

      // Assert
      Assert.Single(linkedFields);
      Assert.Equal(FieldIdentifier.Create(() => poco.StringProperty), linkedFields.Single());
    }

    [Fact]
    public void NoLinkReturnsEmptyCollection()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links = Enumerable.Empty<(Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)>().ToArray();
      TestLinkerStub stub = new TestLinkerStub(links);

      // Act
      IEnumerable<FieldIdentifier> linkedFields = stub.GetLinkedFields(FieldIdentifier.Create(() => poco.IntegerProperty));

      // Assert
      Assert.Empty(linkedFields);
    }

    [Fact]
    public void LinkDifferentPropertiesInSameMethodReturnsBoth()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
          () => poco.DoubleProperty,
        })};
      TestLinkerStub stub = new TestLinkerStub(links);

      // Act
      IEnumerable<FieldIdentifier> linkedFields = stub.GetLinkedFields(FieldIdentifier.Create(() => poco.IntegerProperty));

      // Assert
      Assert.Equal(2, linkedFields.Count());
      Assert.Contains(linkedFields, x => FieldIdentifier.Create(() => poco.StringProperty).Equals(x));
      Assert.Contains(linkedFields, x => FieldIdentifier.Create(() => poco.DoubleProperty).Equals(x));
    }

    [Fact]
    public void LinkDifferentPropertiesInDifferentMethodReturnsBoth()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
        }),
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.DoubleProperty,
        })};
      TestLinkerStub stub = new TestLinkerStub(links);

      // Act
      IEnumerable<FieldIdentifier> linkedFields = stub.GetLinkedFields(FieldIdentifier.Create(() => poco.IntegerProperty));

      // Assert
      Assert.Equal(2, linkedFields.Count());
      Assert.Contains(linkedFields, x => FieldIdentifier.Create(() => poco.StringProperty).Equals(x));
      Assert.Contains(linkedFields, x => FieldIdentifier.Create(() => poco.DoubleProperty).Equals(x));
    }

    [Fact]
    public void LinkSamePropertyTwiceInSameMethodReturnsOnlyOnce()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
          () => poco.StringProperty,
        })};
      TestLinkerStub stub = new TestLinkerStub(links);

      // Act
      IEnumerable<FieldIdentifier> linkedFields = stub.GetLinkedFields(FieldIdentifier.Create(() => poco.IntegerProperty));

      // Assert
      Assert.Single(linkedFields);
      Assert.Equal(FieldIdentifier.Create(() => poco.StringProperty), linkedFields.Single());
    }

    [Fact]
    public void LinkSamePropertyTwiceInTwoMethodsReturnsOnlyOnce()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
        }),
        (() => poco.IntegerProperty,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
        }),
        };
      TestLinkerStub stub = new TestLinkerStub(links);

      // Act
      IEnumerable<FieldIdentifier> linkedFields = stub.GetLinkedFields(FieldIdentifier.Create(() => poco.IntegerProperty));

      // Assert
      Assert.Single(linkedFields);
      Assert.Equal(FieldIdentifier.Create(() => poco.StringProperty), linkedFields.Single());
    }

    [Fact]
    public void ThrowArguementNullExceptionOnNullOrigin()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (null,
        new Expression<Func<object>>[]{
          () => poco.StringProperty,
        })};

      // Assert & Act
      Assert.Throws<ArgumentNullException>(() => new TestLinkerStub(links));
    }

    [Fact]
    public void ThrowArguementNullExceptionOnNullTargets()
    {
      // Arrange
      TestPoco poco = new TestPoco();
      (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[] links =
        new (Expression<Func<object>> origin, IEnumerable<Expression<Func<object>>> targets)[]{
        (() => poco.IntegerProperty,
        null)};

      // Assert & Act
      Assert.Throws<ArgumentNullException>(() => new TestLinkerStub(links));
    }
  }
}
