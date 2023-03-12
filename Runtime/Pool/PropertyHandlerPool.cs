namespace EM.Foundation
{

using System;

public sealed class PropertyHandlerPool
{
	private readonly Pool<PropertyHandler> _pool = new();

	#region MyRegion

	public PropertyHandler Get(IEventProvider eventProvider,
		Action handler)
	{
		Requires.NotNullParam(eventProvider, nameof(eventProvider));
		Requires.NotNullParam(handler, nameof(handler));

		var result = _pool.GetObject();

		if (!result.Failure)
		{
			return result.Data;
		}

		var propertyHandler = new PropertyHandler();
		propertyHandler.Initialize(eventProvider, handler);

		return propertyHandler;

	}

	public void Put(PropertyHandler propertyHandler)
	{
		Requires.NullParam(propertyHandler, nameof(propertyHandler));

		_pool.PutObject(propertyHandler);
	}

	#endregion

}

}