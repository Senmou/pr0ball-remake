using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/Decisions/IsGameOver")]
public class IsGameOver : Decision {

    public override bool Decide(StateController controller) => (controller as PlayStateController).isGameOver;
}
