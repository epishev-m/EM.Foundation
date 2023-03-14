namespace EM.Foundation
{

using System;

public sealed class EventProviderHandlerPool
{
	private readonly Pool<EventProviderHandler> _pool = new();

	#region EventProviderHandlerPool

	public EventProviderHandler Get(IEventProvider eventProvider,
		Action handler)
	{
		Requires.NotNullParam(eventProvider, nameof(eventProvider));

		var result = _pool.GetObject();

		var propertyHandler = result.Failure 
			? new EventProviderHandler() 
			: result.Data;

		propertyHandler.Initialize(eventProvider, handler);

		return propertyHandler;
	}

	public void Put(EventProviderHandler eventProviderHandler)
	{
		Requires.NotNullParam(eventProviderHandler, nameof(eventProviderHandler));

		_pool.PutObject(eventProviderHandler);
	}

	#endregion

}

}