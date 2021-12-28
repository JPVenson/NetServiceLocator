using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator.Discovery
{
	/// <summary>
	///		Defines a list of all <see cref="IServiceDiscovery"/> and allows to load types from it.
	/// </summary>
	public interface IServiceDiscoveryManager
	{
		/// <summary>
		///		The target of the Service injection
		/// </summary>
		IServiceCollection ServiceCollection { get; }

		/// <summary>
		///		Gets a list of all <see cref="IServiceDiscovery"/>
		/// </summary>
		/// <returns></returns>
		IList<IServiceDiscovery> ServiceTypes { get; }

		/// <summary>
		///		Imports all types from <see cref="ServiceTypes"/> into <see cref="ServiceCollection"/>
		/// </summary>
		IServiceCollection LocateServices();
	}
}