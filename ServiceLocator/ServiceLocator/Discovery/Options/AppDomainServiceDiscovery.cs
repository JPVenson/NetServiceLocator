using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace ServiceLocator.Discovery.Options
{
	/// <summary>
	///		Discovers Services that attributes <see cref="ServiceLocator.Attributes.ServiceAttribute"/> in all currently loaded services
	/// </summary>
	public class AppDomainServiceDiscovery : IServiceDiscovery
	{
		private readonly AppDomainServiceDiscoveryOptions _discoveryOptions;

		/// <summary>
		/// 
		/// </summary>
		public AppDomainServiceDiscovery() : this(new AppDomainServiceDiscoveryOptions())
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="discoveryOptions"></param>
		public AppDomainServiceDiscovery(AppDomainServiceDiscoveryOptions discoveryOptions)
		{
			_discoveryOptions = discoveryOptions;
		}

		/// <inheritdoc />
		public IEnumerable<Type> DiscoverTypes(Func<Type, bool> filter)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.Where(_discoveryOptions.FilterAssembly)
				.SelectMany(e => e.GetTypes().Where(_discoveryOptions.FilterType).Where(filter));
		}
	}
}