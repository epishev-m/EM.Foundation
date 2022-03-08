namespace EM.Foundation
{
using System;

public sealed class ReactiveProperty<T>
{
	private T value;

	private event Action OnChanged;

	#region ReactiveProperty

	public T Value
	{
		get => value;
		set
		{
			if (Equals(value, this.value))
			{
				return;
			}

			this.value = value;
			OnChanged?.Invoke();
		}
	}

	public void Subscribe(Action handler)
	{
		Requires.NotNull(handler, nameof(handler));

		OnChanged += handler;
	}

	public void UnSubscribe(Action handler)
	{
		Requires.NotNull(handler, nameof(handler));

		OnChanged -= handler;
	}
	
	#endregion
}

}