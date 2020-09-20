using UnityEngine;

namespace EM.Foundation
{
	public abstract class FactoryAsset : ScriptableObject, IFactory
	{
		#region IFactory

		public abstract bool Create(out object instance);

		#endregion IFactory
	}
}