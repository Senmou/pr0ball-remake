using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/Decisions/OpenedSkillMenu")]
public class OpenedSkillMenu : Decision {

    public override bool Decide(StateController controller) {
        GameStateController c = controller as GameStateController;
        return c.skillBarLongClick;
    }
}
