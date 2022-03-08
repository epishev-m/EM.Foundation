namespace EM.Foundation
{
using System;

public sealed class InstanceProviderFactory :
	IInstanceProvider
{
	private readonly IInstanceProvider instanceProvider;

	#region IInstanceProvider

	public object GetInstance()
	{
		var instance = instanceProvider.GetInstance();

		Requires.Type<IFactory>(instance, nameof(instance));

		var factory = (IFactory) instance;

		if (factory.TryCreate(out var result) == false)
		{
			throw new Exception("Failed to create object.");
		}

		return result;
	}

	#endregion
	#region InstanceProviderFactory

	public InstanceProviderFactory(IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		this.instanceProvider = instanceProvider;
	}

	#endregion
}

}
