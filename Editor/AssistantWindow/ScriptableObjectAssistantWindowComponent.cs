namespace EM.Foundation.Editor
{

using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public abstract class ScriptableObjectAssistantWindowComponent<T> : IAssistantWindowComponent
	where T : ScriptableObject
{
	protected string ConfigPath;

	protected T Config;

	#region IAssistantWindowComponent

	public abstract string Name
	{
		get;
	}

	public void Prepare()
	{
		ConfigPath = EditorPrefs.GetString(ConfigPathKey);

		if (!string.IsNullOrWhiteSpace(ConfigPath))
		{
			Config = AssetDatabase.LoadAssetAtPath(ConfigPath, typeof(T)) as T;
		}
	}

	public void OnGUI()
	{
		if (Config == null)
		{
			OnGUIButtons();
		}
		else
		{
			CheckPath();
			OnGUIConfigField();
			OnGUIConfig();
		}
	}

	#endregion

	#region AssistantWindowComponentRealms

	private string ConfigPathKey => $"AssistantWindow.{Name}.{nameof(ConfigPath)}";

	protected abstract string GetCreatePath();

	protected abstract string GetSelectPath();

	protected abstract void OnGUIConfig();

	private void OnGUIButtons()
	{
		using (new EditorHorizontalGroup())
		{
			if (GUILayout.Button("Create"))
			{
				CreateConfig();
				SetAddressableFlag();
			}

			if (GUILayout.Button("Select"))
			{
				SelectConfig();
				SetAddressableFlag();
			}
		}
	}

	private void CreateConfig()
	{
		var path = GetCreatePath();

		if (string.IsNullOrWhiteSpace(path))
		{
			return;
		}

		Config = ScriptableObject.CreateInstance<T>();
		AssetDatabase.CreateAsset(Config, path);
		AssetDatabase.SaveAssets();
		UnityEditor.EditorUtility.FocusProjectWindow();
		Selection.activeObject = Config;
	}

	private void SelectConfig()
	{
		var path = GetSelectPath();

		if (string.IsNullOrWhiteSpace(path))
		{
			return;
		}

		path = "Assets" + path.Remove(0, Application.dataPath.Length);
		Config = AssetDatabase.LoadAssetAtPath<T>(path);
	}

	private void SetAddressableFlag()
	{
		var path = AssetDatabase.GetAssetPath(Config);
		var guiID = AssetDatabase.AssetPathToGUID(path);
		var settings = AddressableAssetSettingsDefaultObject.Settings;
		var assetEntry = settings.CreateOrMoveEntry(guiID, settings.DefaultGroup);
		assetEntry.address = Config.name;
	}

	private void CheckPath()
	{
		if (Config == null)
		{
			return;
		}

		var path = AssetDatabase.GetAssetPath(Config);

		if (path == ConfigPath)
		{
			return;
		}

		ConfigPath = path;
		EditorPrefs.SetString(ConfigPathKey, path);
	}

	private void OnGUIConfigField()
	{
		GUI.enabled = false;
		EditorGUILayout.ObjectField(Config, typeof(T), false);
		GUI.enabled = true;
		EditorGUILayout.Space();
	}

	#endregion
}

}