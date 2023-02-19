namespace EM.Foundation.Editor
{

public sealed class CodeGeneratorSimpleNamespace : ICodeGenerator
{
	private const string Template = "namespace {0}\n{{\n{1}}}\n";

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

	#region CodeGeneratorSimpleNamespace

	public CodeGeneratorSimpleNamespace(string name,
		ICodeGenerator codeGenerator)
	{
		_name = name;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}