using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Qui.StateMachine {
    
    [System.Serializable]
    public class Transition {
        public class DefaultTransition : Transition {
            public DefaultTransition(State target, List<Action> act = null) : 
                base(target, new List<Condition>{ new TrueCondition() }, act) {}
        }

        public UnityEvent events;
        List<Action> actions;
        State targetState;

        public List<Condition> conditions;
        public List<VisualCondition> _conditions;

        public Transition(State target, List<Condition> conditions = null, List<Action> act = null) {
            targetState = target;
            this.conditions = conditions;
            if(act !=null)
                actions = new List<Action>(act);
            else
                actions = new List<Action>();    
        }

        public void AddCondition(Condition condition) {
            conditions.Add(condition);
        }

        public void SetTargetState(State target) {
            targetState = target;
        }
        
        public void AddAction(Action action) {
            actions.Add(action);
        }

        public Action[] GetActions() { 
            return actions.ToArray(); 
        }
        public State GetTargetState() {
            return targetState; 
        }

        public bool IsTriggered() {
            bool trigger = true;
            foreach (Condition c in conditions)
            {
                trigger = trigger && c.Evaluate();
            }
            return trigger;
        }
    }
}