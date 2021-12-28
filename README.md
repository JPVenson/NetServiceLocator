# NetServiceLocator

Nuget:
https://www.nuget.org/packages/NetServiceLocator


How to use the NetServiceLocator?

The code is seperated into 3 parts:
- Declaration of Services
- Discovery of Services / Options
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
    .FromAppDomain() //Loads all services from all loaded Assemblies in the current AppDomain
    .FromAssembly(Assembly) //Loads all services from that Assembly
    .FromTypes(IEnumerable<Type>) //Loads all given types
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

The NetServiceLocator also allows you to easly discover Options via the `Microsoft.Extensions.Configuration.Binder` package.
To load all your `IOption<T>` sections into the `IServiceCollection` you must use the `[FromConfig("SectionName")]` attribute. 

```csharp
[FromConfig("TestSection")]
public class FromConfigData
{
	public string ConfA { get; set; }
	public string ConfB { get; set; }
}
```

After that you follow the same pattern as with Services. Use the `UseServiceDiscovery()` extension method to start the discovery and after you added all service discovery options just follow with `DiscoverOptions`:

```csharp
.UseServiceDiscovery()
...
.DiscoverOptions(configuration) //this starts the Option discovery
.FromTypes(new[] { typeof(FromConfigData) }) //same pattern here as with services
.LocateServices() //end with LocateServices to add everything into the IServiceCollection

```
