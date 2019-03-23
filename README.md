![fixed-point-types build status](https://ci.appveyor.com/api/projects/status/t6scmo444o1p56gp/branch/master?svg=true)

# Fixed Point Types
A library created to provide fixed point numeric types for use cases where floating point numbers just won't cut it.

[Jump straight to usage](#Usage)

## When would you want to use fixed point numbers over floating point numbers?
* Are fixed point numbers faster than floating point numbers?
  > Not really, dedicated FPUs in most modern processors actually make floating point operations faster than fixed point operations.
* Are fixed point numbers more precise than floating point numbers?
  > No, sometimes fixed point numbers are less precise than floating point numbers. But also this can vary based on the scale the developer chooses.
* Do fixed point numbers have a larger range than floating point numbers?
  > Nope, not when using the same amount of bits when storing the fixed point number anyway. This is actually a huge advantage of floating point numbers.
* So why would you want to use fixed point numbers?
  > Fixed point numbers are deterministic. This is really useful for things like simulations where results need to be perfectly replicable. I originally created this library to create a game using a deterministic lockstep networking model. But There are many different applications for fixed point numbers.

## Usage
*Nuget Package not available... Yet.*

For now all you need to do is include the `Fixed32.cs`(Or the entire `FixedTypes.csproj`) file in your solution.

To initialize a new fixed point number:
```csharp
var someDeterministicNumber = new Fixed32(16); // 0.0
var someDeterministicNumber = new Fixed32(16, 5); // 5.0
```

The 16 passed to constructor is the scale of the number. It basically specifies how many bits should be used to store the **characteristic**(bits preceding the point) and how many bits should be used to store the **mantissa**(bits succeeding the point).

### Tips :information_source:
* A smaller scale will decrease precision and increase range.
* A larger scale will increase precision and decrease range.
* Scale should be between(and including) 1 and 31
* Operations +, -, *, / etc. should only be done on fixed point numbers of the same scale.

There is no way to initialize a fixed point number as a fraction(only as a whole number). This is because all numeric literals in C# are floating point numbers, with the exception of integers. If you wanted to initialize a fraction I would suggest dividing the numerator with denominator of the fraction. For example:
```csharp
var twoAndAHalf = new Fixed32(16, 5) / new Fixed(16, 2); // 5/2 = 2.5
```

## Contributing

This project is covered by many tests, if you find a bug please create and issue. If you know how to solve the issue please create a pull request with the solution and a test for the scenario described in the original issue.

This library is far from complete and still requires many useful features. So feel free to create a pull request.:v: