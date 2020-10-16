
namespace EM.Foundation
{
	public sealed class InstanceProvider :
		IInstanceProvider
	{
		#region IInstanceProvider

		public object GetInstance()
		{
			return instance;
		}

		#endregion
		#region InstanceProvider

		private readonly object instance;

		public InstanceProvider(
			object instance)
		{
			Requires.IsNotNull(instance, nameof(instance));

			this.instance = instance;
		}

		#endregion
	}
}
