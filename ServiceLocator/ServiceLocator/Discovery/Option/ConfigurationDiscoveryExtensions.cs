using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceLocator.Discovery.Service.Options;

namespace ServiceLocator.Discovery.Option
{
	public static class ConfigurationDiscoveryExtensions
	{
		/// <summary>
		///		Allows the discovery of <see cref="IOptions{TOptions}"/> based on the <see cref="FromConfigAttribute"/>
		/// </summary>
		public static IConfigurationDiscoveryManager DiscoverOptions(this IServiceDiscoveryManager discovery, IConfiguration configuration)
		{
			discovery.ServiceCollection.AddOptions();
			return new ConfigurationDiscoveryManager(discovery, configuration);
		}

		/// <summary>
		///		Loads all options from all <see cref="Assembly"/> of the current loaded <see cref="AppDomain"/>
		/// </summary>
		/// <param name="discovery"></param>
		/// <returns></returns>
		public static IConfigurationDiscoveryManager FromAppDomain(this IConfigurationDiscoveryManager discovery)
		{
			discovery.ServiceTypes.Add(new AppDomainOptionDiscovery(discovery.Configuration));
			return discovery;
		}

		/// <summary>
		///		Loads all options from all <see cref="Assembly"/> of the current loaded <see cref="AppDomain"/>
		/// </summary>
		/// <param name="discovery"></param>
		/// <returns></returns>
		public static IConfigurationDiscoveryManager FromAppDomain(this IConfigurationDiscoveryManager discovery, Func<AppDomainServiceDiscoveryOptions, AppDomainServiceDiscoveryOptions> options)
		{
			discovery.ServiceTypes.Add(new AppDomainOptionDiscovery(options(new AppDomainServiceDiscoveryOptions()), discovery.Configuration));
			return discovery;
		}

		/// <summary>
		///		Loads all options from the given <see cref="Assembly"/>
		/// </summary>
		/// <param name="discovery"></param>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static IConfigurationDiscoveryManager FromAssembly(this IConfigurationDiscoveryManager discovery, Assembly assembly)
		{
			discovery.ServiceTypes.Add(new AssemblyOptionDiscovery(assembly, discovery.Configuration));
			return discovery;
		}

		///  <summary>
		/// 		Loads all options from the given <see cref="Assembly"/>
		///  </summary>
		///  <param name="discovery"></param>
		///  <param name="assembly"></param>
		///  <param name="options"></param>
		///  <returns></returns>
		public static IConfigurationDiscoveryManager FromAssembly(this IConfigurationDiscoveryManager discovery, Assembly assembly, Func<AssemblyServiceDiscoveryOptions, AssemblyServiceDiscoveryOptions> options)
		{
			discovery.ServiceTypes.Add(new AssemblyOptionDiscovery(assembly, options(new AssemblyServiceDiscoveryOptions()), discovery.Configuration));
			return discovery;
		}

		/// <summary>
		///		Loads all options from its parameter
		/// </summary>
		/// <param name="discovery"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static IConfigurationDiscoveryManager FromTypes(this IConfigurationDiscoveryManager discovery, IEnumerable<Type> types)
		{
			discovery.ServiceTypes.Add(new OptionsTypesDiscovery(types, discovery.Configuration));
			return discovery;
		}
	}
}
