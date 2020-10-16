
namespace EM.Foundation
{
	using System;
	using ActionSignal = System.Action<ISignal, object[]>;
	
	public class Signal :
		ISignal
	{
		#region ISignal

		public void Dispatch(
			object[] args)
		{
			events?.Invoke(this, args);
		}

		public void AddListener(
			ActionSignal action)
		{
			Requires.IsNotNull(action, nameof(action));

			events = (ActionSignal)Delegate.Combine(events, action);
		}

		public void AddListenerOnce(
			ActionSignal action)
		{
			Requires.IsNotNull(action, nameof(action));

			ActionSignal removeAction = null;
			removeAction += RemoveAction;

			events = (ActionSignal)Delegate.Combine(events, removeAction, action);

			void RemoveAction(ISignal target, object[] args)
			{
				events = (ActionSignal)Delegate.Remove(events, action);
				events = (ActionSignal)Delegate.Remove(events, removeAction);
			}
		}

		public void RemoveAllListeners()
		{
			events = null;
		}

		public void RemoveListener(
			ActionSignal action)
		{
			Requires.IsNotNull(action, nameof(action));

			events = (ActionSignal)Delegate.Remove(events, action);
		}

		#endregion
		#region SignalBase

		protected event ActionSignal events;

		#endregion
	}
}
