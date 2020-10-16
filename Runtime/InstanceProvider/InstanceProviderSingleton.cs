
namespace EM.Foundation
{
	using System;
	
	public sealed class InstanceProviderSingleton :
		IInstanceProvider
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			if (_instance == null)
			{
				_instance = _instanceProvider.GetInstance();
			}

			return _instance;
		}

		#endregion
		#region InstanceProviderSingleton

		private readonly IInstanceProvider _instanceProvider;

		private object _instance;

		public InstanceProviderSingleton(
			IInstanceProvider instanceProvider)
		{
			_instanceProvider = instanceProvider ??
				throw new ArgumentNullException(nameof(instanceProvider));
		}

		#endregion
	}
}
