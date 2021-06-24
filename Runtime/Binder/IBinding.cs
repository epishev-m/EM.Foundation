namespace EM.Foundation
{
using System.Collections.Generic;

public interface IBinding
{
	object Key
	{
		get;
	}

	object Name
	{
		get;
	}

	IEnumerable<object> Values
	{
		get;
	}

	IBinding To<T>();

	IBinding To(
		object value);

	IBinding ToSelf();

	IBinding ToName<T>();

	IBinding ToName(
		object name);

	bool RemoveValue(
		object value);

	void RemoveAllValues();
}

}
