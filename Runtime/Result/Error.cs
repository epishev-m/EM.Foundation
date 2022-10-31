namespace EM.Foundation
{

public class Error
{
	#region Error

	public Error(string code,
		string details)
	{
		Code = code;
		Details = details;
	}

	public string Code
	{
		get;
	}

	public string Details
	{
		get;
	}

	#endregion
}

}