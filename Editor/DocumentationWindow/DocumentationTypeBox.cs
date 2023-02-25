namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Events;

public sealed class DocumentationTypeBox
{
	private readonly DocumentationType _docType;

	private readonly List<DocumentationGroupsBox> _groups = new();

	private readonly AnimBool _showExtraFields = new(false);

	#region DocumentationType

	public DocumentationTypeBox(DocumentationType docType)
	{
		_docType = docType;
		FillGroups();
	}

	public string Name => _docType.Name;

	public IEnumerable<string> Groups => _docType.Groups.Keys.ToArray();

	public void OnGUI()
	{
		using (new EditorVerticalGroup("GroupBox"))
		{
			using (var fadeGroup = new EditorFadeGroup(Name, _showExtraFields))
			{
				if (!fadeGroup.IsVisible)
				{
					return;
				}

				using (new EditorIndentLevel())
				{
					OnGuiType();
				}
			}
		}
	}

	public void ShowGroups(Dictionary<string, bool> groupsState)
	{
		_showExtraFields.target = true;

		foreach (var group in _groups)
		{
			if (groupsState[group.Name])
			{
				group.Show();
			}
			else
			{
				group.Hide();
			}
		}
	}

	public void HideAllGroups()
	{
		_showExtraFields.target = true;

		foreach (var group in _groups)
		{
			group.Hide();
		}
	}

	public void AddListener(UnityAction call)
	{
		_showExtraFields.valueChanged.AddListener(call);

		foreach (var group in _groups)
		{
			group.AddListener(call);
		}
	}

	public void RemoveListener(UnityAction call)
	{
		_showExtraFields.valueChanged.RemoveListener(call);

		foreach (var group in _groups)
		{
			group.RemoveListener(call);
		}
	}

	public void Show()
	{
		_showExtraFields.target = true;

		foreach (var group in _groups)
		{
			group.Show();
		}
	}

	public void Hide()
	{
		_showExtraFields.target = false;

		foreach (var group in _groups)
		{
			group.Hide();
		}
	}

	private void FillGroups()
	{
		foreach (var (group, descriptions) in _docType.Groups)
		{
			_groups.Add(new DocumentationGroupsBox(group, descriptions));
		}
	}

	private void OnGuiType()
	{
		using (new EditorVerticalGroup())
		{
			OnGuiCommonDescription();
			EditorGUILayout.Space();

			foreach (var groupBox in _groups)
			{
				groupBox.OnGUI();
			}
		}
	}

	private void OnGuiCommonDescription()
	{
		foreach (var description in _docType.Descriptions)
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