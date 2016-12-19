# General C# Coding Style Guidelines

I'll update this file with new rules regularly. Check back whenever you see it's been updated in git.





## Naming Conventions

Generally take care to use correct spelling. Code is read much more often than written and bad spelling is irritating when reading.
Prefer descriptively named constants and functions over repeated use of constant strings/numbers/characters. For examples, look at how I refactored the Tokenizer.


### Namespaces
Namespaces should be pascal-cased . However, avoid combining multiple words for namespace names wherever possible.

### Classes and Structs
Classes and structs should be Pascal-cased. 
### Abstract Classes
Abstract classes should be prefixed with “A” or “An”, following the rules of natural English for when to use which. For example: ```APattern```, but ```AnAdapter```.
### Interfaces
Interfaces should be prefixed with “I”. For example: ```ISerializer```.
### Fields
Instance fields should be prefixed with “m” followed by a pascal-cased name.  Static fields should be prefixed with “s” followed by a pascal-cased name. For example:
```c#
static int sNumber = 0;
string mText= string.Empty;
```
### Methods/Properties
Methods and properties should be pascal-cased.
### Events/Delegates
Events and delegates should be prefixed with “On”. For example: ```OnGridSizeChanged```.
### Parameters
Method parameters should be camel-cased.
### Template Parameters
If only one template parameter is needed, it should be called simply “T”. If multiple template parameters are needed, they should be pascal-cased names prefixed with “T”. 
For example: 
```c#
public class List<T> {}
public class Dictionary<TKey, TValue> {}
```
### Abbreviations
Abbreviations should be used very sparingly and only when the abbreviation is very commonly used.
When in doubt, rather use the full form than the abbreviation. When using an abbreviation, regular capitalization rules hold true. 
For example, in a class name only the first letter of the abbreviation will be capitalized. In a parameter name, if the abbreviation
is at the start of the name, it is not capitalized at all. For example:
```c#
public class XmlSerializer
{
	public void Deserialize(string xmlDefinition) {}
}
```

### Local Variables
Variables should be camel-cased.

- Loop variables in for loops should almost always be called just `i` or `j` if their meaning is really just an index.
- If you for-loop over coordinates, the names `x`, `y`, `z` are preferred for the loop variable.
- If a method has a return value and you use a variable in the method's body to hold it, always call it ```result```.

## Organization
### Files
Each class and each interface should live in its own file. The file should be named exactly like the class or interface.
### Namespaces
Namespaces should be mirrored by folders. Definitions inside namespaces should be contained in files inside the corresponding folders. 
### Assemblies
Assemblies follow the same naming conventions as namespaces. Additionally, namespaces must not appear in assemblies unless they share the same root path.
For example, a namespace ```Dosig.Honoris.Core``` may appear in assemblies named ```Dosig``` or ```Dosig.Honoris``` or ```Dosig.Honoris.Core```, 
but not in an assembly named ```Honoris.Core``` etc.
Generally, organize assemblies by thinking about deployment: is the functionality in this assembly going to be used by different applications? 
And does one application need only a small sub-set of the functionality? And is that functionality mostly contained in a single namespace and its children? 
If yes to all three questions, you probably have a very good candidate for splitting the assembly in two parts.
### Test Classes
Test classes always have the same name as their target type under test, postfixed with "Tests".
They should reside in an assembly and namespace mirroring the assembly and namespace of the type under test. 
For example, if the production class is ```Dosig.Honoris.Core.Functor``` and resides in the assembly ```Dosig.Honoris```, 
then the test class should be found in the assembly ```Dosig.Honoris.Tests``` in a namespace ```Dosig.Honoris.Tests.Core``` and be called ```FunctorTests```. 
### Applications
Applications assemblies should contain minimal code. Move as much code as possible into library assemblies. Applications should really just wire these classes together.

## Language Features
### Access Modifiers
Do not use explicit access modifier keywords for the defaults. For example, if you want a field to be private, do not used private as this is the default access for fields anyway. Or if you want a class to be internal, again do not use the internal keyword as internal is the default access for classes.
### Initializers
Make liberal use of initializers for fields. Always prefer them over initializing inside a constructor, where possible. 
### Readonly
Use the readonly keyword for fields wherever you can. 
### Var
Prefer dynamic type inference over explicit typing wherever possible, in other words, use var. 
### Return Expressions
- use expressions wherever a method returns a simple expression; Example:
```c#
static bool IsMultiplicativeOperator(string text) => text == "*" || text == "/";
```
looks nicer than
```c#
{
    return text == "*" || text == "/";
}
```
### Object and Collection Initializers
Use object initializers and collection initializers wherever possible. Example:
```c#
var x= new List<int> {13, 17};
var y= new Person {FirstName= "Dominik", LastName="Siegele", Sex=Sex.Male}
```
looks nicer than
```c#
var x= new List<int>();
x.Add(13);
x.Add(17);
var y= new Person();
x.FirstName= "Dominik";
x.LastName="Siegele";
x.Sex=Sex.Male;
```

