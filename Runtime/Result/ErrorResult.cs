namespace EM.Foundation
{

using System;
using System.Collections.Generic;

public class ErrorResult : Result,
	IErrorResult
{
	#region IErrorResult

	public string Message
	{
		get;
	}

	public IReadOnlyCollection<Error> Errors
	{
		get;
	}

	#endregion

	#region ErrorResult

	public ErrorResult(string message,
		IReadOnlyCollection<Error> errors)
	{
		Message = message;
		Success = false;
		Errors = errors ?? Array.Empty<Error>();
	}

	#endregion
}

public class ErrorResult<T> : Result<T>,
	IErrorResult
{
	#region IErrorResult

	public string Message
	{
		get;
	}

	public IReadOnlyCollection<Error> Errors
	{
		get;
	}

	#endregion

	#region ErrorResult

	public ErrorResult(string message,
		IReadOnlyCollection<Error> errors)
		: base(default)
	{
		Message = message;
		Success = false;
		Errors = errors ?? Array.Empty<Error>();
	}

	#endregion
}

}