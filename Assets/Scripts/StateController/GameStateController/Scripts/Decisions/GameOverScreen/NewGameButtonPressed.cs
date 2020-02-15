using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/NewGameButtonPressed")]
public class NewGameButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.newGameButtonPressed;
    }
}
