using Cysharp.Threading.Tasks;

namespace EM.Foundation
{

using System.Threading;
using UnityEngine;

public interface IAssetsManager
{
	UniTask<Result<GameObject>> InstantiateAsync(string key,
		CancellationToken ct);

	UniTask<Result<GameObject>> InstantiateAsync(string key,
		Transform parent,
		CancellationToken ct);

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