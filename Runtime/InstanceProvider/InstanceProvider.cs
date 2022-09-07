namespace EM.Foundation
{

public class InstanceProvider :
	IInstanceProvider
{
	private readonly object _instance;

	#region IInstanceProvider

	public object GetInstance()
	{
		return _instance;
	}

	#endregion

	#region InstanceProvider

	public InstanceProvider(object instance)
	{
		Requires.NotNull(instance, nameof(instance));

		_instance = instance;
	}

	#endregion
}

}
