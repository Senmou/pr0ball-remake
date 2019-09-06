using UnityEngine;

public class GameStateController : StateController {

    [HideInInspector] public bool backButtonPressed;
    [HideInInspector] public bool resumeButtonPressed;
    [HideInInspector] public bool optionsButtonPressed;
    [HideInInspector] public bool ballMenuButtonPressed;
    [HideInInspector] public bool ballMenuCloseButtonPressed;
    [HideInInspector] public bool optionsMenuCloseButtonPressed;
    [HideInInspector] public bool tappedOnPauseBackground;
    [HideInInspector] public bool skillBarLongClick;

    [HideInInspector] public BallMenu ballMenu;
    [HideInInspector] public SkillMenu skillMenu;
    [HideInInspector] public OptionsMenu optionsMenu;

    private void Awake() {
        ballMenu = FindObjectOfType<BallMenu>();
        skillMenu = FindObjectOfType<SkillMenu>();
        optionsMenu = FindObjectOfType<OptionsMenu>();
    }
}
