[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Common.svg)](https://www.nuget.org/packages/ByteDev.Common)

# ByteDev.Common

Common is a collection of generic utility methods.

**NOTE:** This library has been superseded by a number of separate packages. Functionality from this library has been absorbed into these libraries.  Libraries include:
- `ByteDev.Collections` (includes all collections related functionality)
- `ByteDev.Encoding` (includes base64 and hex related functionality)
- `ByteDev.Time` (includes all functionality in the Time namespace)
- `ByteDev.Strings` (string extension methods and ToStringHelper)
- `ByteDev.Reflection` (reflection related extension methods)
- `ByteDev.Exceptions` (includes the custom exceptions)
- `ByteDev.ResourceIdentifier` (includes Uri related functionality)
- `ByteDev.ValueTypes` (includes Guid and Enum related functionality)
- `ByteDev.Xml` (includes XML related functionality, including serialization)

It is strongly recommended you stop using this package and use any of the respective packages above instead.

## Installation

ByteDev.Common has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Common is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Common`

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Xml/blob/master/docs/RELEASE-NOTES.md).
