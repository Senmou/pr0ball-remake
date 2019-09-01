using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/HidePauseMenu")]
public class HidePauseMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;

        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        pauseMenu.Hide();
    }
}
