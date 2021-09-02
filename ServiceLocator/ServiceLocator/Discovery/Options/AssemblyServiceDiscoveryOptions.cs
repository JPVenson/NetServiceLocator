using System;

namespace ServiceLocator.Discovery.Options
{
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