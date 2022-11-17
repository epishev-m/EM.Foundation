namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class PoolStringResources
{
	internal static string InstanceProviderReturnedNull<T>(IPool<T> pool,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
		where T : class
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] The object pool is empty. The instance provider returned null. \n {0}.{1}:{2}",
			pool.GetType(),
			memberName,
			lineNumber);
	}

	internal static string PutNullObject<T>(IPool<T> pool,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
		where T : class
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Attempt to put a null object to the pool. \n {0}.{1}:{2}",
			pool.GetType(),
			memberName,
			lineNumber);
	}

	internal static string PoolIsEmpty<T>(IPool<T> pool,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
		where T : class
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Object pool is empty. \n {0}.{1}:{2}",
			pool.GetType(),
			memberName,
			lineNumber);
	}
}

}