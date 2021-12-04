using System.Collections.Generic;
using System;

namespace Qui.StateMachine {
    public class State {
        public string name;
        List<Action> _entryActions;
        List<Action> _actions;
        List<Action> _exitActions;
        List<Transition> _transitions;

        public State(Action[] entryActions = null, Action[] actions = null, 
                        Action[] exitActions = null, Transition[] transitions = null)
            {
                if(entryActions != null)
                    _entryActions = new List<Action>(entryActions);
                else 
                    _entryActions = new List<Action>();
                if(actions != null)
                    _actions = new List<Action>(actions);
                else 
                    _actions = new List<Action>();
                if(exitActions != null)
                    _exitActions = new List<Action>(exitActions);
                else 
                    _exitActions = new List<Action>();
                if(transitions != null)
                    _transitions = new List<Transition>(transitions);
                else 
                    _transitions = new List<Transition>();
            }

        public void AddEntryAction(Action action) {
            _entryActions.Add(action);
        }

        public void AddAction(Action action) {
            _actions.Add(action);
        }

        public void AddExitAction(Action action) {
            _exitActions.Add(action);
        }

        public void AddTransition(Transition transition) {
            _transitions.Add(transition);
        }

        public Action[] GetEntryActions() {
            return _entryActions.ToArray();
        }

        public Action[] GetActions() {
            return _actions.ToArray();
        }

        public Action[] GetExitActions() {
            return _exitActions.ToArray();
        }

        public Transition[] GetTransitions() {
            return _transitions.ToArray();
        }
    }
}