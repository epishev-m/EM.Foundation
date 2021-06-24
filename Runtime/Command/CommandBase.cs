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

	public bool IsDone
	{
		get;
		private set;
	}

	public bool IsFailed
	{
		get;
		private set;
	}

	public abstract void Execute();

	public virtual void Clear()
	{
		Done = null;
		Data = null;
	}

	#endregion
	#region CommandBase

	protected void DoneInvoke()
	{
		IsDone = true;
		Done?.Invoke();
	}

	protected void Fail()
	{
		IsFailed = true;
	}

	#endregion
}

}
