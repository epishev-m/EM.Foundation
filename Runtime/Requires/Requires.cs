namespace EM.Foundation
{

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

public static class Requires
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidOperation(bool condition,
		object instance,
		[CallerMemberName] string memberName = "")
	{
		if (condition == false)
		{
			ThrowInvalidOperationException(
				RequiresStringResources.MethodCallInvalidForObjectCurrentState(instance.GetType(), memberName));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidOperation(bool condition,
		string message)
	{
		if (condition == false)
		{
			ThrowInvalidOperationException(message);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidArgument(bool condition,
		string message)
	{
		if (condition == false)
		{
			ThrowArgumentException(message);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotNull(object instance,
		string paramName)
	{
		if (instance == null)
		{
			ThrowArgumentNullException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotNull(UnityEngine.Object instance,
		string paramName)
	{
		if (instance == null)
		{
			ThrowArgumentNullException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Null(object instance,
		string paramName)
	{
		if (instance != null)
		{
			ThrowArgumentException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Null(UnityEngine.Object instance,
		string paramName)
	{
		if (instance != null)
		{
			ThrowArgumentException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReferenceType(Type type,
		string paramName)
	{
		var typeInfo = type.GetTypeInfo();

		if (!typeInfo.IsClass && !typeInfo.IsInterface)
		{
			ThrowArgumentException(paramName,
				RequiresStringResources.SuppliedTypeIsNotAReferenceType(type));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Type<T>(object instance,
		string paramName)
	{
		if (instance is T == false)
		{
			ThrowArgumentException(paramName,
				RequiresStringResources.SuppliedTypeIsNotAGivenType(instance.GetType(), typeof(T)));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Type<T>(UnityEngine.Object instance,
		string paramName)
	{
		if (instance is T == false)
		{
			ThrowArgumentException(paramName,
				RequiresStringResources.SuppliedTypeIsNotAGivenType(instance.GetType(), typeof(T)));
		}
	}

	private static void ThrowArgumentNullException(string paramName)
	{
		throw new ArgumentNullException(paramName);
	}

	private static void ThrowArgumentException(string paramName)
	{
		throw new ArgumentException(paramName);
	}

	private static void ThrowArgumentException(string paramName,
		string message)
	{
		throw new ArgumentException(paramName, message);
	}

	private static void ThrowInvalidOperationException(string message)
	{
		throw new InvalidOperationException(message);
	}
}

}