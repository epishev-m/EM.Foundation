namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using System.Text;

public sealed class CodeGeneratorSimpleAttributes :
	ICodeGenerator
{
	private const string template = "\n[{0}]";

	private readonly IEnumerable<string> attributs;

	private readonly ICodeGenerator codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = codeGenerator?.Create() ?? string.Empty;
		var builder = new StringBuilder();

		foreach (var attribut in attributs)
		{
			builder.AppendFormat(template, attribut);
		}

		builder.Append(code);

		return builder.ToString();
	}

	#endregion

	#region CodeGeneratorSimpleAttributes

	public CodeGeneratorSimpleAttributes(IEnumerable<string> attributs,
		ICodeGenerator codeGenerator)
	{
		this.attributs = attributs;
		this.codeGenerator = codeGenerator;
	}

	#endregion
}

}