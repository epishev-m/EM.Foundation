using System;
using ActionEx = System.Action<EM.Foundation.ISignal, object[]>;

namespace EM.Foundation
{
	public class Signal : ISignal
	{
		#region ISignal

		public void Dispatch(object[] args)
		{
			_events?.Invoke(this, args);
		}

		public void AddListener(ActionEx action)
		{
			_events = (ActionEx)Delegate.Combine(_events, action);
		}

		public void AddListenerOnce(ActionEx action)
		{
			ActionEx removeAction = null;
			removeAction += RemoveAction;

			_events = (ActionEx)Delegate.Combine(_events, removeAction, action);

			void RemoveAction(ISignal target, object[] args)
			{
				_events = (ActionEx)Delegate.Remove(_events, action);
				_events = (ActionEx)Delegate.Remove(_events, removeAction);
			}
		}

		public void RemoveAllListeners()
		{
			_events = null;
		}

		public void RemoveListener(ActionEx action)
		{
			_events = (ActionEx)Delegate.Remove(_events, action);
		}

		#endregion
		#region SignalBase

		protected event ActionEx _events;

		#endregion
	}
}
