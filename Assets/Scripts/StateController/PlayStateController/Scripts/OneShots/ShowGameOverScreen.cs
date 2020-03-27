using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/ShowGameOverScreen")]
public class ShowGameOverScreen : OneShot {

    public override void Act(StateController controller) {
        CanvasManager.instance.SwitchCanvas(CanvasType.GAMEOVER);
        GameOverScreen gameOverScreen = FindObjectOfType<GameOverScreen>();
        gameOverScreen.SaveHighscore();
    }
}
