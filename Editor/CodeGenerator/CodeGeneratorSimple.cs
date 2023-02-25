namespace EM.Foundation.Editor
{

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public sealed class CodeGeneratorSimple : ICodeGenerator
{
	private readonly string _name;

	private readonly string _path;

	private readonly ICodeGenerator _codeGenerator;

	#region ICodeGenerator

	public string Create()
	{
		if (!Directory.Exists(_path))
		{
			Directory.CreateDirectory(_path);
		}

		var code = _codeGenerator?.Create() ?? string.Empty;

		try
		{
			using var stream = File.Open(_path + "/" + _name, FileMode.Create, FileAccess.Write);
			using var writer = new StreamWriter(stream);
			writer.Write(code);
		}
		catch (Exception e)
		{
			Debug.LogException(e);

			throw;
		}

		return code;
	}

	#endregion

	#region CodeGeneratorSimple

	public CodeGeneratorSimple(string name,
		string path,
		ICodeGenerator codeGenerator)
	{
		_name = name;
		_path = path;
		_codeGenerator = codeGenerator;
	}

	#endregion
}

}