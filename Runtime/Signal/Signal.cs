namespace EM.Foundation
{
using System;
using ActionSignal = System.Action<ISignal, object[]>;

public class Signal :
	ISignal
{
	protected event ActionSignal Events;

	#region ISignal

	public bool Dispatch(object[] args)
	{
		var rsult = Events != null;
		Events?.Invoke(this, args);

		return rsult;
	}

	public void AddListener(ActionSignal action)
	{
		Requires.NotNull(action, nameof(action));

		Events = (ActionSignal)Delegate.Combine(Events, action);
	}

	public void AddListenerOnce(ActionSignal action)
	{
		Requires.NotNull(action, nameof(action));

		ActionSignal removeAction = null;
		removeAction += RemoveAction;

		Events = (ActionSignal)Delegate.Combine(Events, removeAction, action);

		void RemoveAction(ISignal target, object[] args)
		{
			Events = (ActionSignal)Delegate.Remove(Events, action);
			Events = (ActionSignal)Delegate.Remove(Events, removeAction);
		}
	}

	public void RemoveAllListeners()
	{
		Events = null;
	}

	public void RemoveListener(ActionSignal action)
	{
		Requires.NotNull(action, nameof(action));

		Events = (ActionSignal)Delegate.Remove(Events, action);
	}

	#endregion
}

}
