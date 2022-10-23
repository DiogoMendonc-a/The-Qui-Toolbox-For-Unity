using System;
using UnityEngine;

namespace Qui.StateMachine {
    public interface Condition {
        public bool Evaluate();
    }

    [System.Serializable]
    public class VisualCondition : Condition{
        public bool Evaluate() {
            return true; //TODO
        }
    }

    public class TrueCondition : Condition {
        public bool Evaluate() { return true; }
    }
    
    public class NOTCondition : Condition {

        Condition _condition;

        public NOTCondition(Condition condition) {
            _condition = condition;
        }
        public bool Evaluate() {
            return !_condition.Evaluate();
        }
    }

    public class BooleanFunctionCondition : Condition {

        Func<bool> function;

        public BooleanFunctionCondition(Func<bool> booleanFunction) {
            function = booleanFunction;
        }
        public bool Evaluate() {
            return function.Invoke();
        }
    }

}