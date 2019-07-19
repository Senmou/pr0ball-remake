using UnityEngine;

public abstract class State : ScriptableObject {

    public OneShot[] oneShots;
    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(StateController controller) {
        UpdateMethod(controller);

        if (!controller.lockOneShots) {
            DoOneShots(controller);
            controller.lockOneShots = true;
        }

        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(controller);
        }
    }

    private void DoOneShots(StateController controller) {
        for (int i = 0; i < oneShots.Length; i++) {
            oneShots[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller) {
        for (int i = 0; i < transitions.Length; i++) {
            bool decision = (transitions[i].alwaysTrue) ? true : transitions[i].decision.Decide(controller);

            if (decision) {
                if (transitions[i].trueState != controller.remainState) {
                    OnExitState(controller);
                    ResetOneShotLock(controller);
                    controller.TransitionToState(transitions[i].trueState);
                }
            } else {
                if (transitions[i].falseState != controller.remainState) {
                    OnExitState(controller);
                    ResetOneShotLock(controller);
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    }

    private void ResetOneShotLock(StateController controller) {
        controller.lockOneShots = false;
    }

    protected virtual void UpdateMethod(StateController controller) {

    }

    protected virtual void OnExitState(StateController controller) {

    }
}
