namespace EM.Foundation
{

public sealed class InstanceProviderSingleton : IInstanceProvider
{
	private readonly IInstanceProvider _instanceProvider;

	private object _instance;

	#region IInstanceProvider

	public object GetInstance()
	{
		return _instance ??= _instanceProvider.GetInstance();
	}

	#endregion

	#region InstanceProviderSingleton

	public InstanceProviderSingleton(IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		_instanceProvider = instanceProvider;
	}

	#endregion
}

}