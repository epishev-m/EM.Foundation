namespace EM.Foundation
{
using System;

public class SignalEx :
	Signal
{
	#region SignalEx

	public bool Dispatch()
	{
		return Dispatch(null);
	}

	public void AddListener(
		Action action)
	{
		Requires.NotNull(action, nameof(action));

		AddListener(Action);

		void Action(ISignal target, object[] args) =>
			action?.Invoke();
	}

	public void AddListenerOnce(
		Action action)
	{
		Requires.NotNull(action, nameof(action));

		AddListenerOnce(Action);

		void Action(ISignal target, object[] args) =>
			action?.Invoke();
	}

	#endregion
}

}