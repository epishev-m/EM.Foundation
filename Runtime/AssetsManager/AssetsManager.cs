namespace EM.Foundation
{

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public sealed class AssetsManager :
	IAssetsManager
{
	#region IAssetsManager

	public async Task<GameObject> InstantiateAsync(string path,
		Transform parent = null)
	{
		var handle = parent == null
			? Addressables.InstantiateAsync(path)
			: Addressables.InstantiateAsync(path, parent);

		var gameObject = await handle.Task;

		if (handle.Status != AsyncOperationStatus.Succeeded)
		{
			return default;
		}

		gameObject.name = path;

		return gameObject;
	}

	public async Task<T> InstantiateAsync<T>(string path,
		Transform parent = null)
		where T : Component
	{
		var gameObject = await InstantiateAsync(path, parent);

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