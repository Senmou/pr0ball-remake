using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/States/FinishCycleState")]
public class FinishCycleState : State {

    protected override void OnExitState(StateController controller) {
        (controller as GameStateController).cycleFinished = false;
    }
}
