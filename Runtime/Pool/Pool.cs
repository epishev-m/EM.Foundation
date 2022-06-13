namespace EM.Foundation
{

using System;
using System.Collections.Concurrent;

public class Pool<T> :
	IPool<T>
	where T : class
{
	private readonly ConcurrentBag<T> instances = new ConcurrentBag<T>();

	private readonly IInstanceProvider instanceProvider;

	#region IPool

	public int Count => instances.Count;

	public T GetObject()
	{
		if (instances.TryTake(out var item))
		{
			return item;
		}

		if (instanceProvider == null)
		{
			return item;
		}

		var instance = instanceProvider.GetInstance() as T;
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

		instances.Add(obj);
	}

	#endregion

	#region Pool

	public Pool()
	{
		instanceProvider = null;
	}

	public Pool(IInstanceProvider instanceProvider)
	{
		Requires.NotNull(instanceProvider, nameof(instanceProvider));

		this.instanceProvider = instanceProvider;
	}

	#endregion
}

}