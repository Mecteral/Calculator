# ROADMAP

## Functions and Units
- support a few functions like cos, sin, tan (keep in mind that we might eventually be adding dozens of functions, so make your life easy in that regard)
- add unit support for trigonometric functions (rad, deg)
- add unit-conversion function (3000m=3km, 1ft=30.48cm)
- add command-line options for default units
- refactor to use command-line argument parser library


## VARIABLE SUPPORT
single-letter variables 
goal: performs arithmetic operations with variables 

### Step 1

(2+4)*x
should return
6*x

compute everything directly possible, ie, where all operands are constants, leave the rest. that means:
(2+4)*x +2*x
6*x+2*x

Doesn't yet know to do perform operations when operands bound to variables.

### Step 2
2x+4x
should return
6x

if operands are directly compatible. ie

3x+y-(2x+2y)
2(x+y) +x

dont work.

### Step 3
3x+y-2*(x+y)
x-y

## Show Thoughts :)
if a certain command-line flag is given, show steps of simplification. examples:

a)
2*(1+3)/2=
2*((1+3)/2)=			this line is not necessary, but for now okay if it happens
2*(4/2)=
2*2=
4

b)
3x+y-2*(x+y)=
3x+y-(2x+2y)=
3x+y-2x-2y=
3x-2x+y-2y				this line is not necessary, but for now okay if it happens
x*(3-2)+y*(1-2)
x*1+y*(-1)
x-y

## MAKE THOUGHTS MORE HUMAN
try and eliminate certain transformation steps from output, for example, the implicit adding of parentheses around multiplicative operations; 
or re-ordering for variables; etc. add command-line switches for:
- leave out all optional transformation steps
- leave out no transformation step at all
- leave out these steps: with a list of descriptive names, comma-separated or such

## CONFIGURATION
Add configuration
1. add a config file for all options. if file is present in working directory, it is used. any switches passed via the commandline override any in the config file
1. make the config file a user-specific thing in their local store; make it so that the command-line with each run implicitly updates that config file with the value of the current switches; make what so far were coded default value of switches come out from that config file
1. add three switches to the command-line:
 - revert stored config to defaults - does nothing else
 - use config from file (file for this run overrides everything in stored config)
 - adopt config from file as stored config - sets stored config to contents of file, does nothing else

## GRAPHICAL USE
- add minimalistic windows GUI, using UWP
- add installer
- add capability to trigger calc via hotkey, enter what you want calculated/simplified, press enter, see result, click away window

## POLISHING/MATURING
- polishing: which features are missing for real usability? what would ppl expect? make a list. discuss the list. estimate effort vs business-value. implement. 
- - validation of user input
- - - multiple consecutive operators
- - - missing parentheses
- - - unknown characters
- - - not parsable numbers
- - allow usage of greek letters
- - function definitions
- - graphing of functions
- code-polishing: refactor code as much as you can. if some cool functions/types are in there not really specific to calculator:
- - identify
- - create library for them; put on github; learn nuget, publish package

## FIRST PUBLISHING
### add auto-updater
###  put the whole thing on github
### create a proper little web-page with:

- download exe
- download installer
- documentation
- release history
- roadmap
- integration of user-voice
### Integrations
- auto-update function
- add in-app bug/feature request feature
- add statistics collection (how often used, types of terms, average processing time etc.), ask user upon installation whether okay to send, create a web-sink for this data somewhere

## DEPLOYMENT AUTOMATION
automate process for creating a new release from a specific git-version


## FOR THE FUTURE
- add stuff like special formula for simplification, like cos= sqrt(1-sin^2)

