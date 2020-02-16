using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/ShowBallMenu")]
public class ShowBallMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.ballMenu.Show();
    }
}
