using System.Collections.Generic;
using System;
using Qui.StateMachine.Exception;

namespace Qui.StateMachine {
    public class StateMachine {
        State currentState;

        public StateMachine () {}

        public StateMachine (State initialState) {
            currentState = initialState;
        }

        public void SetState(State state) {
            currentState = state;
        }

        public Action[] Update() {
            if(currentState == null) {
                throw new NoStateException("No State on State Machine");
            }
            Transition triggered = null;
            foreach(Transition t in currentState.GetTransitions()) {
                if(t.IsTriggered()) {
                    triggered= t;
                    break;
                }
            }
            if(triggered != null) {
                State targetState = triggered.GetTargetState();
                List<Action> actions = new List<Action>(currentState.GetExitActions());
                actions.AddRange(triggered.GetActions());
                actions.AddRange(targetState.GetEntryActions());

                currentState = targetState;
                return actions.ToArray();
            }
            else {
                return currentState.GetActions();
            }

        }

        public string GetCurrentStateName() {
            return currentState.name;
        }
    }
}