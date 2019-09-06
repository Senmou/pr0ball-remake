using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/HideBallMenu")]
public class HideBallMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;

        BallMenu ballMenu = FindObjectOfType<BallMenu>();
        ballMenu.Hide();
    }
}
