using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class StateMachineEditor : EditorWindow
{
    StateMachineView smView;
    InspectorView iView;

    [MenuItem("Window/StateMachineEditor")]
    public static void ShowExample()
    {
        StateMachineEditor wnd = GetWindow<StateMachineEditor>();
        wnd.titleContent = new GUIContent("StateMachineEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/C# State Machine/UI/StateMachineEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/C# State Machine/UI/StateMachineEditor.uss");
        root.styleSheets.Add(styleSheet);

        smView = root.Q<StateMachineView>();
        iView = root.Q<InspectorView>();

        smView.onStateSelected = OnStateSelectionChanged;
        smView.onTransitionSelected = OnTransitionelectionChanged;

        OnSelectionChange();
    }

    private void OnSelectionChange() {
        if(Selection.activeObject is StateMachineController) {
            StateMachineController sm = (StateMachineController)Selection.activeObject;
            if(sm) {
                smView.SetMachine(sm);
                iView.Clear();
            }
        }
    }

    void OnStateSelectionChanged(StateView selected) { 
        iView.SetSelection(selected);
    }

    void OnTransitionelectionChanged(TransitionEdgeView selected) { 
        iView.SetSelection(selected);
    }
}