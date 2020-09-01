using System;

namespace EM.Foundation
{
	public abstract class BaseCommand : ICommand
	{
		#region ICommand

		public event Action Done;

		public object Data
		{
			set;
			protected get;
		}

		public bool IsFailed => _isFailed;

		public abstract void Execute();

		protected void Fail()
		{
			_isFailed = true;
		}

		#endregion
		#region BaseCommand

		private bool _isFailed = false;

		protected void DoneInvoke()
		{
			Done?.Invoke();
		}

		#endregion
	}
}
