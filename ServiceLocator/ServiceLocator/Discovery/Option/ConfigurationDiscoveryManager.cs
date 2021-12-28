using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Discovery.Option
{
	internal class ConfigurationDiscoveryManager : IConfigurationDiscoveryManager
	{
		private readonly IServiceDiscoveryManager _discoveryManager;

		public ConfigurationDiscoveryManager(IServiceDiscoveryManager discoveryManager, IConfiguration configuration)
		{
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_discoveryManager = discoveryManager ?? throw new ArgumentNullException(nameof(discoveryManager));
		}

		/// <inheritdoc />
		public IServiceCollection ServiceCollection
		{
			get { return _discoveryManager.ServiceCollection; }
		}

		/// <inheritdoc />
		public IList<IServiceDiscovery> ServiceTypes
		{
			get { return _discoveryManager.ServiceTypes; }
		}

		/// <inheritdoc />
		public IServiceCollection LocateServices()
		{
			return _discoveryManager.LocateServices();
		}

		/// <inheritdoc />
		public IConfiguration Configuration { get; }
	}
}