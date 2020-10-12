using System;

namespace EM.Foundation
{
	public sealed class InstanceProvider : IInstanceProvider
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			return _instance;
		}

		#endregion
		#region InstanceProvider

		private readonly object _instance;

		public InstanceProvider(object instance)
		{
			_instance = instance ?? throw new ArgumentNullException(nameof(instance));
		}

		#endregion
	}
}
