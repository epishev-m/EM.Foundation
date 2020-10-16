
namespace EM.Foundation
{
	using System;
	
	public abstract class CommandBase :
		ICommand
	{
		#region ICommand

		public event Action Done;

		public object Data
		{
			set;
			protected get;
		}

		public bool IsDone => isDone;

		public bool IsFailed => isFailed;

		public abstract void Execute();

		public virtual void Clear()
		{
			Done = null;
			Data = null;
		}

		#endregion
		#region CommandBase

		private bool isDone = false;

		private bool isFailed = false;

		protected void DoneInvoke()
		{
			isDone = true;
			Done?.Invoke();
		}

		protected void Fail()
		{
			isFailed = true;
		}

		#endregion
	}
}
