using System;
using System.Collections.Generic;

namespace Qui.StateMachine {
    public class Transition {
        public class DefaultTransition : Transition {
            public DefaultTransition(State target, List<Action> act = null) : base(target,  new TrueCondition(), act) {}
        }

        List<Action> actions;
        State targetState;

        Condition condition;

        public Transition(State target, Condition cond, List<Action> act = null) {
            targetState = target;
            condition = cond;
            if(act !=null)
                actions = new List<Action>(act);
            else
                actions = new List<Action>();    
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
            return condition.Evaluate();
        }
    }
}