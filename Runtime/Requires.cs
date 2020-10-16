
namespace EM.Foundation
{
	using System;
	using System.Runtime.CompilerServices;

	public static class Requires
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IsNotNull(object instance, string paramName)
		{
			if (instance is null)
			{
				ThrowArgumentNullException(paramName);
			}
		}

		private static void ThrowArgumentNullException(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}
	}
}
