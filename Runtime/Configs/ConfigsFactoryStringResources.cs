namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class ConfigsFactoryStringResources
{
	internal static string ErrorDeserialization(BaseConfigsFactory factory,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Failed to deserialize config file. \n {0}.{1}:{2}",
			factory.GetType(), memberName, lineNumber);
	}
	
	internal static string FailedToLoad(BaseConfigsFactory factory,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Failed to load text asset. \n {0}.{1}:{2}",
			factory.GetType(), memberName, lineNumber);
	}
}

}