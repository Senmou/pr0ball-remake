using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Actions/DrainLifeTime")]
public class DrainLifeTime : Action {

    public override void Act(StateController controller) {
        (controller as PlayStateController).ballController.DrainLifeTime();
    }
}
