namespace EM.Foundation
{

public abstract class Result
{
	public bool Success
	{
		get;
		protected set;
	}

	public bool Failure => !Success;
}

public abstract class Result<T> : Result
{
	private T _data;

	#region Result

	protected Result(T data)
	{
		Data = data;
	}

	public T Data
	{
		get
		{
			Requires.ValidOperation(Success, this, nameof(Data));

			return _data;
		}
		set => _data = value;
	}

	#endregion
}

}