
namespace EM.Foundation
{
	using System;

	public sealed class InstanceProviderFactory :
		IInstanceProvider
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			var instance = instanceProvider.GetInstance();
			var factory = instance as IFactory ?? throw new Exception();

			if (factory.TryCreate(out var result) == false)
			{
				throw new Exception("Failed to create object.");
			}

			return result;
		}

		#endregion
		#region InstanceProviderFactory


		private readonly IInstanceProvider instanceProvider;

		public InstanceProviderFactory(
			IInstanceProvider instanceProvider)
		{
			Requires.IsNotNull(instanceProvider, nameof(instanceProvider));

			this.instanceProvider = instanceProvider;
		}

		#endregion
	}
}
