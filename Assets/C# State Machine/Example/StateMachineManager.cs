using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Qui.StateMachine;
using UnityEngine.UI;

public class StateMachineManager : MonoBehaviour
{
    [Header("UI INPUT")]
    public InputField xField;
    public InputField yField;
    public Toggle aToggle;
    public Toggle bToggle;
    public Toggle cToggle;
    public Button resetButton;

    [Header("UI STATES IMAGES")]
    public Image entry;
    public Image alpha;
    public Image beta;
    public Image delta;
    public Image delta_A;
    public Image delta_B;
    public Image delta_C;

    StateMachine machine;
    StateMachine deltaMachine;

    float x = 0;
    float y = 0;
    bool condA;
    bool condB;
    bool condC;


    void SetupMachine() {
        //Create the states of the machine
        State entryState = new State();
        entryState.name = "Entry State"; //For in code debug only
        
        State alphaState = new State();
        alphaState.name = "Alpha State";
        
        State betaState = new State();
        betaState.name = "Beta State";

        State deltaState = new State();
        deltaState.name = "Delta State";

        //Create the conditions that trigger transitions
        Condition xValue = new BooleanFunctionCondition(CheckXValue); //Using a regular function
        Condition yValue = new BooleanFunctionCondition(() => y > 6); //Using a lambda expression

        //Associate conditions with transitions
        Transition defaultToAlpha = new Transition.DefaultTransition(alphaState); //Entry state immediatelly goes to alpha state
        Transition transAlpha = new Transition(alphaState, new List<Condition>{ new NOTCondition(yValue) } );
        Transition transBeta = new Transition(betaState, new List<Condition>{ xValue });
        Transition transDelta = new Transition(deltaState, new List<Condition>{ yValue });

        //Give States The Trasitions They can Trigger
        entryState.AddTransition(defaultToAlpha);

        alphaState.AddTransition(transBeta);
        alphaState.AddTransition(transDelta);

        betaState.AddTransition(transDelta);

        deltaState.AddTransition(transAlpha);

        //Add behaviour to States 
        SetupCommonStateBehaviour(entryState, entry); 
        SetupCommonStateBehaviour(alphaState, alpha);
        SetupCommonStateBehaviour(betaState, beta);
        SetupCommonStateBehaviour(deltaState, delta);

        deltaState.AddAction(UpdateDeltaMachine);

        //Add behaviour to Transitions
        defaultToAlpha.AddAction(() => Debug.Log("Defaulting to Alpha"));
        transAlpha.AddAction(() => Debug.Log("Transitioning to Alpha"));
        transBeta.AddAction(() => Debug.Log("Transitioning to Beta"));
        transDelta.AddAction(() => Debug.Log("Transitioning to Delta"));

        //Creating the machine
        machine = new StateMachine(entryState);
    }

    void SetupCommonStateBehaviour(State state, Image image) {
        state.AddEntryAction(() => Debug.Log("Entering " + state.name)); //When initiating a state machine, its entry actions are not called
        state.AddEntryAction(() => image.color = Color.green);
        state.AddAction(() => Debug.Log("Staying in " + state.name)); //In the frame a state is exited, its actions are not called
        state.AddExitAction(() => Debug.Log("Exiting " + state.name));
        state.AddExitAction(() => image.color = Color.white);
    }

    void SetupDeltaMachine() {
        //Create the states of the machine
        State deltaAState = new State();
        deltaAState.name = "Delta-A State";
        
        State deltaBState = new State();
        deltaBState.name = "Delta-B State";

        State deltaCState = new State();
        deltaCState.name = "Delta-C State";

        //Create the conditions that trigger transitions
        Condition conditionA = new BooleanFunctionCondition(CheckA);        //Using a regular function
        Condition conditionB = new BooleanFunctionCondition(() => condB);   //Using a lambda expression
        Condition conditionC = new BooleanFunctionCondition(() => condC);

        //Associate conditions with transitions
        Transition transA = new Transition(deltaAState, new List<Condition>{ conditionA });
        Transition transB = new Transition(deltaBState, new List<Condition>{ conditionB });
        Transition transC = new Transition(deltaCState, new List<Condition>{ conditionC });

        //Give States The Trasitions They can Trigger
        deltaAState.AddTransition(transB);
        deltaBState.AddTransition(transC);
        deltaCState.AddTransition(transA);

        //Add behaviour to States

        SetupCommonStateBehaviour(deltaAState, delta_A);
        SetupCommonStateBehaviour(deltaBState, delta_B);
        SetupCommonStateBehaviour(deltaCState, delta_C);

        //Add behaviour to Transitions
        transA.AddAction(() => Debug.Log("Transitioning to Delta-A"));
        transB.AddAction(() => Debug.Log("Transitioning to Delta-B"));
        transC.AddAction(() => Debug.Log("Transitioning to Delta-C"));

        //Creating the machine
        deltaMachine = new StateMachine(deltaAState);

        delta_A.color = Color.green;
    }

    // Start is called before the first frame update
    void Start()
    {   
        SetupMachine();
        SetupDeltaMachine();

        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {   
        Action[] actions = machine.Update();
        foreach(Action a in actions) {
            a.Invoke();
        }
    }

    void UpdateDeltaMachine()
    {
        Action[] actions = deltaMachine.Update();
        foreach(Action a in actions) {
            a.Invoke();
        }
    }
    
    void ResetMachine() {
        SetupMachine();
        entry.color = Color.green;
        alpha.color = Color.white;
        beta.color = Color.white;
        delta.color = Color.white;
    }

    void SetupUI() {
        condA = aToggle.isOn;
        condB = bToggle.isOn;
        condC = cToggle.isOn;

        xField.onEndEdit.AddListener((str) => x = float.Parse(str));
        yField.onEndEdit.AddListener((str) => y = float.Parse(str));

        aToggle.onValueChanged.AddListener((b) => condA = aToggle.isOn);
        bToggle.onValueChanged.AddListener((b) => condB = bToggle.isOn);
        cToggle.onValueChanged.AddListener((b) => condC = cToggle.isOn);
    
        resetButton.onClick.AddListener(ResetMachine);
    }

    bool CheckXValue() {
        return x > 5;
    }

    bool CheckA() {
        return condA;
    }

}
