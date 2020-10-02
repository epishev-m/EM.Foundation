using System;
using ActionSignal = System.Action<EM.Foundation.ISignal, object[]>;

namespace EM.Foundation
{
	public class Signal : ISignal
	{
		#region ISignal

		public void Dispatch(object[] args)
		{
			_events?.Invoke(this, args);
		}

		public void AddListener(ActionSignal action)
		{
			_events = (ActionSignal)Delegate.Combine(_events, action);
		}

		public void AddListenerOnce(ActionSignal action)
		{
			ActionSignal removeAction = null;
			removeAction += RemoveAction;

			_events = (ActionSignal)Delegate.Combine(_events, removeAction, action);

			void RemoveAction(ISignal target, object[] args)
			{
				_events = (ActionSignal)Delegate.Remove(_events, action);
				_events = (ActionSignal)Delegate.Remove(_events, removeAction);
			}
		}

		public void RemoveAllListeners()
		{
			_events = null;
		}

		public void RemoveListener(ActionSignal action)
		{
			_events = (ActionSignal)Delegate.Remove(_events, action);
		}

		#endregion
		#region SignalBase

		protected event ActionSignal _events;

		#endregion
	}
}
