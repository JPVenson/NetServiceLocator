using System;

namespace ServiceLocator.Attributes
{
	/// <summary>
	///		Defines the Service types that <see cref="WithAutoRegistrationAttribute"/> should include
	/// </summary>
	[Flags]
	public enum RegistrationKind
	{
		All = BaseClasses | Interfaces,
		/// <summary>
		///		Include all bases classes
		/// </summary>
		BaseClasses = 1 << 0,

		/// <summary>
		///		Include all interfaces of that type (not inherted ones)
		/// </summary>
		Interfaces = 1 << 1
	}
}