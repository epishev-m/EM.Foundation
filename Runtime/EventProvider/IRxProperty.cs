namespace EM.Foundation
{

public interface IRxProperty<out T> : IEventProvider
{
	T Value
	{
		get;
	}
}

}