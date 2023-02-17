namespace EM.Foundation
{

using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DocumentationAttribute : Attribute
{
	public readonly string CommonDescription;

	public readonly string Group;

	public readonly string GroupDescription;

	public DocumentationAttribute(string commonDescription)
		: this(commonDescription, "", null)
	{
	}

	public DocumentationAttribute(string group,
		string groupDescription)
		: this("", group, groupDescription)
	{
	}

	private DocumentationAttribute(string commonDescription,
		string group,
		string groupDescription)
	{
		CommonDescription = commonDescription;
		Group = group;
		GroupDescription = groupDescription;
	}
}

}