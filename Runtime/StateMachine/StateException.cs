namespace EM.Foundation
{

using System;

public sealed class StateException : Exception
{
	public StateException(string message) :
		base(message)
	{
	}
}

}