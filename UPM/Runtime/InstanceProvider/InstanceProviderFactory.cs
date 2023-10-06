namespace EM.Foundation
{

public sealed class InstanceProviderFactory : IInstanceProvider
{
	private readonly IInstanceProvider _instanceProvider;

	#region IInstanceProvider

	public Result<object> GetInstance()
	{
		var resultFactory = _instanceProvider.GetInstance();

		if (resultFactory.Failure)
		{
			return new ErrorResult<object>(InstanceProviderStringResources.FailedToGetFactory(this));
		}

		var factory = (IFactory) resultFactory.Data;
		var instanceResult = factory.Create();

		if (instanceResult.Success)
		{
			return new SuccessResult<object>(instanceResult.Data);
		}

		return new ErrorResult<object>(InstanceProviderStringResources.FailedToCreateInstance(this));
	}

	#endregion

	#region InstanceProviderFactory

	public InstanceProviderFactory(IInstanceProvider instanceProvider)
	{
		Requires.NotNullParam(instanceProvider, nameof(instanceProvider));

		_instanceProvider = instanceProvider;
	}

	#endregion
}

}