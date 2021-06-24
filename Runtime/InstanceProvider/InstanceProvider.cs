namespace EM.Foundation
{

public sealed class InstanceProvider :
	IInstanceProvider
{
	private readonly object instance;

	#region IInstanceProvider

	public object GetInstance()
	{
		return instance;
	}

	#endregion
	#region InstanceProvider

	public InstanceProvider(
		object instance)
	{
		Requires.NotNull(instance, nameof(instance));

		this.instance = instance;
	}

	#endregion
}

}
