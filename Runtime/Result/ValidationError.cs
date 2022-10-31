namespace EM.Foundation
{

public sealed class ValidationError : Error
{
	public readonly string PropertyName;

	#region ValidationError

	public ValidationError(string propertyName,
		string details)
		: base(null, details)
	{
		PropertyName = propertyName;
	}

	#endregion
}

}