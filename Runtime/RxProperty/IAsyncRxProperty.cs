namespace EM.Foundation
{

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public interface IAsyncRxProperty<out T>
{
	event Func<T, CancellationToken, UniTask> OnChanged;

	T Value
	{
		get;
	}
}

}