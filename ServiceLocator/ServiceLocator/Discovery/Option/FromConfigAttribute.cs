using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace ServiceLocator.Discovery.Option
{
	/// <summary>
	///     Enables the loading from the annotated type as an <see cref="IOptions{TOptions}" />
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class FromConfigAttribute : Attribute
	{
		/// <summary>
		///     Creates a new <see cref="FromConfigAttribute" />. Uses the <see cref="ConfigurationNode" /> to construct a new
		///     IOption from its declaring type and config node
		/// </summary>
		/// <param name="configurationNode"></param>
		public FromConfigAttribute(string configurationNode)
		{
			ConfigurationNode = configurationNode;
		}

		/// <summary>
		///     The node in the IConfiguration section to construct from
		/// </summary>
		public string ConfigurationNode { get; }

		public IEnumerable<ServiceDescriptor> GetOptionDescriptors(Type type, IConfiguration configuration)
		{
			var configurationSection = configuration.GetSection(ConfigurationNode);
			var changeTokenRegType = typeof(IOptionsChangeTokenSource<>).MakeGenericType(type);
			var changeTokenInstance = Activator.CreateInstance(typeof(ConfigurationChangeTokenSource<>).MakeGenericType(type),
			                                                   configurationSection);


			var configurationType = typeof(IConfigureOptions<>).MakeGenericType(type);

			var configurationInstance = 
				typeof(FromConfigAttribute).GetMethod(nameof(GetConfigInstanceFactory), BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(type)
				.Invoke(null, new object[]
				{
					configuration,
					ConfigurationNode
				});
			//var configurationInstance = Activator.CreateInstance(typeof(ConfigureNamedOptions<>).MakeGenericType(type),
			//                                                     Microsoft.Extensions.Options.Options.DefaultName,
			//                                                     new Action<BinderOptions>(binderOptions =>
			//                                                     {
			//	                                                     configuration.Bind(ConfigurationNode, binderOptions);
			//                                                     }));

			yield return new ServiceDescriptor(changeTokenRegType, changeTokenInstance);
			yield return new ServiceDescriptor(configurationType, configurationInstance);
		}

		private static IConfigureOptions<TOption> GetConfigInstanceFactory<TOption>(IConfiguration config, string sectionName) where TOption : class
		{
			return new ConfigureNamedOptions<TOption>(Microsoft.Extensions.Options.Options.DefaultName, option =>
			{
				config.Bind(sectionName, option);
			});
		}

		public static bool HasConfigDescriptor(Type arg)
		{
			return arg.GetCustomAttribute<FromConfigAttribute>(false) != null;
		}

		/// <summary>
		///     Creates <see cref="IChangeToken" />s so that <see cref="IOptionsMonitor{TOptions}" /> gets
		///     notified when <see cref="IConfiguration" /> changes.
		/// </summary>
		/// <typeparam name="TOptions"></typeparam>
		internal class ConfigurationChangeTokenSource<TOptions> : IOptionsChangeTokenSource<TOptions>
		{
			private readonly IConfiguration _config;

			/// <summary>
			///     Constructor taking the <see cref="IConfiguration" /> instance to watch.
			/// </summary>
			/// <param name="config">The configuration instance.</param>
			public ConfigurationChangeTokenSource(IConfiguration config) : this(Microsoft.Extensions.Options.Options.DefaultName, config)
			{
			}

			/// <summary>
			///     Constructor taking the <see cref="IConfiguration" /> instance to watch.
			/// </summary>
			/// <param name="name">The name of the options instance being watched.</param>
			/// <param name="config">The configuration instance.</param>
			public ConfigurationChangeTokenSource(string name, IConfiguration config)
			{
				if (config == null)
				{
					throw new ArgumentNullException(nameof(config));
				}

				_config = config;
				Name = name ?? Microsoft.Extensions.Options.Options.DefaultName;
			}

			/// <summary>
			///     The name of the option instance being changed.
			/// </summary>
			public string Name { get; }

			/// <summary>
			///     Returns the reloadToken from the <see cref="IConfiguration" />.
			/// </summary>
			/// <returns></returns>
			public IChangeToken GetChangeToken()
			{
				return _config.GetReloadToken();
			}
		}
	}
}