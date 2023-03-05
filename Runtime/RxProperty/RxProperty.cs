namespace EM.Foundation
{

using System;

public sealed class RxProperty<T> : IRxProperty<T>
{
	private T _value;

	#region RxProperty

	public event Action OnChanged;

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

	#endregion
}

}