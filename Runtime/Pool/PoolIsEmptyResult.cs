namespace EM.Foundation
{

public sealed class PoolIsEmptyResult<T> : NotFoundResult<T>
	where T : class
{
	public PoolIsEmptyResult()
		: base(default, default)
	{
	}
}

}