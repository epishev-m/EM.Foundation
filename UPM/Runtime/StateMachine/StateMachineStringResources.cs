namespace EM.Foundation
{

using System.Globalization;
using System.Runtime.CompilerServices;

internal static class StateMachineStringResources
{
	internal static string PassedTypeIsNull(object obj,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Game state type passed is null. \n {0}.{1}:{2}",
			obj.GetType(),
			memberName,
			lineNumber);
	}

	internal static string CannotFindState(object obj,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Can't find specified game state. \n {0}.{1}:{2}",
			obj.GetType(),
			memberName,
			lineNumber);
	}

	internal static string StateIsAlreadyActive(object obj,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int lineNumber = 0)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"[Error] Specified state is already active. \n {0}.{1}:{2}",
			obj.GetType(),
			memberName,
			lineNumber);
	}

	internal static string StateInstanceOf(string state,
		string type)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"State {0} must be instance of IPayloadedState<{1}>!",
			state,
			type);
	}

	internal static string CannotFindOnEnterMethod(string state)
	{
		return string.Format(CultureInfo.InvariantCulture,
			"State {0}: cannot find OnEnter method!",
			state);
	}
}

}