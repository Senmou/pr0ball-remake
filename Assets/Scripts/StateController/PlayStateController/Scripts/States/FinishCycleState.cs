using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/States/FinishCycleState")]
public class FinishCycleState : State {

    protected override void OnExitState(StateController controller) {
        (controller as PlayStateController).cycleFinished = false;
    }
}
