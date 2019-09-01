using UnityEngine;

public class GameStateController : StateController {

    [HideInInspector] public bool backButtonPressed;
    [HideInInspector] public bool resumeButtonPressed;
    [HideInInspector] public bool optionsButtonPressed;
    [HideInInspector] public bool optionsMenuCloseButtonPressed;
    [HideInInspector] public bool tappedOnPauseBackground;
    [HideInInspector] public bool skillBarLongClick;

    [HideInInspector] public SkillMenu skillMenu;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
    }
}
