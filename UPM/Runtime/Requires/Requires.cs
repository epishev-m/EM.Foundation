namespace EM.Foundation
{

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

public static partial class Requires
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidOperation(bool condition,
		object instance,
		[CallerMemberName] string memberName = "")
	{
		if (condition == false)
		{
			throw new InvalidOperationException(
				RequiresStringResources.MethodCallInvalidForObjectCurrentState(instance.GetType(), memberName));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidOperation(bool condition,
		string message)
	{
		if (condition == false)
		{
			throw new InvalidOperationException(message);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ValidArgument(bool condition,
		string message)
	{
		if (condition == false)
		{
			throw new ArgumentException(message);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotNullParam(object instance,
		string paramName)
	{
		if (instance == null)
		{
			throw new ArgumentNullException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NullParam(object instance,
		string paramName)
	{
		if (instance != null)
		{
			throw new ArgumentException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotNull(object instance,
		string objectName)
	{
		if (instance == null)
		{
			throw new NullReferenceException(RequiresStringResources.SuppliedObjectCannotBeNull(objectName));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Null(object instance,
		string paramName)
	{
		if (instance != null)
		{
			throw new ArgumentException(paramName);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReferenceType(Type type,
		string paramName)
	{
		var typeInfo = type.GetTypeInfo();

		if (!typeInfo.IsClass && !typeInfo.IsInterface)
		{
			throw new ArgumentException(paramName, RequiresStringResources.SuppliedTypeIsNotAReferenceType(type));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Type<T>(object instance,
		string paramName)
	{
		if (instance is T == false)
		{
			throw new ArgumentException(paramName,
				RequiresStringResources.SuppliedTypeIsNotAGivenType(instance.GetType(), typeof(T)));
		}
	}
}

}