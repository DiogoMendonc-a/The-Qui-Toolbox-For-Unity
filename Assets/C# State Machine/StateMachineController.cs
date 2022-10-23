using UnityEngine;
using Qui.StateMachine;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu()]
public class StateMachineController : ScriptableObject
{

    public List<State> states = new List<State>();

    public State initialState;

    public State CreateState() {
        State state = State.CreateInstance<State>();
        state.name = "New State";
        state.guid = GUID.Generate().ToString();
        states.Add(state);

        return state;
    }

    public void DeleteState(State state) {
        states.Remove(state);
    }

    public Transition CreateTransition(State from, State to) {
        Transition t = Transition.CreateInstance<Transition>();
        t.SetTargetState(to);
        from.AddTransition(t);
        return t;
    }

    public void DeleteTransition(State from, State to) {
        foreach (Transition t in from.GetTransitions())
        {
            if(t.GetTargetState() == to) {
                from.RemoveTransition(t);
            }
        }
    }

    public Transition[] GetTransitions(State state) {
        return state.GetTransitions();
    }

    public StateMachine getMachine() {
        return new StateMachine(initialState);
    }

    public void SetInitialState(State state) {
        initialState = state;
    }
}
