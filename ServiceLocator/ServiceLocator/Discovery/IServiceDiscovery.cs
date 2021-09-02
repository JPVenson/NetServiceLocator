using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Defines a way to discover services
	/// </summary>
	public interface IServiceDiscovery
	{
		/// <summary>
		///		Loads types that should be discovered
		/// </summary>
		/// <returns>A list of services</returns>
		IEnumerable<ServiceDescriptor> DiscoverServices(IServiceDiscoveryManager locator);
	}
}