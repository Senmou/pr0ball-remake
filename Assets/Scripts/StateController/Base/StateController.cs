using UnityEngine;

public abstract class StateController : MonoBehaviour {

    public State currentState;
    public State remainState;

    public bool lockOneShots = false;

    public virtual void Update() {
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState) {
        Debug.Log(currentState.name + " => " + nextState.name);
        currentState = nextState;
    }
}
