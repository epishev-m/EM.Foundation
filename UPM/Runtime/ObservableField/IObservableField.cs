#if UNITY_64 || UNITY_ANDROID || UNITY_IOS

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

#endif