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

	public bool TryCreate(out object instance)
	{
		var textAsset =  _assetsManager.LoadAsset<TextAsset>(Key);
		var bytes = textAsset.bytes;
		using var memoryStream = new MemoryStream();
		memoryStream.Write(bytes, 0, bytes.Length);
		memoryStream.Seek(0, SeekOrigin.Begin);

		try
		{
			var formatter = new BinaryFormatter();
			instance = formatter.Deserialize(memoryStream);
		}
		catch (SerializationException e)
		{
			Debug.LogException(e);
			instance = null;
		}
		finally
		{
			_assetsManager.ReleaseAsset(textAsset);
		}

		return instance != null;
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