## Design
### LINQ
For common filtering and sorting use LINQ instead of a foreach loop with conditionals. 
There are very rare exceptions where LINQ is less readable, but mostly you trade complex, looped code with if-statements for simpler chained functions calls.
Resharper is an ideal learning tool because it hints whenever you can use LINQ by showing a small light bulb (you must have the cursor on the foreach statement).
For example, instead of:
```c#
public IEnumerable<string> Filter(IEnumerable<string> input)
{
	var result= new List<string>();
	foreach (var s in in input)
	{
		if (s.StartsWith(“hello”)) result.Add(s);
	}
	return result;
}
```
write:
```c#
public IEnumerable<string> Filter(IEnumerable<string> input) => input.Where(s => s.StartsWith("hello"));
```
### Most generalized type
Use the most generalized type possible for parameters and fields. For example, do not use ```List<T>``` if you really do not depend on indexed access.
Rather use ```IEnumerable<T>```. Obviously, this holds just as true for your own classes and interfaces as for such provided by system or 3rd party libraries.

### Singletons
Avoid singletons as much as you can. If you have to introduce one, make sure it is only one. Make this one singleton hold multiple objects you need singleton access for.
And make sure the singleton does not auto-initialize, but instead is initialized from outside. (Otherwise very funky stuff can happen in unit tests and other situations.)

Keep in mind that any static state (static fields, mutable properties etc.) are really just singletons in disguise.

### Structs versus Classes
Structs are value types, i.e., they are created on the stack. Their sole purpose is to give you as a developer the chance to build analogs to 
built-in primitive types with similar performance and semantics. So if you find yourself using that tuple of integers over and over again, 
consider using a struct. When you do use structs, make sure they follow the semantics people expect from primitive types: 
- Make them immutable. Construction/destruction of value types is cheap in the VM. 
And just as you do not expect to be able to really change an integer with an ```Add()``` method, but rather to get a new integer as a result value,
do the same for the users of your struct. Note also that immutability is the most efficient way to ensure thread-safety.
- Override ```Equals()```, ```HashCode()```, ```operator==``` and ```operator!=```.
- Consider providing operator overloads for common operations like adding/concatenation etc.
- Override ```ToString()```.

### ToString()
Generally, whenever you find yourself wanting to print out the value of a class, immediately write an overload of ToString() for it.
This also will make life a bit easier in the debugger for you. 

### Common Code Smells
Patterns of code that make it 'smell fishy':

- Methods with more than two nesting levels inside are suspicious. Consider splitting them into multiple methods.
- Methods long enough to need the use of a pagedown-press to view them completely on a standard resolution are highly suspicious. Split them into multiple methods.
- There is an old rule about a method not having multiple exit points. Following that rule sometimes leads to more nesting and thus less readable code.
If that is the case, ignore that old rule and prefer multiple exits over deeper nesting.
- Avoid returning null. Methods returning null force the caller to use a conditional to check the return value, increasing complexity.
Instead consider returning an ```IEnumerable<>``` (which, if there is no result, simply will be empty) or using the [Null object pattern](http://www.cs.oberlin.edu/~jwalker/nullObjPattern/).
- Do not ever check for the concrete type of a class. If you find yourself in a situation where you need this, use the [Visitor pattern](http://en.wikipedia.org/wiki/Visitor_pattern). 
- If you find yourself using a switch statement on the same enumeration type in multiple places in your code, consider swithcing to the [State  pattern](https://dotnetcodr.com/2013/05/16/design-patterns-and-practices-in-net-the-state-pattern/). 
- If you find a class or interface exposing many public methods, consider whether this class or interface really has a single well-defined responsibility.
Chances are it actually is serving multiple responsibilities which could and should be split over multiple classes/interfaces. 
- Classes should rarely directly depend on other classes. Instead, they should depend on interfaces or delegates.
This allows for better testing (you can mock interfaces in unit-tests) and reduces coupling. Besides, it makes it easier to switch to a
different implementation of the interface if circumstances/requirements/specifications change. 

