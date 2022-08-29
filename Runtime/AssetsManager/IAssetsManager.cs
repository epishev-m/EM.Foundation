using Cysharp.Threading.Tasks;

namespace EM.Foundation
{

using System.Threading;
using UnityEngine;

public interface IAssetsManager
{
	UniTask<GameObject> InstantiateAsync(string path,
		CancellationToken ct);

	UniTask<GameObject> InstantiateAsync(string path,
		Transform parent,
		CancellationToken ct);

	UniTask<T> InstantiateAsync<T>(string path,
		CancellationToken ct)
		where T : Component;

	UniTask<T> InstantiateAsync<T>(string path,
		Transform parent,
		CancellationToken ct)
		where T : Component;

	bool ReleaseInstance(GameObject gameObject);
}

}