# Overview
The purpose of this library is to store cache data, the infrastructure "StackExchange.Redis" is fed, but this library offers more **generic** and **clean** architecture. This library was developed .NET Standard.

# Covarage
 - StringSet
 - SortedSet
 - Set

# Installation
[Nuget Package](https://www.nuget.org/packages/TStack.RedisExchange/)
#### Package Manager
```PM
Install-Package TStack.RedisExchange -Version 1.0.1
```
#### .NET CLI
```PM
dotnet add package TStack.RedisExchange --version 1.0.1
```
#### PackageReference
```PM
<PackageReference Include="TStack.RedisExchange" Version="1.0.1" />
```
#### Paket CLI
```PM
paket add TStack.RedisExchange --version 1.0.1
```

# Usage
This library supports cluster mode, for usage follow steps.
### Step One
Define Redis Context Config
```csharp
   public class RedisContext : RedisContextConfig
    {
        public RedisContext() : base(new List<RedisServer> { new RedisServer("localhost", 6379)}, "", "ClientName", 15000, 15000)
        {
        }
    }
```
### Step Two
Define Provider
```csharp
 public class ProjectProvider : RedisProvider<RedisContext>
    {
    }
```
Ready to use.


# Author

Ferhat Candaş - Software Developer
 - Mail : candasferhat61@gmail.com
 - LinkedIn : https://www.linkedin.com/in/ferhatcandas