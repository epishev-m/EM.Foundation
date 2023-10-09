namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class FactoryStringResources
{
	internal static string AddressablesLoadAsset(IFactory factory,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Failed loading of Addressables Asset. \n {0}.{1}:{2}",
			factory.GetType(), memberName, lineNumber);
	}
}

}