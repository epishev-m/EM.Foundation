namespace EM.Foundation
{

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public interface IAssetsManager
{
	Result<GameObject> Instantiate(string key);

	Result<GameObject> Instantiate(string key,
		Transform parent);

	UniTask<Result<GameObject>> InstantiateAsync(string key,
		CancellationToken ct);

	UniTask<Result<GameObject>> InstantiateAsync(string key,
		Transform parent,
		CancellationToken ct);

	Result<T> Instantiate<T>(string key)
		where T : Component;

	Result<T> Instantiate<T>(string key,
		Transform parent)
		where T : Component;

	UniTask<Result<T>> InstantiateAsync<T>(string key,
		CancellationToken ct)
		where T : Component;

	UniTask<Result<T>> InstantiateAsync<T>(string key,
		Transform parent,
		CancellationToken ct)
		where T : Component;

	Result<T> LoadAsset<T>(string key)
		where T : Object;

	Result ReleaseInstance(GameObject gameObject);

	void ReleaseAsset<T>(T asset)
		where T : Object;
}

}