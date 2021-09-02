using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ServiceLocator.Discovery.Options;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Loads all types from the given <see cref="Assembly"/>
	/// </summary>
	public class AssemblyServiceDiscovery : TypeBasedServiceDiscovery
	{
		private readonly Assembly _assembly;
		private readonly AssemblyServiceDiscoveryOptions _options;

		public AssemblyServiceDiscovery(Assembly assembly) : this(assembly, new AssemblyServiceDiscoveryOptions())
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public AssemblyServiceDiscovery(Assembly assembly, AssemblyServiceDiscoveryOptions options)
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