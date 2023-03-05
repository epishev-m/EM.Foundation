namespace EM.Foundation
{

using System;

public sealed class PropertyHandler : IDisposable
{
	private IEventProvider _eventProvider;
	private Action _handler;

	#region IDisposable

	public void Dispose()
	{
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
		Requires.NotNullParam(handler, nameof(handler));

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