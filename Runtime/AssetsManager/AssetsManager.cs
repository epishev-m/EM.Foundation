namespace EM.Foundation
{

using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public sealed class AssetsManager : IAssetsManager
{
	#region IAssetsManager

	public async UniTask<GameObject> InstantiateAsync(string key,
		CancellationToken ct)
	{
		return await InstantiateAsync(key, null, ct);
	}

	public async UniTask<GameObject> InstantiateAsync(string key,
		Transform parent,
		CancellationToken ct)
	{
		ct.ThrowIfCancellationRequested();

		var handler = parent == null
			? Addressables.InstantiateAsync(key)
			: Addressables.InstantiateAsync(key, parent);

		var gameObject = await handler.Task;

		if (handler.Status != AsyncOperationStatus.Succeeded)
		{
			return default;
		}

		gameObject.name = key;

		return gameObject;
	}

	public async UniTask<T> InstantiateAsync<T>(string key,
		CancellationToken ct)
		where T : Component
	{
		return await InstantiateAsync<T>(key, null, ct);
	}

	public async UniTask<T> InstantiateAsync<T>(string key,
		Transform parent,
		CancellationToken ct)
		where T : Component
	{
		var gameObject = await InstantiateAsync(key, parent, ct);

		if (gameObject.TryGetComponent(out T component) == false)
		{
			throw new NullReferenceException(
				$"Object of type {typeof(T)} is null on attempt to load it from addressables");
		}

		return component;
	}

	public T LoadAsset<T>(string key)
		where T : UnityEngine.Object
	{
		var operationHandle =  Addressables.LoadAssetAsync<T>(key);
		var asset = operationHandle.WaitForCompletion();

		return asset;
	}

	public void ReleaseAsset<T>(T asset)
		where T : UnityEngine.Object
	{
		Addressables.Release(asset);
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