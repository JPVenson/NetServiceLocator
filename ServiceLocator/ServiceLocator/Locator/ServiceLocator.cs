using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator.Attributes;
using ServiceLocator.Discovery;

namespace ServiceLocator.Locator
{
	/// <summary>
	///		Uses the <see cref="IServiceDiscoveryManager"/> to import all its <see cref="Type"/> into a <see cref="IServiceCollection"/>
	/// </summary>
	public interface IServiceLocator
	{
		/// <summary>
		///		Imports all types
		/// </summary>
		void LocateServices();
	}
	
	/// <inheritdoc />
	[Service(typeof(object), RegistrationType = ServiceLifetime.Singleton)]
	public class ServiceLocator : IServiceLocator
	{
		private readonly IServiceDiscoveryManager _serviceDiscoveryManager;

		public ServiceLocator(IServiceDiscoveryManager serviceDiscoveryManager)
		{
			_serviceDiscoveryManager = serviceDiscoveryManager;
		}
		
		/// <inheritdoc />
		public void LocateServices()
		{
			foreach (var discoverType in _serviceDiscoveryManager.DiscoverTypes())
			{
				foreach (var services in discoverType.GetCustomAttribute<ServiceAttribute>(false).GetDescriptors(discoverType))
				{
					_serviceDiscoveryManager.ServiceCollection.Add(services);
				}
			}
		}
	}
}