
namespace EM.Foundation
{
	using System;
	
	public sealed class InstanceProvider :
		IInstanceProvider
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			return _instance;
		}

		#endregion
		#region InstanceProvider

		private readonly object _instance;

		public InstanceProvider(
			object instance)
		{
			Requires.IsNotNull(instance, nameof(instance));

			_instance = instance;
		}

		#endregion
	}
}
