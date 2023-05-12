namespace EM.Foundation
{

using System;
using System.Collections.Generic;

public sealed class ObservableField<T> : IObservableField<T>
{
	private T _value;

	#region IObservableField

	public event Action<T> OnChanged;

	public T Value => _value;

	#endregion

	#region ObservableField

	public void SetValue(T value)
	{
		if (EqualityComparer<T>.Default.Equals(_value, value))
		{
			return;
		}

		_value = value;
		OnChanged?.Invoke(_value);
	}

	public void SetValueWithoutNotify(T value)
	{
		_value = value;
	}

	#endregion
}

}