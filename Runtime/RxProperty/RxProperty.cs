namespace EM.Foundation
{

using System;

public sealed class RxProperty<T> : IRxProperty<T>
{
	private T _value;

	#region IRxProperty

	public event Action<T> OnChanged;

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

	#endregion

	#region RxProperty

	public void SetValueWithoutNotify(T value)
	{
		_value = value;
	}

	#endregion
}

}