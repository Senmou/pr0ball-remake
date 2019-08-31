using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/OneShots/ShowPauseMenu")]
public class ShowPauseMenu : OneShot {

    public override void Act(StateController controller) {
        GameStateController c = controller as GameStateController;

        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        pauseMenu.Show();
    }
}
