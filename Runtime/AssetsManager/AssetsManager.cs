namespace EM.Foundation
{

using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public sealed class AssetsManager :
	IAssetsManager
{
	#region IAssetsManager

	public async UniTask<GameObject> InstantiateAsync(string path,
		CancellationToken ct)
	{
		return await InstantiateAsync(path, null, ct);
	}

	public async UniTask<GameObject> InstantiateAsync(string path,
		Transform parent,
		CancellationToken ct)
	{
		ct.ThrowIfCancellationRequested();

		var handler = parent == null
			? Addressables.InstantiateAsync(path)
			: Addressables.InstantiateAsync(path, parent);

		var gameObject = await handler.Task;

		if (handler.Status != AsyncOperationStatus.Succeeded)
		{
			return default;
		}

		gameObject.name = path;

		return gameObject;
	}

	public async UniTask<T> InstantiateAsync<T>(string path,
		CancellationToken ct)
		where T : Component
	{
		return await InstantiateAsync<T>(path, null, ct);
	}

	public async UniTask<T> InstantiateAsync<T>(string path,
		Transform parent,
		CancellationToken ct)
		where T : Component
	{
		var gameObject = await InstantiateAsync(path, parent, ct);

		if (gameObject.TryGetComponent(out T component) == false)
		{
			throw new NullReferenceException(
				$"Object of type {typeof(T)} is null on attempt to load it from addressables");
		}

		return component;
	}

	public bool ReleaseInstance(GameObject gameObject)
	{
		Requires.NotNull(gameObject, nameof(gameObject));

		gameObject.SetActive(false);
		var result = Addressables.ReleaseInstance(gameObject);

		return result;
	}

	#endregion
}

}