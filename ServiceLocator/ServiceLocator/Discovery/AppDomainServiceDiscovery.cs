using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLocator.Discovery.Options;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Discovers Services that annotates <see cref="ServiceLocator.Attributes.ServiceAttribute"/> in all currently loaded assemblies
	/// </summary>
	public class AppDomainServiceDiscovery : TypeBasedServiceDiscovery
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
		
		protected override IEnumerable<Type> DiscoverTypes()
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.Where(_discoveryOptions.FilterAssembly)
				.SelectMany(e => e.GetTypes().Where(_discoveryOptions.FilterType));
		}
	}
}