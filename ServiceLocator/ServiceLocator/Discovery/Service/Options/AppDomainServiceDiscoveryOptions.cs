using System;
using System.Reflection;

namespace ServiceLocator.Discovery.Service.Options
{
	/// <summary>
	/// 
	/// </summary>
	public class AppDomainServiceDiscoveryOptions
	{
		/// <summary>
		/// 
		/// </summary>
		public AppDomainServiceDiscoveryOptions()
		{
			FilterAssembly = FilterAssemblyImpl;
			FilterType = FilterTypeImpl;
		}

		private bool FilterTypeImpl(Type arg)
		{
			return true;
		}

		private bool FilterAssemblyImpl(Assembly arg)
		{
			return true;
		}

		/// <summary>
		///		Can be set to filter the Assemblies to be loaded by the <see cref="AppDomainServiceDiscovery"/>
		/// </summary>
		public Func<Assembly, bool> FilterAssembly { get; set; }
		
		/// <summary>
		///		Can be set to filter the types to be loaded by the <see cref="AppDomainServiceDiscovery"/>
		/// </summary>
		public Func<Type, bool> FilterType { get; set; }
	}
}