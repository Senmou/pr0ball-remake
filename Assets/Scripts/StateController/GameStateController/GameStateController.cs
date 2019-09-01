using UnityEngine;

public class GameStateController : StateController {

    [HideInInspector] public StartButton startButton;
    [HideInInspector] public PlayStateController playStateController;
    [HideInInspector] public bool backButtonPressed;
    [HideInInspector] public bool resumeButtonPressed;
    [HideInInspector] public bool optionsButtonPressed;
    [HideInInspector] public bool optionsMenuCloseButtonPressed;
    [HideInInspector] public bool tappedOnPauseBackground;

    private void Awake() {
        playStateController = FindObjectOfType<PlayStateController>();
        startButton = FindObjectOfType<StartButton>();
    }
}
