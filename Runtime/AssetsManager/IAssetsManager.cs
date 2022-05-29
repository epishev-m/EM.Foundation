namespace EM.Foundation
{

using System.Threading.Tasks;
using UnityEngine;

public interface IAssetsManager
{
	Task<GameObject> InstantiateAsync(string path,
		Transform parent = null);

	Task<T> InstantiateAsync<T>(string path,
		Transform parent = null)
		where T : Component;

	bool ReleaseInstance(GameObject gameObject);
}

}