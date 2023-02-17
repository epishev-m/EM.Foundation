namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

public sealed class DocumentationGroupsBox
{
	private readonly IEnumerable<string> _descriptions;

	private readonly IEnumerable<DocumentationAttribute> _attributes;

	private readonly AnimBool _showExtraFields = new(false);

	#region DocumentationGroupsBox

	public DocumentationGroupsBox(string group,
		IEnumerable<string> descriptions)
	{
		Name = group;
		_descriptions = descriptions;
	}

	public string Name { get; }

	public void AddListener(UnityAction call)
	{
		_showExtraFields.valueChanged.AddListener(call);
	}

	public void RemoveListener(UnityAction call)
	{
		_showExtraFields.valueChanged.RemoveListener(call);
	}

	public void Show()
	{
		_showExtraFields.target = true;
	}

	public void Hide()
	{
		_showExtraFields.target = false;
	}

	public void OnGUI()
	{
		GUI.color = _showExtraFields.target ? Color.white : Color.gray;

		using (new EditorVerticalGroup())
		{
			using (var editorFoldout = new EditorFadeGroup(Name, _showExtraFields))
			{
				if (editorFoldout.IsVisible)
				{
					using (new EditorIndentLevel())
					{
						OnGuiTypeGroup();
					}
				}
			}
		}

		EditorGUILayout.Space();
	}

	private void OnGuiTypeGroup()
	{
		foreach (var description in _descriptions)
		{
			EditorGUILayout.Space();

			var descriptionStyle = new GUIStyle(EditorStyles.label)
			{
				wordWrap = true
			};
			EditorGUILayout.LabelField(description, descriptionStyle);
		}
	}

	#endregion
}

}