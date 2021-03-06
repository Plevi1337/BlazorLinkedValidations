# BlazorLinkedValidations

This library allows you to link together Blazor form fields to fire validations on multiple fields without requiring a full form validation.

There are more detailed samples in the [sample project](https://github.com/Plevi1337/BlazorLinkedValidations/tree/master/src/BlazorLinkedValidations/BlazorLinkedValidations.Samples).

## 1. Link the fields together

Create a linker class that inherits from the ValidationLinkerBase class:

```csharp
    public class FluentValidationsLinker : ValidationLinkerBase
    { }
```

Then setup the links in the contstructor: 

```csharp
    public class FluentValidationsLinker : ValidationLinkerBase
    {
        public FluentValidationsLinker(FluentValidationsFormExampleModelParent model)
        {
        Link(() => model.Number1, () => model.Number2, () => model.Sum);
        Link(() => model.Number2, () => model.Number1, () => model.Sum);
        Link(() => model.Child.City, () => model.Sum);
        Link(() => model.Sum, () => model.Child.Street);
        }
    }
```

The Link methods first parameters is the origin field, that fires validation events on the remaining parameters.

## 2. Add the ValidationLinker component to the form: 

```html 
    <EditForm Model="ViewModel">
        <FluentValidationValidator />
        <FluentValidationsChildExample @bind-ChildModel="ViewModel.Child" />
        <ValidationLinker Linker="new FluentValidationsLinker(ViewModel)" />
        .
        .
        .    
    </EditForm>
```

## 3. Profit:
<img src="https://raw.githubusercontent.com/Plevi1337/BlazorLinkedValidations/master/.github/resources/fluent_example.gif" />