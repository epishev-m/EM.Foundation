namespace EM.Foundation
{

using System;

public sealed class EventProviderHandler : IDisposable
{
	private IEventProvider _eventProvider;

	private Action _handler;

	#region IDisposable

	public void Dispose()
	{
		if (_eventProvider is IDisposable disposable)
		{
			disposable.Dispose();
		}

		_eventProvider.OnChanged -= InvokeHandler;
		_eventProvider = null;
		_handler = null;
	}

	#endregion

	#region PropertyHandler

	public void Initialize(IEventProvider eventProvider,
		Action handler)
	{
		Requires.NotNullParam(eventProvider, nameof(eventProvider));

		_eventProvider = eventProvider;
		_handler = handler;
		_eventProvider.OnChanged += InvokeHandler;
	}

	private void InvokeHandler()
	{
		_handler?.Invoke();
	}

	#endregion
}

}