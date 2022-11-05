namespace EM.Foundation
{

using System.Collections.Generic;

public class NotFoundResult<T> : ErrorResult<T>
{
	public NotFoundResult(string message,
		IReadOnlyCollection<Error> errors)
		: base(message, errors)
	{
	}
}

}