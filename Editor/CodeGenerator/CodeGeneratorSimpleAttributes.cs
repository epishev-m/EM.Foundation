namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using System.Text;

public sealed class CodeGeneratorSimpleAttributes : ICodeGenerator
{
	private const string Template = "\n[{0}]";

	private readonly IEnumerable<string> _attributs;

	private readonly ICodeGenerator _codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = _codeGenerator?.Create() ?? string.Empty;
		var builder = new StringBuilder();

		foreach (var attribute in _attributs)
		{
			builder.AppendFormat(Template, attribute);
		}

		builder.Append(code);

		return builder.ToString();
	}

	#endregion

	#region CodeGeneratorSimpleAttributes

	public CodeGeneratorSimpleAttributes(IEnumerable<string> attributs,
		ICodeGenerator codeGenerator)
	{
		_attributs = attributs;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}