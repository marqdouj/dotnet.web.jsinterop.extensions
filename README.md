# dotnet.web.jsinterop.extensions
[![.NET](https://img.shields.io/badge/.NET-blueviolet.svg)](https://github.com/dotnet/aspnetcore)
[![MIT License](https://img.shields.io/github/license/marqdouj/dotnet.azuremaps?color=%230b0&style=flat-square)](https://github.com/marqdouj/dotnet.web.jsinterop.extensions/blob/master/LICENSE.txt)
[![NuGet](https://img.shields.io/badge/nuget-repo-blue?logo=nuget)](https://www.nuget.org/packages/Marqdouj.DotNet.Web.JsInterop.Extensions/)

# Overview
A .NET library for extension methods I find useful with [JS Interop](https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/).

## Features
- `ModuleTask` extension methods for working with a lazily loaded module, translating JavaScript exceptions to a custom `JS2CSharpException`.

## Demo
See the `Sandbox` project for a working example of the library in action.

## Release Notes
- ### 11.0.0
  - Initial release of the library.