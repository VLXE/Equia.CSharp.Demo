# Use cases of Equia.Api.Shared NuGet package

This repository contains a collection of samples showcasing some typical uses of [Equia.Api.Shared](https://www.nuget.org/packages/Equia.Api.Shared).

> All samples use .NET 9.0

## Status
![Test status](https://github.com/vlxe/Equia.CSharp.Demo/actions/workflows/dotnet.yml/badge.svg)

## What is Equia?

Equia is the cloud version of VLXE. It support systems and calculations from the Excel version of VLXE but also much more.
The NuGet package [Equia.Api.Shared](https://www.nuget.org/packages/Equia.Api.Shared) integrates the Equia platform by letting you write C# code that invokes functionality exposed by Equia API.

## Before you start

Please contact VLXE to obtain access.  
You will then recieve a accesskey.  
Once you have the values edit the file: SharedSettings.cs found in the project: 'Equia.CSharp.Shared'    

```
  /// <summary>
  /// Shared settings between projects
  /// </summary>
  public class SharedSettings
  {
    /// <summary>
    /// Access key as provided by VLXE
    /// </summary>
    public static string AccessKey = "Enter your access key here";
    /// <summary>
    /// Url to the Equia API server
    /// </summary>
    public static string ApiUrl = "https://api.equia.com/";
  }
```

You can get these values by contacting us at [VLXE](https://vlxe.com/contact)

!!! Do not share or expose this value or commit them to a public repository !!!

## Run any sample

Now just run samples like any other .NET Console application
