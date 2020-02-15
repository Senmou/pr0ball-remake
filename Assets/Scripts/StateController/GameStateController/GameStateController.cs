using UnityEngine;

public class GameStateController : StateController {

    [HideInInspector] public bool backButtonPressed;
    [HideInInspector] public bool resumeButtonPressed;
    [HideInInspector] public bool tappedOnPauseBackground;

    // Options menu
    [HideInInspector] public bool optionsButtonPressed;
    [HideInInspector] public bool optionsMenuCloseButtonPressed;

    // Ball menu
    [HideInInspector] public bool ballMenuButtonPressed;
    [HideInInspector] public bool ballMenuCloseButtonPressed;

    // Skill menu
    [HideInInspector] public bool skillBarLongClick;
    [HideInInspector] public bool skillMenuCloseButtonPressed;

    // Game Over screen
    [HideInInspector] public bool isGameOver;
    [HideInInspector] public bool newGameButtonPressed;

    [HideInInspector] public BallMenu ballMenu;
    [HideInInspector] public SkillMenu skillMenu;
    [HideInInspector] public OptionsMenu optionsMenu;
    [HideInInspector] public GameOverScreen gameOverScreen;

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        skillMenu = FindObjectOfType<SkillMenu>();
        optionsMenu = FindObjectOfType<OptionsMenu>();
        gameOverScreen = FindObjectOfType<GameOverScreen>();
    }
}
