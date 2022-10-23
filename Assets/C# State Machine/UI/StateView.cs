using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using Qui.StateMachine;
using System;

public class StateView : Node
{
    StateMachineView smv;
    public Action<StateView> onStateSelected;
    public State state;

    public Port inP;
    public Port outP;

    public StateView(State state, StateMachineView stateMachineView) {
        smv = stateMachineView;
        this.state = state;
        title = this.state.name;
        this.viewDataKey = state.guid;

        style.left = state.guiPositionX;
        style.top = state.guiPositionY;

        CreateIns();
        CreateOuts();
    }

    void CreateIns() {
        inP = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        inP.portName = "In";
        inputContainer.Add(inP);
    }

    void CreateOuts() {
        outP = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        outP.portName = "Out";
        outputContainer.Add(outP);
    }

	public override void SetPosition(Rect newPos)
	{
		base.SetPosition(newPos);
        state.guiPositionX = newPos.xMin;
        state.guiPositionY = newPos.yMin;
	}

	public override void OnSelected()
	{
		base.OnSelected();
        if(onStateSelected != null) {
            onStateSelected.Invoke(this);
        }
	}

    void SetInitialState() {
        smv.SetInitialState(this);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		//base.BuildContextualMenu(evt);

        evt.menu.AppendAction("Set Initial State", (a) => SetInitialState());
	}
}
