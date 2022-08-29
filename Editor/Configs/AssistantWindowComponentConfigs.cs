namespace EM.Foundation.Editor
{

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Foundation;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public sealed class AssistantWindowComponentConfigs<T> : IAssistantWindowComponent
	where T : class, new()
{
	private string _inputPath;

	private string _outputPath;

	#region IAssistantWindowComponent

	public string Name => "Configs";

	public void Prepare()
	{
		_inputPath = EditorPrefs.GetString(InputPathKey);
		_outputPath = EditorPrefs.GetString(OutputPathKey);
	}

	public void OnGUI()
	{
		EditorGUILayout.LabelField("Input path:");
		TextField(InputPathKey, () => _inputPath, value => _inputPath = value);
		EditorGUILayout.LabelField("Output path:");
		TextField(OutputPathKey, () => _outputPath, value => _outputPath = value);
		EditorGUILayout.Space();

		if (GUILayout.Button("Configure"))
		{
			CreateConfig();
			AssetDatabase.Refresh();
			SetAddressableFlag();
		}
	}

	#endregion

	#region AssistantWindowComponentConfigs

	private string InputPathKey => $"AssistantWindow.{Name}.{nameof(_inputPath)}";

	private string OutputPathKey => $"AssistantWindow.{Name}.{nameof(_outputPath)}";

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
		var dir = new DirectoryInfo(_inputPath);
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
		using var fs = new FileStream(_outputPath, FileMode.OpenOrCreate);
		formatter.Serialize(fs, library);
	}

	private void SetAddressableFlag()
	{
		var guiID = AssetDatabase.AssetPathToGUID(_outputPath);
		var settings = AddressableAssetSettingsDefaultObject.Settings;
		var assetEntry = settings.CreateOrMoveEntry(guiID, settings.DefaultGroup);
		var name = Path.GetFileNameWithoutExtension(_outputPath);
		assetEntry.address = name;
	}

	#endregion
}

}