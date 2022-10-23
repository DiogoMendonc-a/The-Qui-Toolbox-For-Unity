using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Qui.StateMachine {
    public class State : ScriptableObject{
        public new string name;
        [HideInInspector]
        public string guid;
        [HideInInspector]
        public float guiPositionX;
        [HideInInspector]
        public float guiPositionY;


        List<Action> _entryActions;
        List<Action> _actions;
        List<Action> _exitActions;
        public UnityEvent _entryEvent;
        public UnityEvent _executionEvent;
        public UnityEvent _exitEvent;
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
            if(_transitions == null) _transitions = new List<Transition>();
            _transitions.Add(transition);
        }

        public void RemoveTransition(Transition transition) {
            if(_transitions == null) _transitions = new List<Transition>();
            _transitions.Remove(transition);
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
            if(_transitions == null) _transitions = new List<Transition>();
            return _transitions.ToArray();
        }
    }
}