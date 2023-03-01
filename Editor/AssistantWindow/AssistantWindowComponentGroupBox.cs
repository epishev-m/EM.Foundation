namespace EM.Foundation.Editor
{

using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;

public sealed class AssistantWindowComponentGroupBox : IAssistantWindowComponent
{
	private readonly IAssistantWindowComponent _component;

	private readonly AnimBool _showExtraFields = new(false);

	#region IAssistantWindowComponent

	public string Name => _component.Name;

	public void Prepare() => _component.Prepare();

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

				EditorGUI.indentLevel++;
				_component.OnGUI();
				EditorGUI.indentLevel--;
			}
		}
	}

	#endregion

	#region AssistantWindowComponentGroupBox

	public AssistantWindowComponentGroupBox(IAssistantWindowComponent component)
	{
		Requires.NotNullParam(component, nameof(component));

		_component = component;
	}

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

	#endregion
}

}