using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/States/FinishCycleState")]
public class FinishCycleState : State {

    protected override void OnExitState(StateController controller) {
        PlayStateController c = controller as PlayStateController;
        EventManager.TriggerEvent("WaveCompleted");
        c.cycleFinished = false;
    }
}
