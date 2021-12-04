using System;

namespace Qui.StateMachine.Exception {

    [Serializable]
    public class NoStateException : System.Exception
    {
        public NoStateException() : base() { }
        public NoStateException(string message) : base(message) { }
        public NoStateException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected NoStateException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}