using System;

namespace ServiceLocator.Attributes
{
	/// <summary>
	///		If used with <see cref="ServiceAttribute"/> allows to automatically register all used interfaces or base classes 
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class WithAutoRegistrationAttribute : Attribute
	{
		public WithAutoRegistrationAttribute()
		{
			RegistrationKind = RegistrationKind.BaseClasses | RegistrationKind.Interfaces;
		}

		public RegistrationKind RegistrationKind { get; set; }
	}
}