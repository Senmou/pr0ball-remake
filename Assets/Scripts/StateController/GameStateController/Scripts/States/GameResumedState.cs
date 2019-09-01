using UnityEngine;

[CreateAssetMenu(menuName = "GameStateController/States/GameResumedState")]
public class GameResumedState : State {

    protected override void OnExitState(StateController controller) {
        GameStateController c = controller as GameStateController;
        c.backButtonPressed = false;
        c.resumeButtonPressed = false;
        c.tappedOnPauseBackground = false;
        c.optionsMenuCloseButtonPressed = false;
    }
}
