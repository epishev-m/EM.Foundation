namespace EM.Foundation
{

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class BaseConfigsFactory : IFactory
{
	#region IFactory

	public bool TryCreate(out object instance)
	{
		var operationHandle = Addressables.LoadAssetAsync<TextAsset>(Path);
		var textAsset = operationHandle.WaitForCompletion();
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

			return false;
		}
		finally
		{
			Addressables.Release(textAsset);
		}

		return true;
	}

	#endregion

	#region BaseConfigsFactory

	protected abstract string Path
	{
		get;
	}

	#endregion
}

}