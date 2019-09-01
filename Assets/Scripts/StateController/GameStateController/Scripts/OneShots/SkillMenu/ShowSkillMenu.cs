using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/ShowSkillMenu")]
public class ShowSkillMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.skillMenu.Show();
    }
}
