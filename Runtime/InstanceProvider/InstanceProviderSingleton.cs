namespace EM.Foundation
{

public sealed class InstanceProviderSingleton :
	IInstanceProvider
{
	private readonly IInstanceProvider instanceProvider;

	private object instance;

	#region IInstanceProvider

	public object GetInstance()
	{
		return instance ??= instanceProvider.GetInstance();
	}

	#endregion
	#region InstanceProviderSingleton

	public InstanceProviderSingleton(
		IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		this.instanceProvider = instanceProvider;
	}

	#endregion
}

}
