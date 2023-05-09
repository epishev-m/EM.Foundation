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
}

}