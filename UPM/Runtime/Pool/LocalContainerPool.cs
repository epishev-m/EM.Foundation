﻿namespace EM.Foundation
{

using System.Collections.Concurrent;
using UnityEngine;

public class LocalContainerPool<T> : IPool<T>
	where T : MonoBehaviour
{
	private readonly ConcurrentBag<T> _instances = new();

	#region IPool

	public int Count => _instances.Count;

	public Result<T> GetObject()
	{
		if (_instances.TryTake(out var obj))
		{
			obj.gameObject.SetActive(true);

			return new SuccessResult<T>(obj);
		}

		return new ErrorResult<T>("No elements available");
	}

	public Result PutObject(T obj)
	{
		if (obj == null)
		{
			return new ErrorResult(PoolStringResources.PutNullObject(this));
		}

		if (obj is IPoolable {IsRestored: false} poolItem)
		{
			poolItem.Restore();
		}

		obj.gameObject.SetActive(false);
		_instances.Add(obj);

		return new SuccessResult();
	}

	#endregion

	#region LocalContainerPool

	public LocalContainerPool(Component container)
	{
		var objects = container.GetComponentsInChildren<T>(true);
		var containerObj = container.GetComponent<T>();

		foreach (var obj in objects)
		{
			if (containerObj != obj)
			{
				PutObject(obj);
			}
		}
	}

	#endregion
}

}