using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/BallMenuButtonPressed")]
public class BallMenuButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.ballMenuButtonPressed;
    }
}
