namespace EM.Foundation
{

public sealed class InstanceProviderFactory : IInstanceProvider
{
	private readonly IInstanceProvider _instanceProvider;

	#region IInstanceProvider

	public object GetInstance()
	{
		var instance = _instanceProvider.GetInstance();
		var factory = instance as IFactory;

		Requires.ValidOperation(factory != null, this);

		if (factory != null && factory.TryCreate(out var result))
		{
			return result;
		}

		Requires.ValidOperation(false, this);

		return null;
	}

	#endregion

	#region InstanceProviderFactory

	public InstanceProviderFactory(IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		_instanceProvider = instanceProvider;
	}

	#endregion
}

}