#if UNITY_64 || UNITY_ANDROID || UNITY_IOS

namespace EM.Foundation
{

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public interface IObservableFieldAsync<out T>
{
	event Func<T, CancellationToken, UniTask> OnChanged;

	T Value
	{
		get;
	}
}

}

#endif