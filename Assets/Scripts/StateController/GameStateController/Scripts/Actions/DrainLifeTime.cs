using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Actions/DrainLifeTime")]
public class DrainLifeTime : Action {

    public override void Act(StateController controller) {
        (controller as GameStateController).ballController.DrainLifeTime();
    }
}
