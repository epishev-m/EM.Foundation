namespace EM.Foundation
{

using System.Globalization;

public static class PoolStringResources
{
	internal static string NoObjectsAvailable()
	{
		return "[Pool] No objects available";
	}

	internal static string IsEmptyAndInstanceProviderReturnedNull(string memberName)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[{0}] The object pool is empty. The instance provider returned null.",
			memberName);
	}
}

}