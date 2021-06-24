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
		var factory = instance as IFactory ?? throw new Exception();

		if (factory.TryCreate(out var result) == false)
		{
			throw new Exception("Failed to create object.");
		}

		return result;
	}

	#endregion
	#region InstanceProviderFactory

	public InstanceProviderFactory(
		IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		this.instanceProvider = instanceProvider;
	}

	#endregion
}

}
