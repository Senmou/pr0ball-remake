using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/FinishCycle")]
public class FinishCycle : OneShot {

    public override void Act(StateController controller) {
        (controller as GameStateController).ballController.OnCycleFinish();
        (controller as GameStateController).cycleFinished = true;
    }
}
