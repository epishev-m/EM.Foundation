namespace EM.Foundation
{

public class ErrorResult : Result,
	IErrorResult
{
	#region IErrorResult

	public string Message
	{
		get;
	}

	#endregion

	#region ErrorResult

	public ErrorResult(string message)
	{
		Message = message;
		Success = false;
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

	#endregion

	#region ErrorResult

	public ErrorResult(string message)
		: base(default)
	{
		Message = message;
		Success = false;
	}

	#endregion
}

}