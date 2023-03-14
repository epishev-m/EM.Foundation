namespace EM.Foundation
{

using System;
using UnityEngine.Events;

public sealed class UnityEventProvider : IEventProvider,
	IDisposable
{
	private readonly UnityEvent _unityEvent;

	#region IDisposable

	public void Dispose()
	{
		_unityEvent.RemoveAllListeners();
	}

	#endregion

	#region IEventProvider

	public event Action OnChanged;

	#endregion

	#region UnityEventProvider

	public UnityEventProvider(UnityEvent unityEvent)
	{
		_unityEvent = unityEvent;
		_unityEvent.AddListener(() => OnChanged?.Invoke());
	}

	#endregion
}

public sealed class UnityEventProvider<T> : IEventProvider,
	IDisposable
{
	private readonly UnityEvent<T> _unityEvent;

	#region IDisposable

	public void Dispose()
	{
		_unityEvent.RemoveAllListeners();
	}

	#endregion

	#region IEventProvider

	public event Action OnChanged;

	#endregion

	#region UnityEventProvider

	public UnityEventProvider(UnityEvent<T> unityEvent)
	{
		_unityEvent = unityEvent;
		_unityEvent.AddListener(_ => OnChanged?.Invoke());
	}

	#endregion
}

}