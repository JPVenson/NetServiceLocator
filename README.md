# NetServiceLocator

Nuget:
https://www.nuget.org/packages/NetServiceLocator/1.0.0


How to use the NetServiceLocator?

The code is seperated into 3 parts:
- Declaration of Services
- Discovery of Services
- Importing the services

First you must annoate your type with the corresponding Attribute.
You can use:
- `[SingletonServiceAttribute]`
- `[TransientServiceAttribute]`
- `[ScopedServiceAttribute]`
- or the parameter of the `[ServiceAttribute]`

to declare your type a service. Use the `params Type[] registerAs` parameter of the attributes to declare all service interfaces your Type should be registered with like this:

```csharp
[SingeltonService(typeof(IFooRepository), typeof(IDatabaseRepository))] //this is the same as
[Service(typeof(IFooRepository), typeof(IDatabaseRepository), RegistrationType = ServiceLifetime.Singleton)] //as this
public class FooRepository : IFooRepository
{
    public string Connection { get; set; }
    public async Task DoStuff() { }
}

public interface IFooRepository : IDatabaseRepository
{
    Task DoStuff();
}

public interface IDatabaseRepository
{
    string Connection { get; set; }
}
```

then you should setup your discovery options on a `IServiceCollection`:
```csharp
using ServiceLocator.Discovery;
public void ConfigureServices(IServiceCollection services)
{
  services.UseServiceDiscovery()
    .DiscoverFromAppDomain() //Loads all services from all loaded Assemblies in the current AppDomain
    .DiscoverFromAssembly(Assembly) //Loads all services from that Assembly
    .DiscoverTypes(IEnumerable<Type>) //Loads all given types
}
```

After you discovered your services you Locate your services and import them into your `IServiceCollection`:

```csharp
using ServiceLocator.Discovery;
public void ConfigureServices(IServiceCollection services)
{
  services.UseServiceDiscovery()
  ...
  .LocateServices() //this imports all services into IServiceCollection
}
```
