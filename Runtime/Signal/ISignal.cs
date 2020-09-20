using System;

namespace EM.Foundation
{
	public interface ISignal
	{
		void Dispatch(object[] args);

		void AddListener(Action<ISignal, object[]> action);

		void AddListenerOnce(Action<ISignal, object[]> action);

		void RemoveListener(Action<ISignal, object[]> action);

		void RemoveAllListeners();
	}
}