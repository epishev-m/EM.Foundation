namespace EM.Foundation.Editor
{

using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;

public sealed class AssistantWindowComponentGroupBox :
	IAssistantWindowComponent
{
	private readonly IAssistantWindowComponent component;

	private readonly AnimBool showExtraFields = new(false);

	#region IAssistantWindowComponent

	public string Name => component.Name;

	public void Prepare() => component.Prepare();

	public void OnGUI()
	{
		EditorGUILayout.BeginVertical("GroupBox");
		showExtraFields.target = EditorGUILayout.Foldout(showExtraFields.target, Name, true);

		if (EditorGUILayout.BeginFadeGroup(showExtraFields.faded))
		{
			EditorGUI.indentLevel++;
			component.OnGUI();
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndFadeGroup();
		EditorGUILayout.EndVertical();
	}

	#endregion

	#region AssistantWindowComponentGroupBox

	public AssistantWindowComponentGroupBox(IAssistantWindowComponent component)
	{
		Requires.NotNull(component, nameof(component));

		this.component = component;
	}

	public void AddListener(UnityAction call)
	{
		showExtraFields.valueChanged.AddListener(call);
	}

	public void RemoveListener(UnityAction call)
	{
		showExtraFields.valueChanged.RemoveListener(call);
	}

	public void Show()
	{
		showExtraFields.target = true;
	}

	public void Hide()
	{
		showExtraFields.target = false;
	}

	#endregion
}

}