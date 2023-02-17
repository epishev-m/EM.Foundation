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
		using (new EditorVerticalGroup("GroupBox"))
		{
			OnGuiButtons();
			EditorGUILayout.Space();
			OnGuiSearch();
		}
	}

	private void OnGuiButtons()
	{
		using (new EditorHorizontalGroup())
		{
			if (GUILayout.Button("Show All"))
			{
				_components.ForEach(c => c.Show());
			}

			if (GUILayout.Button("Hide All"))
			{
				_components.ForEach(c => c.Hide());
			}
		}
	}

	private void OnGuiSearch()
	{
		EditorLayoutUtility.ToolbarSearch(ref _filter);
	}

	private void OnGuiComponents()
	{
		using (new EditorScrollView(ref _scrollPos))
		{
			foreach (var component in _components.Where(component => component.Name.Contains(_filter)))
			{
				component.OnGUI();
			}
		}
	}

	#endregion
}

}