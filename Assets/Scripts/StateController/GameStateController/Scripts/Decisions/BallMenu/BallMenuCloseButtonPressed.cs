using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/BallMenuCloseButtonPressed")]
public class BallMenuCloseButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.ballMenuCloseButtonPressed;
    }
}
