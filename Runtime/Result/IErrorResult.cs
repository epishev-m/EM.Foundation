namespace EM.Foundation
{

using System.Collections.Generic;

internal interface IErrorResult
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