namespace EM.Foundation
{

using System;

public interface IRxProperty<out T>
{
	event Action<T> OnChanged;

	T Value
	{
		get;
	}

	void Subscribe(Action<T> handler);

	void UnSubscribe(Action<T> handler);
}

}