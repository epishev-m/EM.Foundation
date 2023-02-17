namespace EM.Foundation.Editor
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public sealed class DocumentationWindow : EditorWindow
{
	private const int LeftPanelWidth = 250;

	private const int RightPanelMinWidth = 500;

	private const int TopPanelButtonMinHeight = 80;

	private readonly List<DocumentationType> _docTypes = new();

	private readonly List<string> _groups = new();

	private readonly Dictionary<string, bool> _groupsState = new();

	private readonly List<DocumentationTypeBox> _typesBoxes = new();

	private Vector2 _groupsScrollPos;

	private Vector2 _typesScrollPos;

	private string _groupsFilter = string.Empty;

	private string _typesFilter = string.Empty;

	#region EditorWindow

	public static void Open()
	{
		GetWindow<DocumentationWindow>("Documentation", true);
	}

	private void OnEnable()
	{
		var types = AppDomain.CurrentDomain.GetAssemblies()
			.Where(assembly => !assembly.IsDynamic)
			.SelectMany(assembly => assembly.GetTypes())
			.Where(type => type.GetCustomAttributes<DocumentationAttribute>(true).Any());

		foreach (var type in types)
		{
			FillTypes(type);
		}

		FillGroups();
		CreateTypesBoxes();
	}

	private void OnDisable()
	{
		_docTypes.Clear();
		_groups.Clear();
		_groupsState.Clear();

		_typesBoxes.ForEach(c => c.RemoveListener(Repaint));
	}

	private void OnGUI()
	{
		using (new EditorHorizontalGroup())
		{
			OnGuiGroups();
			OnGuiTypes();
			GUILayout.FlexibleSpace();
		}
	}

	#endregion

	#region EcsDocumentationWindow

	private void FillTypes(MemberInfo type)
	{
		if (_docTypes.Any(docType => docType.Name == type.Name))
		{
			return;
		}

		var attributes = type.GetCustomAttributes<DocumentationAttribute>(true).ToList();
		var descriptions = new List<string>();
		var groups = new Dictionary<string, List<string>>();

		foreach (var attribute in attributes)
		{
			if (!string.IsNullOrWhiteSpace(attribute.CommonDescription))
			{
				descriptions.Add(attribute.CommonDescription);
			}

			if (string.IsNullOrWhiteSpace(attribute.Group))
			{
				continue;
			}

			if (!groups.ContainsKey(attribute.Group))
			{
				groups.Add(attribute.Group, new List<string>());
			}

			groups[attribute.Group].Add(attribute.GroupDescription);
		}

		var docTypes = new DocumentationType()
		{
			Name = type.Name,
			Descriptions = descriptions,
			Groups = groups,
		};

		_docTypes.Add(docTypes);
	}

	private void FillGroups()
	{
		foreach (var docType in _docTypes)
		{
			foreach (var (group, _) in docType.Groups)
			{
				if (_groups.Contains(group))
				{
					continue;
				}

				_groups.Add(group);
				_groupsState.Add(group, false);
			}
		}
	}

	private void CreateTypesBoxes()
	{
		_typesBoxes.ForEach(c => c.RemoveListener(Repaint));
		_typesBoxes.Clear();

		foreach (var docType in _docTypes)
		{
			var docTypeBox = new DocumentationTypeBox(docType);
			docTypeBox.AddListener(Repaint);
			_typesBoxes.Add(docTypeBox);
		}
	}

	#region Groups

	private void OnGuiGroups()
	{
		using (new EditorVerticalGroup())
		{
			OnGuiGroupsTopPanel();
			OnGuiGroupsList();
		}
	}

	private void OnGuiGroupsTopPanel()
	{
		using (new EditorVerticalGroup("GroupBox", GUILayout.MinWidth(LeftPanelWidth)))
		{
			OnGuiGroupsButton();
			EditorGUILayout.Space();
			EditorLayoutUtility.ToolbarSearch(ref _groupsFilter);
		}
	}

	private void OnGuiGroupsButton()
	{
		using (new EditorHorizontalGroup())
		{
			var keys = _groupsState.Keys.ToList();

			if (GUILayout.Button("Select All"))
			{
				foreach (var key in keys)
				{
					_groupsState[key] = true;
				}

				foreach (var component in _typesBoxes)
				{
					component.ShowGroups(_groupsState);
				}
			}

			if (GUILayout.Button("Deselect All"))
			{
				foreach (var key in keys)
				{
					_groupsState[key] = false;
				}
			}
		}
	}

	private void OnGuiGroupsList()
	{
		var showGroup = false;

		using (new EditorVerticalGroup("GroupBox", GUILayout.MinWidth(LeftPanelWidth)))
		{
			using (new EditorScrollView(ref _groupsScrollPos))
			{
				var buttonStyle = new GUIStyle(GUI.skin.button);
				var filter = ToSnakeCase(_groupsFilter);

				foreach (var key in _groups.Where(group =>
					         !string.IsNullOrWhiteSpace(group) && ToSnakeCase(group).Contains(filter)))
				{
					var temp = _groupsState[key];
					_groupsState[key] = GUILayout.Toggle(_groupsState[key], key, buttonStyle);

					if (temp != _groupsState[key])
					{
						showGroup = true;
					}
				}
			}
		}

		if (!showGroup)
		{
			return;
		}

		foreach (var component in _typesBoxes)
		{
			component.ShowGroups(_groupsState);
		}
	}

	#endregion

	#region Types

	private void OnGuiTypes()
	{
		using (new EditorVerticalGroup())
		{
			OnGuiTypesTopPanel();
			OnGuiTypesList();
		}
	}

	private void OnGuiTypesTopPanel()
	{
		using (new EditorVerticalGroup("GroupBox", GetGuiLayoutRightWidth()))
		{
			OnGuiTypesButtons();
			EditorGUILayout.Space();
			EditorLayoutUtility.ToolbarSearch(ref _typesFilter);
		}
	}

	private void OnGuiTypesButtons()
	{
		using (new EditorHorizontalGroup())
		{
			if (GUILayout.Button("Show All", GUILayout.MinWidth(TopPanelButtonMinHeight)))
			{
				foreach (var component in _typesBoxes)
				{
					component.Show();
				}
			}

			if (GUILayout.Button("Hide All", GUILayout.MinWidth(TopPanelButtonMinHeight)))
			{
				foreach (var component in _typesBoxes)
				{
					component.Hide();
				}
			}

			if (GUILayout.Button("Show Selected Groups", GUILayout.MinWidth(TopPanelButtonMinHeight)))
			{
				foreach (var component in _typesBoxes)
				{
					component.ShowGroups(_groupsState);
				}
			}

			if (GUILayout.Button("Hide All Groups", GUILayout.MinWidth(TopPanelButtonMinHeight)))
			{
				foreach (var component in _typesBoxes)
				{
					component.HideAllGroups();
				}
			}

			GUILayout.FlexibleSpace();
		}
	}

	private void OnGuiTypesList()
	{
		using (new EditorVerticalGroup("GroupBox", GetGuiLayoutRightWidth()))
		{
			using (new EditorScrollView(ref _typesScrollPos))
			{
				var filter = ToSnakeCase(_typesFilter);

				foreach (var typeComponent in _typesBoxes.Where(docType => ToSnakeCase(docType.Name).Contains(filter)))
				{
					var groups = typeComponent.Groups;

					if (!groups.Any(group => _groupsState[group]))
					{
						continue;
					}

					typeComponent.OnGUI();
				}
			}
		}
	}

	#endregion

	private static string ToSnakeCase(string s)
	{
		return Regex.Replace(s, "[A-Z]", "_$0").ToLower();
	}

	private GUILayoutOption GetGuiLayoutRightWidth()
	{
		var width = position.width - LeftPanelWidth - 30;

		if (width < RightPanelMinWidth)
		{
			width = RightPanelMinWidth;
		}

		return GUILayout.MinWidth(width);
	}

	#endregion
}

}