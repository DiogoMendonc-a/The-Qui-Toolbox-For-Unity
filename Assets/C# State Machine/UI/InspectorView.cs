using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> {}

    Editor editor;

    public InspectorView() {}

    public void SetSelection(StateView selection) {
        Debug.Log("Changin to state " + selection.state.name);
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(selection.state);
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }

    public void SetSelection(TransitionEdgeView selection) {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(selection.transition);
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
}
