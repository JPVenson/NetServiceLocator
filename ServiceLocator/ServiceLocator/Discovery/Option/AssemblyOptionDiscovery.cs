using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ServiceLocator.Discovery.Service.Options;

namespace ServiceLocator.Discovery.Option
{
	/// <summary>
	///		Loads all types from the given <see cref="Assembly"/>
	/// </summary>
	public class AssemblyOptionDiscovery : TypeBasedOptionDiscovery
	{
		private readonly Assembly _assembly;
		private readonly AssemblyServiceDiscoveryOptions _options;

		public AssemblyOptionDiscovery(Assembly assembly, IConfiguration configuration) : this(assembly, new AssemblyServiceDiscoveryOptions(), configuration)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public AssemblyOptionDiscovery(Assembly assembly, AssemblyServiceDiscoveryOptions options, IConfiguration configuration) : base(configuration)
		{
			_assembly = assembly;
			_options = options;
		}

		/// <inheritdoc />
		protected override IEnumerable<Type> DiscoverTypes()
		{
			return _assembly.GetTypes().Where(_options.TypeFilter);
		}
	}
}