namespace EM.Foundation
{

public class NotFoundErrorResult<T> : ErrorResult<T>
{
	public NotFoundErrorResult(string message)
		: base(message)
	{
	}
}

}