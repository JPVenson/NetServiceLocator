using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ServiceLocator.Discovery.Service.Options;

namespace ServiceLocator.Discovery.Option
{
	/// <summary>
	///		Discovers Services that annotates <see cref="ServiceLocator.Attributes.ServiceAttribute"/> in all currently loaded assemblies
	/// </summary>
	public class AppDomainOptionDiscovery : TypeBasedOptionDiscovery
	{
		private readonly AppDomainServiceDiscoveryOptions _discoveryOptions;

		/// <summary>
		/// 
		/// </summary>
		public AppDomainOptionDiscovery(IConfiguration configuration) : this(new AppDomainServiceDiscoveryOptions(), configuration)
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="discoveryOptions"></param>
		/// <param name="configuration"></param>
		public AppDomainOptionDiscovery(AppDomainServiceDiscoveryOptions discoveryOptions, IConfiguration configuration) : base(configuration)
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