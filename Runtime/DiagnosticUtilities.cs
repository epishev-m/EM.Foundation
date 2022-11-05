namespace EM.Foundation
{

using System.Runtime.CompilerServices;

public static class DiagnosticUtilities
{
	public static string GetMemberNameAndLineNumber(object instance,
		[CallerMemberName] string memberName = "",
		[CallerLineNumber] int sourceLineNumber = 0)
	{
		return $"{instance.GetType()}.{memberName}:{sourceLineNumber}";
	}
}

}