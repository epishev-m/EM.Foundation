using System.Text;

namespace EM.Foundation.Editor
{

public class CodeGeneratorSimpleComment :
	ICodeGenerator
{
	private const string template = "/* {0} */\n";

	private readonly string comment;
	
	private readonly ICodeGenerator codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var stringBuilder = new StringBuilder();
		var code = codeGenerator?.Create() ?? string.Empty;
		stringBuilder.AppendFormat(template, comment);
		stringBuilder.Append(code);
		code = stringBuilder.ToString();

		return code;
	}

	#endregion
	#region CodeGeneratorSimpleComment

	public CodeGeneratorSimpleComment(
		string comment,
		ICodeGenerator codeGenerator)
	{
		this.comment = comment;
		this.codeGenerator = codeGenerator;
	}

	#endregion
}

}