
namespace EM.Foundation
{
	using System;
	using System.Reflection;
	using System.Runtime.CompilerServices;

	public static class Requires
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IsValidOperation(
			bool condition,
			object instance,
			string methodName)
		{
			if (condition == false)
			{
				ThrowInvalidOperationException(
					StringResources.MethodCallInvalidForObjectCurrentState(
						instance.GetType(), methodName));
			}
		}

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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void IsType<T>(
			object instance,
			string paramName)
		{
			if ((instance is T) == false)
			{
				ThrowArgumentException(paramName,
					StringResources.SuppliedTypeIsNotAGivenType(instance.GetType(), typeof(T)));
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

		private static void ThrowInvalidOperationException(
			string message)
		{
			throw new InvalidOperationException(message);
		}
	}
}
