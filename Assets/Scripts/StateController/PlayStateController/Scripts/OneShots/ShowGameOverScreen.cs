using UnityEngine;

[CreateAssetMenu(menuName = "PlayStateController/OneShots/ShowGameOverScreen")]
public class ShowGameOverScreen : OneShot {

    public override void Act(StateController controller) {

        Statistics.Instance.balls.damage = BallStats.Instance.damage;
        Statistics.Instance.balls.critChance = BallStats.Instance.critChance;
        Statistics.Instance.balls.critDamage = BallStats.Instance.critDamage;
        Statistics.Instance.balls.ballCount = BallStats.Instance.ballCount;

        CanvasManager.instance.SwitchCanvas(CanvasType.GAMEOVER);
        GameOverScreen gameOverScreen = FindObjectOfType<GameOverScreen>();
        gameOverScreen.SaveHighscore();
    }
}
