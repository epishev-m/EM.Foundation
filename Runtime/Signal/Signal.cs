using System;

namespace EM.Foundation
{
	public class Signal : ISignal
	{
		#region ISignal

		public void Dispatch(object[] args)
		{
			_events?.Invoke(this, args);
		}

		public void AddListener(Action<ISignal, object[]> action)
		{
			var unused = action ?? throw new ArgumentNullException(nameof(action));
			_events += action;
		}

		public void AddListenerOnce(Action<ISignal, object[]> action)
		{
			var unused = action ?? throw new ArgumentNullException(nameof(action));
			_events += removeAction;
			_events += action;

			void removeAction(ISignal target, object[] args)
			{
				_events -= removeAction;
				_events -= action;
			}
		}

		public void RemoveAllListeners()
		{
			_events = null;
		}

		public void RemoveListener(Action<ISignal, object[]> action)
		{
			_events -= action;
		}

		#endregion
		#region SignalBase

		protected event Action<ISignal, object[]> _events;

		#endregion
	}
}
