using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/FinishCycle")]
public class FinishCycle : OneShot {

    public override void Act(StateController controller) {
        PlayStateController c = controller as PlayStateController;
        c.ballController.OnCycleFinish(c);
    }
}
