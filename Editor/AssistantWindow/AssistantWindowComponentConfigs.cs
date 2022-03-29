namespace EM.Foundation.Editor
{

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Foundation;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public sealed class AssistantWindowComponentConfigs<T> :
	IAssistantWindowComponent
	where T : class, new()
{
	private string inputPath;

	private string outputPath;

	#region IAssistantWindowComponent

	public string Name => "Configs";

	public void Prepare()
	{
		inputPath = EditorPrefs.GetString(InputPathKey);
		outputPath = EditorPrefs.GetString(OutputPathKey);
	}

	public void OnGUI()
	{
		EditorGUILayout.LabelField("Input path:");
		TextField(InputPathKey, () => inputPath, value => inputPath = value);
		EditorGUILayout.LabelField("Output path:");
		TextField(OutputPathKey, () => outputPath, value => outputPath = value);
		EditorGUILayout.Space();

		if (GUILayout.Button("Configure"))
		{
			CreateConfig();
			AssetDatabase.Refresh();
		}
	}

	#endregion

	#region AssistantWindowComponentConfigs

	private string InputPathKey => $"AssistantWindow.{Name}.{nameof(inputPath)}";

	private string OutputPathKey => $"AssistantWindow.{Name}.{nameof(outputPath)}";

	private static void TextField(string prefsKey,
		Func<string> getter,
		Action<string> setter)
	{
		var current = getter();
		var newValue = EditorGUILayout.TextField(current);

		if (newValue == current)
		{
			return;
		}

		setter(newValue);
		EditorPrefs.SetString(prefsKey, newValue);
	}

	private void CreateConfig()
	{
		var library = new T();
		var dir = new DirectoryInfo(inputPath);
		var files = dir.GetFiles("*.json");

		JsonSerializerSettings jsonSettings = new()
		{
			Converters =
			{
				new ConfigLinkJsonConverter(),
				new UnionConverter()
			}
		};

		foreach (var fileInfo in files)
		{
			var json = File.ReadAllText(fileInfo.FullName);
			JsonConvert.PopulateObject(json, library, jsonSettings);
		}

		var formatter = new BinaryFormatter();
		using var fs = new FileStream(outputPath, FileMode.OpenOrCreate);
		formatter.Serialize(fs, library);
	}

	#endregion
}

}