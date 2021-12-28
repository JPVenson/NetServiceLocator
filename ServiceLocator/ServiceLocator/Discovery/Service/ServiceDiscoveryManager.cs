using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Discovery.Service
{
	/// <inheritdoc />
	public class ServiceDiscoveryManager : IServiceDiscoveryManager
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="serviceCollection"></param>
		public ServiceDiscoveryManager(IServiceCollection serviceCollection)
		{
			ServiceCollection = serviceCollection;
			ServiceTypes = new List<IServiceDiscovery>();
		}
		
		/// <inheritdoc />
		public IServiceCollection ServiceCollection { get; }
		
		/// <inheritdoc />
		public IList<IServiceDiscovery> ServiceTypes { get; }
		
		/// <inheritdoc />
		public IServiceCollection LocateServices()
		{
			foreach (var serviceDescriptor in ServiceTypes.SelectMany(e => e.DiscoverServices(this)))
			{
				ServiceCollection.Add(serviceDescriptor);
			}

			return ServiceCollection;
		}
	}
}