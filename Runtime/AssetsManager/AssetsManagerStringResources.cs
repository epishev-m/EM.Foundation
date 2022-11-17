namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class AssetsManagerStringResources
{
	internal static string FailedLoaded(IAssetsManager assetsManager,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Failed to load asset. \n {0}.{1}:{2}",
			assetsManager.GetType(), memberName, lineNumber);
	}
	
	internal static string GameObjectNull(IAssetsManager assetsManager,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] GameObject is null. \n {0}.{1}:{2}",
			assetsManager.GetType(), memberName, lineNumber);
	}
}

}