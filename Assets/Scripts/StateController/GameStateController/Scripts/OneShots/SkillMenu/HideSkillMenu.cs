using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/HideSkillMenu")]
public class HideSkillMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.skillMenu.Hide();
    }
}
