namespace EM.Foundation
{

using System.Collections.Generic;

public interface IErrorResult
{
	string Message
	{
		get;
	}

	IReadOnlyCollection<Error> Errors
	{
		get;
	}
}

}