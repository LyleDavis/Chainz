# Chainz ![CI](https://github.com/LyleDavis/Chainz/workflows/CI/badge.svg?branch=master)

Generic library implementation of the Middleware/Chain of Responsibility Pattern.

### Installation
Typical nuget package: https://www.nuget.org/packages/Chainz/

`dotnet add package Chainz`

### Usage

#### Params
 The library supports _typed_ chains. 
 A chain must be instantiated with the intended type of the parameter to pass down through it.
 For instance:
 
 ```c#
interface ICommandModel {
    ...
}

var chain = new Chain<ICommandModel>();
```
#### Chains
There are several convenience methods added to create and modify existing chains, plus a compilation step to produce something runnable.
The only restriction on these is that the compilation step forms a linked list of the _instantiated_ chainlinks, and therefore the order cannot be modified after this point.

It is **highly recommended** to cache the compiled chain, as forming the compiled linked list is a relatively expensive operation involving instantiations with `Activator`. 
This is to keep the API fluid. 
Constructor arguments are _not_ made available to the consumer for this reason, a chain (once compiled) should be able to be run through many times for many different objects of the same type.

If the consumer of Chainz is a library in and of itself, it may make sense to _not_ compile the Chain before giving the application a reference to the uncompiled chain object and allowing it to modify as appropriate.  

```c#
var compiledChain = new Chain<ChainParam>()
    .Use<SecondChainLink>() // Adds a chainlink to the chain
    .After<SecondChainLink, FourthChainLink>() // adds a chainlink after a particular other chainlink
    .Prepend<FirstChainLink>() // prepends a chainlink to the beginning of the chain
    .Before<FourthChainLink, ThirdChainLink>() // adds a chainlink before a different particular chainlink
    .Use<DoesNotContinueChainLink>() // Adds a chainlink to the chain
    .Replace<DoesNotContinueChainLink, FifthChainLink>() // replaces a particular chainlink with something different
    .Use<DoesNotContinueChainLink>() // Adds a chainlink to the chain
    .Use<SixthChainLink>() // Adds a chainlink to the chain
    .Compile();

compiledChain.Handle(new ChainParam());
```

See `src/Chainz/Chain.cs` for all convenience methods to help build or modify chains.

#### ChainLinks

Each ChainLink must implement `IChainLink<Type>`, where `Type` is the type of the intended parameter (or some sort of abstracted interface).

Do not worry about calling `Handle` when `Next` may be blank due to it being the last link in the chain, `Compile()` automatically adds a blank link to the end of the chain to ensure links can be used both in the middle and at the end of various chains.

For example:
```c#
interface IChainLinkParam {
    string Something { get; set; }
}

class FirstChainLink : IChainLink<IChainLinkParam> {
    // this will be automatically setup by the chain compilation process. It points to the next handler. 
    public IChainLink<IChainLinkParam> Next { get; set; }
    
    public void Handle(IChainLinkParam args) {
        if (args.Something != "SomeValue") {
            Next.Handle(args); // Moving onto the next handler in the chain
        }
    }
}
``` 