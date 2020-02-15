using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/ShowGameOverScreen")]
public class ShowGameOverScreen : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.gameOverScreen.Show();
    }
}
