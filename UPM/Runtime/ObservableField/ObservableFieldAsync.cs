#if UNITY_64 || UNITY_ANDROID || UNITY_IOS

namespace EM.Foundation
{

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

public sealed class ObservableFieldAsync<T> : IObservableFieldAsync<T>
{
	private T _value;

	#region IAsyncRxProperty

	public event Func<T, CancellationToken, UniTask> OnChanged;

	public T Value => _value;

	#endregion

	#region AsyncRxProperty

	public UniTask SetValueAsync(T value,
		CancellationToken ct)
	{
		if (EqualityComparer<T>.Default.Equals(_value, value))
		{
			return UniTask.CompletedTask;
		}

		_value = value;

		if (OnChanged == null)
		{
			return UniTask.CompletedTask;
		}

		var tasks = OnChanged.GetInvocationList()
			.Cast<Func<T, CancellationToken, UniTask>>()
			.Select(func => func(_value, ct));

		return UniTask.WhenAll(tasks);
	}

	public void SetValueWithoutNotify(T value)
	{
		_value = value;
	}

	#endregion
}

}

#endif