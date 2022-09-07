namespace EM.Foundation.Editor
{

public class CodeGeneratorSimpleClass : ICodeGenerator
{
	private const string Template = "\npublic sealed class {0}\n{{{1}}}\n";

	private readonly string _name;

	private readonly ICodeGenerator _codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		var code = _codeGenerator?.Create() ?? string.Empty;
		code = string.Format(Template, _name, code);

		return code;
	}

	#endregion

	#region ClassTemplate

	public CodeGeneratorSimpleClass(string name,
		ICodeGenerator codeGenerator)
	{
		_name = name;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}