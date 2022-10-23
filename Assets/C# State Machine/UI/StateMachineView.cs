using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using Qui.StateMachine;
using System.Collections.Generic;
using System.Linq;
using System;

public class StateMachineView : GraphView
{
    public Action<StateView> onStateSelected;
    public Action<TransitionEdgeView> onTransitionSelected;

    public new class UxmlFactory : UxmlFactory<StateMachineView, GraphView.UxmlTraits> {}

    StateMachineController smc;

    public StateMachineView() {

        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/C# State Machine/UI/StateMachineEditor.uss");
        styleSheets.Add(styleSheet);
    }

    StateView FindStateView(State state) {
        return GetNodeByGuid(state.guid) as StateView;
    }

    public void SetMachine(StateMachineController controller) {
        smc = controller;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        foreach (State state in smc.states)
        {
            StateView sv = CreateStateView(state);
            sv.onStateSelected = onStateSelected;
        }

        foreach (State state in smc.states)
        {
            foreach (Transition t in state.GetTransitions())
            {
                StateView from = FindStateView(state);
                StateView to = FindStateView(t.GetTargetState());

                Edge edge = from.outP.ConnectTo(to.inP);
                TransitionEdgeView  tev = new TransitionEdgeView(t, edge);
                tev.onTransitionSelected = onTransitionSelected;

                AddElement(tev);
            }
        }
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
        return ports.ToList().Where(endPort => 
        endPort.direction != startPort.direction).ToList();
    }

    GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
        EditorUtility.SetDirty(smc);
        if(graphViewChange.elementsToRemove != null) {
            graphViewChange.elementsToRemove.ForEach(elem => {
                StateView stateView = elem as StateView;
                if(stateView != null) {
                    smc.DeleteState(stateView.state);
                }

                Edge edge = elem as Edge;
                if(edge != null) {
                    StateView from = edge.output.node as StateView;
                    StateView to = edge.input.node as StateView;
                    smc.DeleteTransition(from.state, to.state);
                } 
            });
        }

        if(graphViewChange.edgesToCreate != null) {
            CreateEdges(graphViewChange.edgesToCreate);
        }
        return graphViewChange;
    }

    void CreateEdges(List<Edge> toCreate) {
        List<Edge> toRemove = new List<Edge>();
        List<Edge> toAdd = new List<Edge>();
        foreach (Edge edge in toCreate)
        {
            StateView from = edge.output.node as StateView;
            StateView to = edge.input.node as StateView;
            Transition t = smc.CreateTransition(from.state, to.state);
            toRemove.Add(edge);
            TransitionEdgeView  tev = new TransitionEdgeView(t, edge);
            tev.onTransitionSelected = onTransitionSelected;
            toAdd.Add(tev);
        }

        foreach (Edge e in toRemove)
        {
            toCreate.Remove(e);
        }

        foreach (Edge e in toAdd)
        {
            toCreate.Add(e);
        }
    }

    StateView CreateStateView(State state) {
        StateView stateView = new StateView(state, this);
        AddElement(stateView);
        return stateView;
    }

    void CreateNewState() {
        State state = smc.CreateState();
        StateView sv = CreateStateView(state);
        sv.onStateSelected = onStateSelected;
    }

    public void SetInitialState(StateView state) {
        EditorUtility.SetDirty(smc);
        smc.SetInitialState(state.state);
    }

	public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		//base.BuildContextualMenu(evt);

        evt.menu.AppendAction("Create State", (a) => CreateNewState());
	}
}
