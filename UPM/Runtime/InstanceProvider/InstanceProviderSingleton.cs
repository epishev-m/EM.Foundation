namespace EM.Foundation
{

public sealed class InstanceProviderSingleton : IInstanceProvider
{
	private readonly IInstanceProvider _instanceProvider;

	private object _instance;

	#region IInstanceProvider

	public Result<object> GetInstance()
	{
		if (_instance != null)
		{
			return new SuccessResult<object>(_instance);
		}

		var result = _instanceProvider.GetInstance();

		if (result.Success)
		{
			_instance = result.Data;
		}

		return result;
	}

	#endregion

	#region InstanceProviderSingleton

	public InstanceProviderSingleton(IInstanceProvider instanceProvider)
	{
		Requires.NotNullParam(instanceProvider, nameof(instanceProvider));

		_instanceProvider = instanceProvider;
	}

	#endregion
}

}