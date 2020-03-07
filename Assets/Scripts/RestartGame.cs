using UnityEngine;

public class RestartGame : MonoBehaviour {

    private BallMenu ballMenu;
    private SkillBar skillBar;
    private BallController ballController;
    private SkillController skillController;
    private PlayStateController playStateController;

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        skillBar = FindObjectOfType<SkillBar>();
        ballController = FindObjectOfType<BallController>();
        skillController = FindObjectOfType<SkillController>();
        playStateController = FindObjectOfType<PlayStateController>();
    }

    public void StartNewGame() {

        playStateController.gameRestarted = true;
        playStateController.isGameOver = false;
        PersistentData.instance.isGameOver = false;

        skillBar.ResetData();
        ballMenu.ResetData();
        Score.instance.ResetData();
        skillController.ResetData();
        ballController.ResetData();
        Statistics.Instance.ResetData();
    }
}
