# How to Use State Machines

All the classes are in the `Qui.StateMachine` namespace

## States
The first step you need to take is to define states for your machine, using the State class.
The State constructor is
	`public State(Action[] entryActions = null, Action[] actions = null, 
                        Action[] exitActions = null, Transition[] transitions = null)`

- `entryActions` are actions to be executed when the machine enters the state
- `actions` are actions to be executed while the machine is in the state
- `exitActions` are actions to be executed when the machine exits the state
- `transitions` is the set of transitions that go out of this state

Any of these parameters can be left empty when creating a new State, and the state has functions for adding new actions and transitions

- void state.AddEntryAction(Action action) 
- void state.AddAction(Action action)
- void state.AddExitAction(Action action)
- void state.AddTransition(Transition transition)

The State also has a `name` string that you can set, which is meant for debug purposes only

The `Action` class is from the namespace `System` and refers to any method that has no parameters and returns null

```csharp
void DefineMachine() {
	State chaseState = new State();
	chaseState.name = "Chase"; 
	State patrolState = new State();
	patrolState.name = "Patrol";
	chaseState.AddAction(Chase);
	patrolState.AddAction(Patrol);
}

void Patrol(){
	Debug.Log("Patroling");
}

void Chase(){
	Debug.Log("Chasing");
}
```

## Transitions

Transitions define situations in which the machine changes its current state.
The Transition constructor is
`Transition(State target, Condition cond, List<Action> act = null)`

- `target` is the state the machine ends in when the transition is triggered
- `cond` is the condition in which the transition is triggered
- `act` is the optional list of actions that are executed when the transition is triggered

Only the `act` parameter is optional, and you can add to it after creating the transition

- `void transition.AddAction(Action action)`

### Conditions
Conditions define when a trasition is triggered. 
The Condition interface is as follows
```csharp
public interface Condition {
public bool Evaluate();
}
```
You are free to create Conditions at will, but there's one already that should cover most needs, the `BooleanFunctionCondition` Condition, which returns whatever the return of its function is
`BooleanFunctionCondition(Func<bool> booleanFunction)`
- `booleanFunction` is any function that returns a boolean (and has no parameters)

```csharp
void DefineMachine() {
State chaseState = new State();
chaseState.name = "Chase"; 
State patrolState = new State();
patrolState.name = "Patrol";
chaseState.AddAction(Chase);
patrolState.AddAction(Patrol);

Condition cond = new BooleanFunctionCondition(IsPlayerDetectable); 
}

public bool IsPlayerDetectable() { ... }
```


Transitions are added to the state they come out of.
```csharp
void DefineMachine() {
	State chaseState = new State();
	chaseState.name = "Chase"; 
	State patrolState = new State();
	patrolState.name = "Patrol";
	chaseState.AddAction(Chase);
	patrolState.AddAction(Patrol);

	Condition cond = new BooleanFunctionCondition(IsPlayerDetectable); 

	Transition findPlayer = new Transition(chaseState, cond);
	patrolState.AddTransition(findPlayer);
}
```

## State Machines
The machine itself.
`StateMachine (State initialState)`
- `initialState` is the state the machine starts in

The StateMachine class need only the initial state, as everything else has already been defined. It has a function to update its state:

- `Action[] stateMachine.Update()` updates the state machine, calculating transitions and new states, and returns a list of actions to be executed

	- If the state did not change, then the list consists only in the `actions` defined in the state
	- If did change, then the lists consists of the actions in the `exitActions` of the former state, the `actions` of the transition that triggered, and the `entryActions` of the new state, in this order

 	+ Objects of the class `Action` can be executed by callling their `Invoke()` method

It also has a function that returns the name of the current state:
- `string stateMachine.GetCurrentStateName()`

Creating the State Machine:

```csharp
StateMachine stateMachine

void DefineMachine() {
	State chaseState = new State();
	chaseState.name = "Chase"; 
	State patrolState = new State();
	patrolState.name = "Patrol";
	chaseState.AddAction(Chase);
	patrolState.AddAction(Patrol);

	Condition cond = new BooleanFunctionCondition(IsPlayerDetectable); 

	Transition findPlayer = new Transition(chaseState, cond);
	patrolState.AddTransition(findPlayer);
	stateMachine = new StateMachine(patrolState);
}
```

Executing the State Machine:
```csharp
void Update() {
	Action[] actions = stateMachine.Update();
	foreach (Action action in actions)
	{
		action.Invoke();
	}
	if(debugText != null) {
		debugText.text = "State: " + stateMachine.GetCurrentStateName();
	}
}
```
