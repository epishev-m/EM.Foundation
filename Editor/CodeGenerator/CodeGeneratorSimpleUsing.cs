namespace EM.Foundation.Editor
{
using System.Collections.Generic;
using System.Text;

public sealed class CodeGeneratorSimpleUsing :
	ICodeGenerator
{
	private const string template = "\nusing {0};";

	private readonly IEnumerable<string> names;

	private readonly ICodeGenerator codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = codeGenerator?.Create() ?? string.Empty;
		var builder = new StringBuilder();

		foreach (var name in names)
		{
			builder.AppendFormat(template, name);
		}

		builder.AppendLine();
		builder.AppendLine(code);

		return builder.ToString();
	}

	#endregion

	#region CodeGeneratorSimpleUsing

	public CodeGeneratorSimpleUsing(
		IEnumerable<string> names,
		ICodeGenerator codeGenerator)
	{
		this.names = names;
		this.codeGenerator = codeGenerator;
	}

	#endregion
}

}