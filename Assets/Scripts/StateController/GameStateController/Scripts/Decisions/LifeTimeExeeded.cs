using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/LifeTimeExeeded")]
public class LifeTimeExeeded : Decision {

    public override bool Decide(StateController controller) => (controller as GameStateController).ballController.LifeTimeExeeded;
}
