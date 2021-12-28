using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator.Discovery.Service.Options;

namespace ServiceLocator.Discovery.Service
{
	/// <summary>
	///		
	/// </summary>
	public static class ServiceDiscoveryExtensions
	{
		/// <summary>
		///		Creates a new <see cref="IServiceDiscoveryManager"/>
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceDiscoveryManager UseServiceDiscovery(this IServiceCollection services)
		{
			return new ServiceDiscoveryManager(services);
		}

		/// <summary>
		///		Loads all types from all <see cref="Assembly"/> of the current loaded <see cref="AppDomain"/>
		/// </summary>
		/// <param name="discovery"></param>
		/// <returns></returns>
		public static IServiceDiscoveryManager FromAppDomain(this IServiceDiscoveryManager discovery)
		{
			discovery.ServiceTypes.Add(new AppDomainServiceDiscovery());
			return discovery;
		}

		/// <summary>
		///		Loads all types from all <see cref="Assembly"/> of the current loaded <see cref="AppDomain"/>
		/// </summary>
		/// <param name="discovery"></param>
		/// <returns></returns>
		public static IServiceDiscoveryManager FromAppDomain(this IServiceDiscoveryManager discovery, Func<AppDomainServiceDiscoveryOptions, AppDomainServiceDiscoveryOptions> options)
		{
			discovery.ServiceTypes.Add(new AppDomainServiceDiscovery(options(new AppDomainServiceDiscoveryOptions())));
			return discovery;
		}

		/// <summary>
		///		Loads all types from the given <see cref="Assembly"/>
		/// </summary>
		/// <param name="discovery"></param>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static IServiceDiscoveryManager FromAssembly(this IServiceDiscoveryManager discovery, Assembly assembly)
		{
			discovery.ServiceTypes.Add(new AssemblyServiceDiscovery(assembly));
			return discovery;
		}

		///  <summary>
		/// 		Loads all types from the given <see cref="Assembly"/>
		///  </summary>
		///  <param name="discovery"></param>
		///  <param name="assembly"></param>
		///  <param name="options"></param>
		///  <returns></returns>
		public static IServiceDiscoveryManager FromAssembly(this IServiceDiscoveryManager discovery, Assembly assembly, Func<AssemblyServiceDiscoveryOptions, AssemblyServiceDiscoveryOptions> options)
		{
			discovery.ServiceTypes.Add(new AssemblyServiceDiscovery(assembly, options(new AssemblyServiceDiscoveryOptions())));
			return discovery;
		}

		/// <summary>
		///		Loads all types from its parameter
		/// </summary>
		/// <param name="discovery"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static IServiceDiscoveryManager FromTypes(this IServiceDiscoveryManager discovery, IEnumerable<Type> types)
		{
			discovery.ServiceTypes.Add(new ServiceTypesDiscovery(types));
			return discovery;
		}
	}
}