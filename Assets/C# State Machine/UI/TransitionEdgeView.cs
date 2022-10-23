using UnityEditor.Experimental.GraphView;
using Qui.StateMachine;
using UnityEngine;
using System;

public class TransitionEdgeView : Edge {
	public Action<TransitionEdgeView> onTransitionSelected;
	public Transition transition;

	public TransitionEdgeView(Transition transition, Edge original) {
		this.transition = transition;
		this.candidatePosition = original.candidatePosition;
		this.input = original.input;
		this.output = original.output;
		this.isGhostEdge = original.isGhostEdge;
		DrawEdge();
	}



	public override void OnSelected()
	{
		base.OnSelected();
        if(onTransitionSelected != null) {
            onTransitionSelected.Invoke(this);
        }
	}

}