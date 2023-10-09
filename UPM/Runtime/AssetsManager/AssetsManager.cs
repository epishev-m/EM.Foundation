namespace EM.Foundation
{

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public sealed class AssetsManager : IAssetsManager
{
	#region IAssetsManager

	public Result<GameObject> Instantiate(string key)
	{
		return Instantiate(key, null);
	}

	public Result<GameObject> Instantiate(string key,
		Transform parent)
	{
		var gameObject = parent == null
			? Addressables.InstantiateAsync(key).WaitForCompletion()
			: Addressables.InstantiateAsync(key, parent).WaitForCompletion();

		if (gameObject == null)
		{
			return new ErrorResult<GameObject>(AssetsManagerStringResources.FailedLoaded(this));
		}

		gameObject.name = key;

		return new SuccessResult<GameObject>(gameObject);
	}

	public async UniTask<Result<GameObject>> InstantiateAsync(string key,
		CancellationToken ct)
	{
		return await InstantiateAsync(key, null, ct);
	}

	public async UniTask<Result<GameObject>> InstantiateAsync(string key,
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
			return new ErrorResult<GameObject>(AssetsManagerStringResources.FailedLoaded(this));
		}

		gameObject.name = key;

		return new SuccessResult<GameObject>(gameObject);
	}

	public Result<T> Instantiate<T>(string key)
		where T : Component
	{
		return Instantiate<T>(key, null);
	}

	public Result<T> Instantiate<T>(string key,
		Transform parent)
		where T : Component
	{
		var result = Instantiate(key, parent);

		if (result.Failure)
		{
			return new ErrorResult<T>(AssetsManagerStringResources.FailedLoaded(this));
		}

		if (result.Data.TryGetComponent(out T component) == false)
		{
			return new ErrorResult<T>(AssetsManagerStringResources.FailedLoaded(this));
		}

		return new SuccessResult<T>(component);
	}

	public async UniTask<Result<T>> InstantiateAsync<T>(string key,
		CancellationToken ct)
		where T : Component
	{
		return await InstantiateAsync<T>(key, null, ct);
	}

	public async UniTask<Result<T>> InstantiateAsync<T>(string key,
		Transform parent,
		CancellationToken ct)
		where T : Component
	{
		var result = await InstantiateAsync(key, parent, ct);

		if (result.Failure)
		{
			return new ErrorResult<T>(AssetsManagerStringResources.FailedLoaded(this));
		}

		if (result.Data.TryGetComponent(out T component) == false)
		{
			return new ErrorResult<T>(AssetsManagerStringResources.FailedLoaded(this));
		}

		return new SuccessResult<T>(component);
	}

	public Result<T> LoadAsset<T>(string key)
		where T : Object
	{
		var operationHandle =  Addressables.LoadAssetAsync<T>(key);
		var asset = operationHandle.WaitForCompletion();

		if (operationHandle.Status == AsyncOperationStatus.Failed)
		{
			return new ErrorResult<T>(AssetsManagerStringResources.FailedLoaded(this));
		}
		
		return new SuccessResult<T>(asset);
	}

	public void ReleaseAsset<T>(T asset)
		where T : Object
	{
		Addressables.Release(asset);
	}

	public Result ReleaseInstance(GameObject gameObject)
	{
		if (gameObject == null)
		{
			return new ErrorResult(AssetsManagerStringResources.GameObjectNull(this));
		}

		gameObject.SetActive(false);
		var result = Addressables.ReleaseInstance(gameObject);

		if (result)
		{
			return new SuccessResult();
		}

		return new ErrorResult(AssetsManagerStringResources.FailedLoaded(this));
	}

	#endregion
}

}