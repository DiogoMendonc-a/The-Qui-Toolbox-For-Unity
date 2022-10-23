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

        StateDrawer sd = StateDrawer.CreateInstance<StateDrawer>();
        sd.state = selection.state;
        editor = Editor.CreateEditor(sd);
        
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }

    public void SetSelection(TransitionEdgeView selection) {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);

        TransitionDrawer td = TransitionDrawer.CreateInstance<TransitionDrawer>();
        td.transiton = selection.transition;
        editor = Editor.CreateEditor(td);

        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
}
