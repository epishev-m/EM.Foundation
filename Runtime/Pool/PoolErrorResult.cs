namespace EM.Foundation
{

using System.Collections.Generic;

public sealed class PoolErrorResult<T> : ErrorResult<T>
	where T : class
{
	public PoolErrorResult(string message,
		string errorPrefix)
		: base(message, GetErrors(errorPrefix))
	{
	}

	private static IReadOnlyCollection<Error> GetErrors(string errorPrefix)
	{
		var details = PoolStringResources.IsEmptyAndInstanceProviderReturnedNull(errorPrefix);
		var error = new Error(default, details);

		var resultErrorsList = new List<Error>()
		{
			error
		};

		return resultErrorsList.AsReadOnly();
	}
}

}