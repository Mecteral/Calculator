# CURRENTLY OPEN TASKS
This file will always contain what I consider open tasks, be that refactoring, code cleanup, new features or the installation of tools/extensions. 

## TOOLS/EXTENSIONS

## NAMING
- Avoid names like `mResultString` which include a description of the type - the type is obvious, anyway, in a strongly typed language like C#.
And then sometimes it might change: imagine you call a parameter `inputList` because it's a `List<>`, 
then you realize that you actually don't need any of `List<>`'s interface and change the type to the more general `IEnumerable<>` - you would now have to rename the parameter.

## MINOR CODE IMPROVEMENTS


## OPPORTUNITIES FOR BIG REFACTORING


## TESTING/COVERAGE

## NEXT FEATURE(S)
