using System;

namespace EM.Foundation
{
	public abstract class BaseCompositeCommand : BaseCommand, ICompositeCommand
	{
		#region ICompositeCommand

		public ICompositeCommand Add(ICommand command)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		#endregion
		#region BaseCompositeCommand

		#endregion
	}
}
