namespace EM.Foundation
{

using System;
using System.Collections.Concurrent;

public class Pool<T> : IPool<T>
	where T : class
{
	private readonly ConcurrentBag<T> _instances = new();

	private readonly IInstanceProvider _instanceProvider;

	#region IPool

	public int Count => _instances.Count;

	public T GetObject()
	{
		if (_instances.TryTake(out var item))
		{
			return item;
		}

		if (_instanceProvider == null)
		{
			return item;
		}

		var instance = _instanceProvider.GetInstance() as T;
		item = instance ?? throw new Exception();

		return item;
	}

	public void PutObject(T obj)
	{
		Requires.NotNull(obj, nameof(obj));

		if (obj is IPoolable {IsRestored: false} poolItem)
		{
			poolItem.Restore();
		}

		_instances.Add(obj);
	}

	#endregion

	#region Pool

	public Pool()
	{
		_instanceProvider = null;
	}

	public Pool(IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		_instanceProvider = instanceProvider;
	}

	#endregion
}

}