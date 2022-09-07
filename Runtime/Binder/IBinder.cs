namespace EM.Foundation
{

using System;

public interface IBinder
{
	IBinding Bind<T>(object name = null);

	IBinding Bind(object key,
		object name = null);

	bool Unbind<T>(object name = null);

	bool Unbind(object key,
		object name = null);

	void Unbind(Predicate<IBinding> match);

	void UnbindAll();
}

}