using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceLocator.Discovery.Options
{
	/// <summary>
	///		Loads all types from the given <see cref="Assembly"/>
	/// </summary>
	public class AssemblyServiceDiscovery : IServiceDiscovery
	{
		private readonly Assembly _assembly;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="assembly"></param>
		public AssemblyServiceDiscovery(Assembly assembly)
		{
			_assembly = assembly;
		}

		/// <inheritdoc />
		public IEnumerable<Type> DiscoverTypes(Func<Type, bool> filter)
		{
			return _assembly.GetTypes().Where(filter);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class AssemblyServiceDiscoveryOptions
	{
		/// <summary>
		/// 
		/// </summary>
		public AssemblyServiceDiscoveryOptions()
		{
			TypeFilter = FilterTypeImpl;
		}

		/// <summary>
		///		Can be set to filter the types to be loaded by the <see cref="AppDomainServiceDiscovery"/>
		/// </summary>
		public Func<Type, bool> TypeFilter { get; set; }

		private bool FilterTypeImpl(Type arg)
		{
			return true;
		}
	}
}