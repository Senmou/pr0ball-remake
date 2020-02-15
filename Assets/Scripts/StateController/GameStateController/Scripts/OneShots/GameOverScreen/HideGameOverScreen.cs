using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/HideGameOverScreen")]
public class HideGameOverScreen : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.gameOverScreen.Hide();
    }
}
