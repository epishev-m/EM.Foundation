namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class ReflectorStringResources
{
	internal static string MultipleConstructors(IReflectionInfo info,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] The specified type has multiple constructors. \n {0}.{1}:{2}",
			info.GetType(),
			memberName,
			lineNumber);
	}

	internal static string TypeNull(IReflector info,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Specified type is null. \n {0}.{1}:{2}",
			info.GetType(),
			memberName,
			lineNumber);
	}
}

}