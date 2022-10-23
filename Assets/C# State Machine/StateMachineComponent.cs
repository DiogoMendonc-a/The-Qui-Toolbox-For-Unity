using UnityEngine;
using Qui.StateMachine;

public class StateMachineComponent : MonoBehaviour
{
    public enum AutoUpdateType {
        NONE,
        UPDATE,
        FIXED_UPDATE,
        LATE_UPDATE
    }
    public StateMachineController controller;
    StateMachine sm;
    public AutoUpdateType autoUpdateOn;

    void Awake() {
        sm = controller.getMachine();    
    }

    void Update() {
        if(autoUpdateOn == AutoUpdateType.UPDATE) {
            sm.Update();
        }
    }

    void FixedUpdate() {
        if(autoUpdateOn == AutoUpdateType.FIXED_UPDATE) {
            sm.Update();
        }
    }

    void LateUpdate() {
        if(autoUpdateOn == AutoUpdateType.LATE_UPDATE) {
            sm.Update();
        }
    }
}
