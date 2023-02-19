namespace EM.Foundation.Editor
{

using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public abstract class ScriptableObjectAssistantWindowComponent<T> : IAssistantWindowComponent
	where T : ScriptableObject
{
	protected string ConfigPath;

	protected T Settings;

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
			Settings = AssetDatabase.LoadAssetAtPath(ConfigPath, typeof(T)) as T;
		}
	}

	public void OnGUI()
	{
		if (Settings == null)
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

		Settings = ScriptableObject.CreateInstance<T>();
		AssetDatabase.CreateAsset(Settings, path);
		AssetDatabase.SaveAssets();
		UnityEditor.EditorUtility.FocusProjectWindow();
		Selection.activeObject = Settings;
	}

	private void SelectConfig()
	{
		var path = GetSelectPath();

		if (string.IsNullOrWhiteSpace(path))
		{
			return;
		}

		path = "Assets" + path.Remove(0, Application.dataPath.Length);
		Settings = AssetDatabase.LoadAssetAtPath<T>(path);
	}

	private void SetAddressableFlag()
	{
		var path = AssetDatabase.GetAssetPath(Settings);
		var guiID = AssetDatabase.AssetPathToGUID(path);
		var settings = AddressableAssetSettingsDefaultObject.Settings;
		var assetEntry = settings.CreateOrMoveEntry(guiID, settings.DefaultGroup);
		assetEntry.address = Settings.name;
	}

	private void CheckPath()
	{
		if (Settings == null)
		{
			return;
		}

		var path = AssetDatabase.GetAssetPath(Settings);

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
		EditorGUILayout.ObjectField(Settings, typeof(T), false);
		GUI.enabled = true;
		EditorGUILayout.Space();
	}

	#endregion
}

}