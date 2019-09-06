using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/SkillMenuCloseButtonPressed")]
public class SkillMenuCloseButtonPressed : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.skillMenuCloseButtonPressed;
    }
}
