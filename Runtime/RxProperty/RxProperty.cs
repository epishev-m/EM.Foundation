namespace EM.Foundation
{

using System;

public sealed class RxProperty<T> : IRxProperty<T>
{
	private T _value;

	public event Action<T> OnChanged;

	#region RxProperty

	public T Value
	{
		get => _value;
		set
		{
			if (Equals(value, _value))
			{
				return;
			}

			_value = value;
			OnChanged?.Invoke(_value);
		}
	}

	public void Subscribe(Action<T> handler)
	{
		Requires.NotNullParam(handler, nameof(handler));

		OnChanged += handler;
		handler?.Invoke(_value);
	}

	public void UnSubscribe(Action<T> handler)
	{
		Requires.NotNullParam(handler, nameof(handler));

		OnChanged -= handler;
	}

	public void UnSubscribeAll()
	{
		OnChanged = null;
	}

	#endregion
}

}