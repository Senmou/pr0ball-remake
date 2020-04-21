using UnityEngine;

public class RestartGame : MonoBehaviour {

    private BallMenu ballMenu;
    private SkillBar skillBar;
    private BallController ballController;
    private SkillController skillController;
    private SkillPointSlider skillPointSlider;
    private PlayStateController playStateController;

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        skillBar = FindObjectOfType<SkillBar>();
        ballController = FindObjectOfType<BallController>();
        skillController = FindObjectOfType<SkillController>();
        skillPointSlider = FindObjectOfType<SkillPointSlider>();
        playStateController = FindObjectOfType<PlayStateController>();
    }

    public void OnStartButtonClicked() {

        CanvasManager.instance.SwitchCanvas(CanvasType.SECURITY_QUESTION_NEW_GAME, false);
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
        skillPointSlider.ResetData();

        CanvasManager.instance.SwitchCanvas(CanvasType.NONE);
    }
}
