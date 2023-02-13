namespace EM.Foundation.Editor
{

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class AssistantWindowBase : EditorWindow
{
	private readonly List<AssistantWindowComponentGroupBox> _components = new();

	private Vector2 _scrollPos;

	private string _filter = string.Empty;

	#region EditorWindow

	private void OnEnable()
	{
		CreateComponents();

		foreach (var component in _components)
		{
			component.Prepare();
			component.AddListener(Repaint);
		}
	}

	private void OnDisable()
	{
		_components.ForEach(c => c.RemoveListener(Repaint));
	}

	private void OnGUI()
	{
		OnGuiTopPanel();
		OnGuiComponents();
	}

	#endregion

	#region AssistantWindowBase

	protected abstract IEnumerable<IAssistantWindowComponent> GetWindowComponents();

	private void CreateComponents()
	{
		_components.ForEach(c => c.RemoveListener(Repaint));
		_components.Clear();
		var newComponents = GetWindowComponents();

		foreach (var component in newComponents)
		{
			_components.Add(new AssistantWindowComponentGroupBox(component));
		}
	}

	private void OnGuiTopPanel()
	{
		EditorGUILayout.BeginVertical("GroupBox");

		OnGuiButtons();
		EditorGUILayout.Space();
		OnGuiSearch();

		EditorGUILayout.EndVertical();
	}

	private void OnGuiButtons()
	{
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Show All"))
		{
			_components.ForEach(c => c.Show());
		}

		if (GUILayout.Button("Hide All"))
		{
			_components.ForEach(c => c.Hide());
		}

		EditorGUILayout.EndHorizontal();
	}

	private void OnGuiSearch()
	{
		_filter = EditorGUILayout.TextField(_filter, EditorStyles.toolbarSearchField);
	}

	private void OnGuiComponents()
	{
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

		foreach (var component in _components.Where(component => component.Name.Contains(_filter)))
		{
			component.OnGUI();
		}

		EditorGUILayout.EndScrollView();
	}

	#endregion
}

}