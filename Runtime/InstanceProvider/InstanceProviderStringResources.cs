namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class InstanceProviderStringResources
{
	internal static string FailedToGetFactory(IInstanceProvider instanceProvider,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Failed to get factory instance. \n {0}.{1}:{2}",
			instanceProvider.GetType(),
			memberName,
			lineNumber);
	}

	internal static string FailedToCreateInstance(IInstanceProvider instanceProvider,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Failed to create instance via factory. \n {0}.{1}:{2}",
			instanceProvider.GetType(),
			memberName,
			lineNumber);
	}
}

}