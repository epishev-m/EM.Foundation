using Cysharp.Threading.Tasks;

namespace EM.Foundation
{

using System.Threading.Tasks;
using UnityEngine;

public interface IAssetsManager
{
	UniTask<GameObject> InstantiateAsync(string path,
		Transform parent = null);

	UniTask<T> InstantiateAsync<T>(string path,
		Transform parent = null)
		where T : Component;

	bool ReleaseInstance(GameObject gameObject);
}

}