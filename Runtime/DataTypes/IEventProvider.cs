namespace EM.Foundation
{

using System;

public interface IEventProvider
{
	event Action OnChanged;
}

}