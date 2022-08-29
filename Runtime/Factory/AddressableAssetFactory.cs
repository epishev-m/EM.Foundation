namespace EM.Foundation
{

using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class AddressableAssetFactory<T> : IFactory
	where T : ScriptableObject
{
	#region IFactory

	public bool TryCreate(out object instance)
	{
		var operationHandle = Addressables.LoadAssetAsync<T>(Path);
		var config = operationHandle.WaitForCompletion();
		instance = config;

		if (config == null)
		{
			return false;
		}

		Addressables.Release(config);

		return true;
	}

	#endregion

	#region AddressableAssetFactory

	protected abstract string Path
	{
		get;
	}

	#endregion
}

}