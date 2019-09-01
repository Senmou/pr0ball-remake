using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/OptionsButtonPressed")]
public class OptionsButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;

        if (c.optionsButtonPressed) {
            c.optionsButtonPressed = false;
            return true;
        }
        return false;
    }
}
