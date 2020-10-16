
namespace EM.Foundation
{
	using UnityEngine;
	
	public abstract class FactoryAsset :
		ScriptableObject,
		IFactory
	{
		#region IFactory

		public abstract bool TryCreate(out object instance);

		#endregion IFactory
	}
}