using Cysharp.Threading.Tasks;

namespace EM.Foundation
{

using System.Threading;
using UnityEngine;

public interface IAssetsManager
{
	UniTask<GameObject> InstantiateAsync(string key,
		CancellationToken ct);

	UniTask<GameObject> InstantiateAsync(string key,
		Transform parent,
		CancellationToken ct);

	UniTask<T> InstantiateAsync<T>(string key,
		CancellationToken ct)
		where T : Component;

	UniTask<T> InstantiateAsync<T>(string key,
		Transform parent,
		CancellationToken ct)
		where T : Component;

	T LoadAsset<T>(string key)
		where T : Object;

	bool ReleaseInstance(GameObject gameObject);

	void ReleaseAsset<T>(T asset)
		where T : Object;
}

}