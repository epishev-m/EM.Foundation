namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using System.Text;

public sealed class CodeGeneratorSimpleUsing : ICodeGenerator
{
	private const string Template = "\nusing {0};";

	private readonly IEnumerable<string> _names;

	private readonly ICodeGenerator _codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = _codeGenerator?.Create() ?? string.Empty;
		var builder = new StringBuilder();

		foreach (var name in _names)
		{
			builder.AppendFormat(Template, name);
		}

		builder.AppendLine();
		builder.AppendLine(code);

		return builder.ToString();
	}

	#endregion

	#region CodeGeneratorSimpleUsing

	public CodeGeneratorSimpleUsing(IEnumerable<string> names,
		ICodeGenerator codeGenerator)
	{
		_names = names;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}