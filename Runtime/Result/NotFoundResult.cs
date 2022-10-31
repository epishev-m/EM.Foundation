namespace EM.Foundation
{

using System.Collections.Generic;

public sealed class NotFoundResult<T> : ErrorResult<T>
{
	public NotFoundResult(string message,
		IReadOnlyCollection<Error> errors)
		: base(message, errors)
	{
	}
}

}