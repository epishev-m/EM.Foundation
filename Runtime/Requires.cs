
namespace EM.Foundation
{
	using System;
	using System.Reflection;
	using System.Runtime.CompilerServices;

	public static class Requires
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IsNotNull(
			object instance,
			string paramName)
		{
			if (instance is null)
			{
				ThrowArgumentNullException(paramName);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IsNull(
			object instance,
			string paramName)
		{
			if ((instance is null) == false)
			{
				ThrowArgumentException(paramName);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IsReferenceType(
			Type type,
			string paramName)
		{
			var typeInfo = type.GetTypeInfo();

			if (!typeInfo.IsClass && !typeInfo.IsInterface)
			{
				ThrowArgumentException(paramName,
					StringResources.SuppliedTypeIsNotAReferenceType(type));
			}
		}

		private static void ThrowArgumentNullException(
			string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		private static void ThrowArgumentNullException(
			string paramName,
			string message)
		{
			throw new ArgumentNullException(paramName, message);
		}

		private static void ThrowArgumentException(
			string paramName)
		{
			throw new ArgumentException(paramName);
		}

		private static void ThrowArgumentException(
			string paramName,
			string message)
		{
			throw new ArgumentException(paramName, message);
		}
	}
}
