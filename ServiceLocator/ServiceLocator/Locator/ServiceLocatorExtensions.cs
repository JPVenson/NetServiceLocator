using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator.Discovery;

namespace ServiceLocator.Locator
{
	/// <summary>
	/// 
	/// </summary>
	public static class ServiceLocatorExtensions
	{
		/// <summary>
		///		Creates a new <see cref="IServiceLocator"/> from <see cref="IServiceDiscoveryManager"/>
		/// </summary>
		/// <param name="discoveryManager"></param>
		/// <returns></returns>
		public static IServiceLocator UseServiceLocator(this IServiceDiscoveryManager discoveryManager)
		{
			return new ServiceLocator(discoveryManager);
		}

		/// <summary>
		///		Creates a new <see cref="IServiceDiscoveryManager"/> and sets a discovery for all types in the current <see cref="AppDomain"/>
		/// </summary>
		/// <param name="serviceCollection"></param>
		/// <returns></returns>
		public static IServiceLocator UseServiceLocator(this IServiceCollection serviceCollection)
		{
			return new ServiceLocator(serviceCollection.UseDiscovery().UseDiscoverServicesFromAppDomain());
		}
	}
}
