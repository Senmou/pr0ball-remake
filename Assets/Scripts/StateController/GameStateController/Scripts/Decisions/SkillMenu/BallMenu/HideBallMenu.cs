using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/HideBallMenu")]
public class HideBallMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.ballMenu.Hide();
    }
}
