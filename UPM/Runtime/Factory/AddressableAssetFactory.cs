namespace EM.Foundation
{

using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class AddressableAssetFactory<T> : IFactory
	where T : ScriptableObject
{
	#region IFactory

	public Result<object> Create()
	{
		var operationHandle = Addressables.LoadAssetAsync<T>(Path);
		var config = operationHandle.WaitForCompletion();

		if (config == null)
		{
			return new ErrorResult<object>(FactoryStringResources.AddressablesLoadAsset(this));
		}

		Addressables.Release(config);

		return new SuccessResult<object>(config);
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