using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/FinishCycle")]
public class FinishCycle : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.ballController.OnCycleFinish(c);
    }
}
