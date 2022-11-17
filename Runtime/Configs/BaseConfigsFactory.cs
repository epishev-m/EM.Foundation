namespace EM.Foundation
{

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class BaseConfigsFactory : IFactory
{
	private readonly IAssetsManager _assetsManager;

	#region IFactory

	public Result<object> Create()
	{
		var loadAssetResult = _assetsManager.LoadAsset<TextAsset>(Key);

		if (loadAssetResult.Failure)
		{
			return new ErrorResult<object>(ConfigsFactoryStringResources.FailedToLoad(this));
		}

		var textAsset = loadAssetResult.Data;
		var bytes = textAsset.bytes;
		using var memoryStream = new MemoryStream();
		memoryStream.Write(bytes, 0, bytes.Length);
		memoryStream.Seek(0, SeekOrigin.Begin);
		Result<object> result;

		try
		{
			var formatter = new BinaryFormatter();
			var instance = formatter.Deserialize(memoryStream);
			result = new SuccessResult<object>(instance);
		}
		catch (SerializationException)
		{
			result = new ErrorResult<object>(ConfigsFactoryStringResources.ErrorDeserialization(this));
		}
		finally
		{
			_assetsManager.ReleaseAsset(textAsset);
		}

		return result;
	}

	#endregion

	#region BaseConfigsFactory

	protected BaseConfigsFactory(IAssetsManager assetsManager)
	{
		Requires.NotNull(assetsManager, nameof(assetsManager));

		_assetsManager = assetsManager;
	}

	protected abstract string Key
	{
		get;
	}

	#endregion
}

}