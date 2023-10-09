namespace EM.Foundation
{

using UnityEngine;

public abstract class FactoryAsset : ScriptableObject,
	IFactory
{
	#region IFactory

	public abstract Result<object> Create();

	#endregion IFactory
}

}