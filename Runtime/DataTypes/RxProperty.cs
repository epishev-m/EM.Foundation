namespace EM.Foundation
{

using System;

public sealed class RxProperty<T>
{
	private T _value;

	private event Action OnChanged;

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
			OnChanged?.Invoke();
		}
	}

	public void Subscribe(Action handler)
	{
		Requires.NotNull(handler, nameof(handler));

		OnChanged += handler;
		OnChanged?.Invoke();
	}

	public void UnSubscribe(Action handler)
	{
		Requires.NotNull(handler, nameof(handler));

		OnChanged -= handler;
	}

	public void UnSubscribeAll()
	{
		OnChanged = null;
	}

	#endregion
}

}