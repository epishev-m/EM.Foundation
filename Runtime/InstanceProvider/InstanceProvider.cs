namespace EM.Foundation
{

public class InstanceProvider :
	IInstanceProvider
{
	private readonly object _instance;

	#region IInstanceProvider

	public Result<object> GetInstance()
	{
		return new SuccessResult<object>(_instance);
	}

	#endregion

	#region InstanceProvider

	public InstanceProvider(object instance)
	{
		Requires.NotNullParam(instance, nameof(instance));

		_instance = instance;
	}

	#endregion
}

}
