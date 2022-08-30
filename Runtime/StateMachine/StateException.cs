namespace EM.Foundation
{

using System;

public sealed class StateException :
	Exception
{
	public StateException()
	{
	}

	public StateException(string message) :
		base(message)
	{
	}

	public StateException(string message,
		Exception innerException) :
		base(message, innerException)
	{
	}
}

}