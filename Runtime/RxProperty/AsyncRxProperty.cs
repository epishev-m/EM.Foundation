using System.Threading.Tasks;

namespace EM.Foundation
{

using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

public sealed class AsyncRxProperty<T> : IAsyncRxProperty<T>
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