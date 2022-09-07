namespace EM.Foundation.Editor
{

using System.Text;

public class CodeGeneratorSimpleComment : ICodeGenerator
{
	private const string Template = "/* {0} */\n";

	private readonly string _comment;

	private readonly ICodeGenerator _codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var stringBuilder = new StringBuilder();
		var code = _codeGenerator?.Create() ?? string.Empty;
		stringBuilder.AppendFormat(Template, _comment);
		stringBuilder.Append(code);
		code = stringBuilder.ToString();

		return code;
	}

	#endregion

	#region CodeGeneratorSimpleComment

	public CodeGeneratorSimpleComment(string comment,
		ICodeGenerator codeGenerator)
	{
		_comment = comment;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}