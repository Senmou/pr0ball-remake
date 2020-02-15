using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/IsGameOver")]
public class IsGameOver : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.isGameOver;
    }
}
