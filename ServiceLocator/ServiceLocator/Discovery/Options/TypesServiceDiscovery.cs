using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLocator.Discovery.Options
{
	/// <summary>
	///		Loads all types that are set in its constructor
	/// </summary>
	public class TypesServiceDiscovery : IServiceDiscovery
	{
		private readonly IEnumerable<Type> _assembly;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="assembly"></param>
		public TypesServiceDiscovery(IEnumerable<Type> assembly)
		{
			_assembly = assembly;
		}

		/// <inheritdoc />
		public IEnumerable<Type> DiscoverTypes(Func<Type, bool> filter)
		{
			return _assembly.Where(filter);
		}
	}
}