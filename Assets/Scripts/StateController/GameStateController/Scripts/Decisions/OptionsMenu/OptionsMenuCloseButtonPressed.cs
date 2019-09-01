using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/OptionsMenuCloseButtonPressed")]
public class OptionsMenuCloseButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.optionsMenuCloseButtonPressed;
    }
}
