namespace EM.Foundation
{

using System;

public interface IObservableField<out T>
{
	event Action<T> OnChanged;

	T Value
	{
		get;
	}
}

}