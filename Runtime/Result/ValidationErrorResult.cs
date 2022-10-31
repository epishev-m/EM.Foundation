namespace EM.Foundation
{

using System.Collections.Generic;

public sealed class ValidationErrorResult : ErrorResult
{
	public ValidationErrorResult(string message,
		IReadOnlyCollection<ValidationError> errors)
		: base(message, errors)
	{
	}
}

}