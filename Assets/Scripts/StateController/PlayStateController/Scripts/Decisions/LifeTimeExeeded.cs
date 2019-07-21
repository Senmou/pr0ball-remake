using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/LifeTimeExeeded")]
public class LifeTimeExeeded : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).ballController.LifeTimeExeeded;
}
