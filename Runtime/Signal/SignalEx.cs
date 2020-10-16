
namespace EM.Foundation
{
	using System;
	
	public sealed class SignalEx :
		Signal
	{
		public void Dispatch()
		{
			Dispatch(null);
		}

		public void AddListener(
			Action action)
		{
			Requires.IsNotNull(action, nameof(action));

			AddListener(Action);

			void Action(ISignal target, object[] args) => 
				action?.Invoke();
		}

		public void AddListenerOnce(
			Action action)
		{
			Requires.IsNotNull(action, nameof(action));

			AddListenerOnce(Action);

			void Action(ISignal target, object[] args) =>
				action?.Invoke();
		}
	}
}