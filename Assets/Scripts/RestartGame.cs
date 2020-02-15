using UnityEngine;
using TMPro;

public class RestartGame : MonoBehaviour {

    private BallMenu ballMenu;
    private SkillBar skillBar;
    private SkillController skillController;
    private PlayStateController playStateController;
    private GameStateController gameStateController;

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        skillBar = FindObjectOfType<SkillBar>();
        skillController = FindObjectOfType<SkillController>();
        playStateController = FindObjectOfType<PlayStateController>();
        gameStateController = FindObjectOfType<GameStateController>();
    }

    public void StartNewGame() {

        playStateController.gameRestarted = true;
        gameStateController.newGameButtonPressed = true;

        skillBar.ResetData();
        ballMenu.ResetData();
        Score.instance.ResetData();
        skillController.ResetData();
    }
}
