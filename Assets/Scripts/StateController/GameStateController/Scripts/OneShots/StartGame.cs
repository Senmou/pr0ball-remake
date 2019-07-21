using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/StartGame")]
public class StartGame : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.playStateController.hasGameStarted = true;
        c.startButton.pressed = false;
    }
}
