# Next Tasks
I did some minor cleanup in the project and noted a few things. Fixing these is your next exercise:

First of all, in VS, under *Tools | Extensions and Updates | Online*, find and install the [MarkdownEditor extension](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor), 
so you can view this here properly :)

## NAMING:
- `ArgumentParser` is named too specifically - it's really got nothing to do with arguments.
- Correct spelling mistakes in the naming of methods, variables etc and make sure to use consistent naming. `(` and `)` are *parentheses*, `[` and `]` are *brackets*.
- Avoid naming methods `Check...` implying they would check a condition when they do not return a boolean, but instead *modify* something. Look for example at `CheckIfBracketsNeedToBeAdded`.

## MINOR CODE NICETIES:
- Eliminate redundant if/then/else clauses. Hint: look at `DeleteWhitespacesOfListAndAddBracketsAroundTheInput`.
- In `DeleteWhitespacesOfListAndAddBracketsAroundTheInput`, find a way to avoid inserting the opening '(' *at the end* of the method. Instead try to get that opening parenthesis there the way a human would.

## OPPORTUNITIES FOR BIG REFACTORING:

- In Program.Main(), the line:

```c#
var commandLine = string.Join(" ", args);
```

would create a single string from the commandline arguments.
Considering this, you could change the interface of `ArgumentParser` to take a string - if you look at your tests, they would become easier to write that way.

- Furthermore, `ArgumentParser` is essentially a static class currently, but you already introduced state (via the static member field). Whenever we find ourselves introducing state 
into a so far static class, we ought to seek to refactor the static class to a proper one. In this case, change the class so it can be used like this:

```c#
	var tokenizer= new ArgumentParser(args);
	tokenizer.Tokenize();
	foreach (var token in tokenizer.Tokens)
	{
		Console.Write(token);
	}
```

I.e., the input is passed in the constructor. The `Tokenize()` method returns nothing. The result is found in a property `Tokens` which is readonly from outside `ArgumentParser`.
This also will allow to extend the results returned if needed in the future, for example, adding a property containing error messages if tokenizing detected illegal input.

## COVERAGE:
Eliminate the large portions of code in ArgumentParser not yet covered by tests. Either write tests for them. Or remove them. Hint: you can use Resharper for coverage, too, 
by right-clicking the solution and selecting *Cover Unit Tests*.

