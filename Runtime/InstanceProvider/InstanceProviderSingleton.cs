
namespace EM.Foundation
{
	public sealed class InstanceProviderSingleton :
		IInstanceProvider
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			if (instance == null)
			{
				instance = instanceProvider.GetInstance();
			}

			return instance;
		}

		#endregion
		#region InstanceProviderSingleton

		private readonly IInstanceProvider instanceProvider;

		private object instance;

		public InstanceProviderSingleton(
			IInstanceProvider instanceProvider)
		{
			Requires.IsNotNull(instanceProvider, nameof(instanceProvider));

			this.instanceProvider = instanceProvider;
		}

		#endregion
	}
}
