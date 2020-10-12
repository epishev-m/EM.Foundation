using System;

namespace EM.Foundation
{
	public sealed class InstanceProviderActivator<T> : IInstanceProvider
		where T : class, new()
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			var instance = Activator.CreateInstance<T>();

			return instance;
		}

		#endregion
	}
}
